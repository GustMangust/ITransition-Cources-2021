using Microsoft.AspNet.Identity.EntityFramework;

namespace Task4.Models {
  public class ApplicationContext : IdentityDbContext<ApplicationUser> {
    public ApplicationContext() : base("Remote1") { }
    public static ApplicationContext Create() {
      return new ApplicationContext();
    }
  }
}