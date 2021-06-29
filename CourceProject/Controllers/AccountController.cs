using CourceProject.Utility;
using CourceProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourceProject.Controllers {
  public class AccountController : Controller {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager) {
      _userManager = userManager;
      _signInManager = signInManager;
    }
    public IActionResult Register() {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model) {
      if(ModelState.IsValid) {
        var user = new IdentityUser {
          UserName = model.Username,
          Email = model.Email,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if(result.Succeeded) {
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          var callbackUrl = Url.Action(
              "ConfirmEmail",
              "Account",
              new { userId = user.Id, code = code },
              protocol: HttpContext.Request.Scheme);
          EmailService emailService = new EmailService();
          await emailService.SendEmailAsync(model.Email, "Confirm your account",
              $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

          return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
          //return RedirectToAction("Index", "Home");
        }
        foreach(var error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
        ModelState.AddModelError(string.Empty, "Неверные данные");
      }
      return View(model);
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code) {
      if(userId == null || code == null) {
        return View("Error");
      }
      var user = await _userManager.FindByIdAsync(userId);
      if(user == null) {
        return View("Error");
      }
      var result = await _userManager.ConfirmEmailAsync(user, code);
      if(result.Succeeded) {
        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
      } else
        return View("Error");
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null) {
      return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel user) {
      if(ModelState.IsValid) {
        var user1 = await _userManager.FindByEmailAsync(user.Email);

        if(user1 == null) {
          ModelState.AddModelError(string.Empty, "Неверные данные");
          return View(user);
        }
        var result = await _signInManager.PasswordSignInAsync(user1.UserName, user.Password, user.RememberMe, false);
        if(result.Succeeded) {
          return RedirectToAction("Index", "Home");
        } else if(!await _userManager.IsEmailConfirmedAsync(user1)) {
          ModelState.AddModelError(string.Empty, "Подтвердите вашу почту");
        } else {
          ModelState.AddModelError(string.Empty, "Неверные данные");
        }
      }
      return View(user);
    }
    public async Task<IActionResult> Logout() {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Login");
    }

  }
}