using System;

namespace CourceProject.Models {
  public class Fanfic {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Fandom_Id { get; set; }
    public string User_Id { get; set; }
    public DateTime UpdateDate { get; set; }
  }

}
