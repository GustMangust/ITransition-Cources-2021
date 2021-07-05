using System.ComponentModel.DataAnnotations;

namespace CourceProject.ViewModel {
  public class RegisterViewModel {
    [Required]
    [Display(Name = "Имя пользователя")]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Повторите пароль")]
    [Compare("Password", ErrorMessage = "Введённые пароли не совпадают")]
    public string ConfirmPassword { get; set; }
  }
}
