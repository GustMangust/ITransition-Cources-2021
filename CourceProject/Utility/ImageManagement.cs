using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourceProject.Models;

namespace CourceProject.Utility {
  public static class ImageManagement {
    public static Account account;
    public static Cloudinary cloudinary;
    static ImageManagement() {
      Account account = new Account("gustmangust",
                                    "718477222848953",
                                    "7PksEMQd1XJkoIc1OwdQ-Wwq9ak");
      cloudinary = new Cloudinary(account);
    }
    public static string AddImageToChapter(Chapter chapter) {
      if(!string.IsNullOrEmpty(chapter.LocalUrl)) {
        var uploadParams = new ImageUploadParams() {
          File = new FileDescription(chapter.LocalUrl),
          PublicId = $"{chapter.Id}{chapter.Fanfic_Id}image"
        };
        var uploadResult = cloudinary.Upload(uploadParams);
        return uploadResult.Url.ToString();
      }
      return null;
    }
    public static string EditImageToChapter(Chapter chapter) {
      if(!string.IsNullOrEmpty(chapter.ImageUrl)) {
        cloudinary.DeleteResources($"{chapter.Id}{chapter.Fanfic_Id}image");
      }
      if(!string.IsNullOrEmpty(chapter.LocalUrl)) {
        var uploadParams = new ImageUploadParams() {
          File = new FileDescription(chapter.LocalUrl),
          PublicId = $"{chapter.Id}{chapter.Fanfic_Id}image"
        };
        var uploadResult = cloudinary.Upload(uploadParams);
        return uploadResult.Url.ToString();
      } 
      return null;
    }
  }
}
