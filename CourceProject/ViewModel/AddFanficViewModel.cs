using System.ComponentModel.DataAnnotations;
namespace CourceProject.ViewModel {
    public class AddFanficViewModel {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Fandom { get; set; }
    }
}
