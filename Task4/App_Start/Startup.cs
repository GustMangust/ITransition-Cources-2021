using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task4.Models;

[assembly: OwinStartup(typeof(Task4.App_Start.Startup))]
namespace Task4.App_Start {
  public class Startup {
    public void Configuration(IAppBuilder app) {
      app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
      app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
      app.UseCookieAuthentication(new CookieAuthenticationOptions {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login"),
      });
    }
  }
}