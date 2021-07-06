namespace CourceProject.Models {
  public class Chapter {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int Number { get; set; }
    public string ImageUrl { get; set; }
    public string LocalUrl { get; set; }
    public int Fanfic_Id { get; set; }
  }
}
