using System.ComponentModel.DataAnnotations;

namespace CourceProject.ViewModel {
  public class LoginViewModel {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Запомнить")]
    public bool RememberMe { get; set; }
    public object ReturnUrl { get; internal set; }
  }
}
