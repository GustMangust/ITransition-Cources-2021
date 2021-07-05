using System.ComponentModel.DataAnnotations;
namespace CourceProject.ViewModel {
  public class AddFanficViewModel {
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int FandomId { get; set; }
    public string Tags { get; set; }
  }
}
