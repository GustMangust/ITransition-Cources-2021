using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Task4.Models;

namespace Task4.Controllers {
  public class AccountController : Controller {
    private ApplicationUserManager UserManager {
      get {
        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
    }
    public ActionResult Register() {
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Register(RegisterModel model) {
      if(ModelState.IsValid) {
        ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        user.DateOfRegistration = DateTime.Now;
        user.Status = "Active";
        IdentityResult result1 = await UserManager.UpdateAsync(user);
        if(result.Succeeded) {
          return RedirectToAction("Login", "Account");
        } else {
          foreach(string error in result.Errors) {
            ModelState.AddModelError("", error);
          }
        }
      }
      return View(model);
    }
    private IAuthenticationManager AuthenticationManager {
      get {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    public ActionResult Login(string returnUrl) {
      ViewBag.returnUrl = returnUrl;
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginModel model, string returnUrl) {
      if(ModelState.IsValid) {
        ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
        if(user == null) {
          ModelState.AddModelError("", "Неверный логин или пароль.");
        } else if(user.LockoutEndDateUtc == null) {
          ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
          user.LastLoginTime = DateTime.Now;
          IdentityResult result = await UserManager.UpdateAsync(user);
          AuthenticationManager.SignOut();
          AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
          if(String.IsNullOrEmpty(returnUrl)) {
            return RedirectToAction("Index", "Home");
          }
          return Redirect(returnUrl);
        } else {
          ModelState.AddModelError("", "You were blocked.");
        }
      }

      ViewBag.returnUrl = returnUrl;
      return View(model);
    }
    public ActionResult Logout() {
      AuthenticationManager.SignOut();
      return RedirectToAction("Login");
    }
    [HttpGet]
    public ActionResult Delete() {
      return View();
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed() {
      ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
      if(user != null) {
        IdentityResult result = await UserManager.DeleteAsync(user);
        if(result.Succeeded) {
          return RedirectToAction("Logout", "Account");
        }
      }
      return RedirectToAction("Index", "Home");
    }

    public async Task<ActionResult> Edit() {
      ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
      if(user != null) {
        EditModel model = new EditModel { Name = user.Name };
        return View(model);
      }
      return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    public async Task<ActionResult> Edit(EditModel model) {
      ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
      if(user != null) {
        user.Name = model.Name;
        IdentityResult result = await UserManager.UpdateAsync(user);
        if(result.Succeeded) {
          return RedirectToAction("Index", "Home");
        } else {
          ModelState.AddModelError("", "Что-то пошло не так");
        }
      } else {
        ModelState.AddModelError("", "Пользователь не найден");
      }

      return View(model);
    }
  }
}