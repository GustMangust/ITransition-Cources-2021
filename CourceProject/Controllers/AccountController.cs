using CourceProject.Data.Repository;
using CourceProject.Models;
using CourceProject.Utility;
using CourceProject.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Controllers {
  public class AccountController : Controller {
    private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private IRepository ctx;
    public AccountController(IRepository repo,Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager) {
      _userManager = userManager;
      _signInManager = signInManager;
      ctx = repo;
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
        return RedirectToAction("AllFanfics", "Fanfic");
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
          if(ctx.GetPreferences(user1.Id).Count == 0) {
            Debug.WriteLine(user1.Id);
            return RedirectToAction("SetPreferences", "Account");
          }
          return RedirectToAction("AllFanfics", "Fanfic");
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
    [HttpGet]
    public IActionResult SetPreferences() {
      ViewData["Id"] = new SelectList(ctx.GetAllFandoms(), "Id", "Name");
      ViewBag.preferences = ctx.GetPreferences(User.Identity.GetUserId());
      ViewBag.fandoms = ctx.GetAllFandoms();
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> SetPreferences(Preference preference) {
      preference.UserId = User.Identity.GetUserId();
      if(ctx.GetPreferences(User.Identity.GetUserId()).FirstOrDefault(x => x.FandomId == preference.FandomId) == null) {
        Debug.WriteLine("Cool");
        ctx.AddPreference(preference);

      }
      await ctx.SaveChangesAsync();
      return RedirectToAction("SetPreferences");
    }
    [HttpPost]
    public async Task<IActionResult> RemovePreference(int preferenceId) {
      
      if(ctx.GetPreference(preferenceId) != null && ctx.GetPreferences(User.Identity.GetUserId()).Count-1>=1) {
        ctx.RemovePreference(preferenceId);
      }
      await ctx.SaveChangesAsync();
      return RedirectToAction("SetPreferences");
    }
    [HttpPost]
    public IActionResult EndChangePreferences() {
      if(ctx.GetPreferences(User.Identity.GetUserId()).Count >= 1) {
        return RedirectToAction("AllFanfics", "Fanfic");
      } else {
        return RedirectToAction("SetPreferences", "Account");
      }
    }
    [HttpGet]
    public async Task<IActionResult> UserSettings() {
      IdentityUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
      ViewBag.User = user;
      ViewBag.Preferences = ctx.GetPreferences(user.Id);
      ViewBag.Fandoms = ctx.GetAllFandoms();
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> EditUsername(string username,string userId) {
      IdentityUser user = await _userManager.FindByIdAsync(userId);
      if(username!=null && username != "") {
        user.UserName = username;
        await _userManager.UpdateAsync(user);
      }
      return RedirectToAction("UserSettings");
    }
  }
}