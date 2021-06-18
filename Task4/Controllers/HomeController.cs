using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Task4.Models;

namespace Task4.Controllers {
  public class HomeController : Controller {
    private ApplicationUserManager UserManager {
      get {
        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
    }
    private IAuthenticationManager AuthenticationManager {
      get {
        return HttpContext.GetOwinContext().Authentication;
      }
    }
    public async Task<ActionResult> Index() {
      ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
      if(currentUser!=null && User.Identity.IsAuthenticated && currentUser.Status=="Active") {
        List<ApplicationUser> list = new List<ApplicationUser>(UserManager.Users);
        foreach(var user in list) {
          if(user.LockoutEndDateUtc != null) {
            user.Status = "Banned";
            IdentityResult result1 = await UserManager.UpdateAsync(user);
          }
        }
        ViewBag.Users = UserManager.Users;
        return View();
      }
      return View("About");
    }
    [HttpPost]
    public async Task<ActionResult> MyAction(string[] checkbox, string action) {
      bool flag = false;
      ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
      if(currentUser != null && currentUser.Status == "Active") {
        if(checkbox != null) {
          foreach(string value in checkbox) {
            ApplicationUser user = await UserManager.FindByIdAsync(value);
            if(action == "block") {
              DateTime dateTime = new DateTime();
              user.LockoutEndDateUtc = dateTime.AddYears(3000);
              user.Status = "Blocked";
              IdentityResult result = await UserManager.UpdateAsync(user);
              if(User.Identity.GetUserId() == value) {
                flag = true;
              }
            }
            if(action == "unblock") {
              user.LockoutEndDateUtc = null;
              user.Status = "Active";
              IdentityResult result = await UserManager.UpdateAsync(user);
            }
            if(action == "delete") {
              IdentityResult result = await UserManager.DeleteAsync(user);
              if(User.Identity.GetUserId() == value) {
                flag = true;
              }
            }
          }
        }
      } else {
        return RedirectToAction("SignOut", "Home");
      }
      if(flag) {
        AuthenticationManager.SignOut();
        return RedirectToAction("SignOut", "Home");
      } else {
        return RedirectToAction("Index", "Home");
      }
    }
    public ActionResult SignOut() {
      AuthenticationManager.SignOut();
      return RedirectToAction("Login", "Account");
    }
    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}