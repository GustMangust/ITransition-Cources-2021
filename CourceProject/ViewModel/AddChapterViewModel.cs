using System.ComponentModel.DataAnnotations;

namespace CourceProject.ViewModel {
  public class AddChapterViewModel {
    [Required]
    public int Fanfic_Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
  }
}
