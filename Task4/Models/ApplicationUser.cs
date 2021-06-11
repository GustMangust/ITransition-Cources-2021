using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Task4.Models {
  public class ApplicationUser : IdentityUser {
    public string Name { get; set; }
    public DateTime? DateOfRegistration { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public string Status { get; set; }
    public ApplicationUser() {
    }
  }
}