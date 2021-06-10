using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public ActionResult Index() {
      string result = "Вы не авторизованы";
      if(User.Identity.IsAuthenticated) {
        List<ApplicationUser> list = new List<ApplicationUser>(UserManager.Users);
        foreach(var a in list) {
          MessageBox.Show(a.Email);
        }
        result = "Ваш логин: " + User.Identity.Name;
        return View();
      }
      return View();
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