using CourceProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Data.Repository {
  public class Repository : IRepository {
    private AppDbContext ctx;
    public Repository(AppDbContext ctx) {
      this.ctx = ctx;
    }
    public void AddFanfic(Fanfic work) {
      ctx.Fanfics.Add(work);
    }
    public List<Fanfic> GetAllFanfics() {
      return ctx.Fanfics.ToList();
    }
    public Fanfic GetFanfic(int id) {
      return ctx.Fanfics.FirstOrDefault(x => x.Id == id);
    }
    public void RemoveFanfic(int id) {
      ctx.Remove(GetFanfic(id));
    }
    public void UpdateFanfic(Fanfic work) {
      ctx.Update(work);
    }
    public async Task<bool> SaveChangesAsync() {
      if(await ctx.SaveChangesAsync() > 0) {
        return true;
      }
      return false;
    }
    public List<Fanfic> GetUserFanfics(string id) {
      return new List<Fanfic>(ctx.Fanfics.Where(x => x.User_Id == id));
    }
    public void AddChapter(Chapter chapter) {
      ctx.Chapters.Add(chapter);
    }
    public void RemoveChapter(int id) {
      ctx.Remove(GetChapter(id));
    }
    public void UpdateChapter(Chapter chapter) {
      ctx.Update(chapter);
    }
    public List<Chapter> GetChapters(int fanficId) {
      return ctx.Chapters.Where(x => x.Fanfic_Id == fanficId).OrderBy(x => x.Number).ToList();
    }
    public Chapter GetChapter(int id) {
      return ctx.Chapters.FirstOrDefault(x => x.Id == id);
    }
    public List<Chapter> GetAllChapters() {
      return ctx.Chapters.ToList();
    }

    public Fandom GetFandom(int id) {
      return ctx.Fandoms.FirstOrDefault(x => x.Id == id);
    }

    public List<Fandom> GetAllFandoms() {
      return ctx.Fandoms.ToList();
    }

    public Tag GetTag(int id) {
      return ctx.Tags.FirstOrDefault(x => x.Id == id);
    }

    public List<Tag> GetAllTags() {
      return ctx.Tags.ToList();
    }

    public List<Comment> GetFanficComments(int fanficId) {
      return ctx.Comments.Where(x => x.Fanfic_Id == fanficId).ToList();
    }
    public void AddComment(Comment comment) {
      ctx.Comments.Add(comment);
    }

    public List<Rating> GetFanficRatings(int fanficId) {
      return ctx.Ratings.Where(x => x.FanficId == fanficId).ToList();
    }

    public Rating GetRating(int fanficId,string userId) {
      return ctx.Ratings.FirstOrDefault(x => x.FanficId == fanficId && x.UserId == userId);
    }

    public void AddRating(Rating rating) {
      var rate = GetRating(rating.FanficId, rating.UserId);
      if(rate != null) {
        ctx.Update(rating);
      } else {
        ctx.Ratings.Add(rating);
      }
    }
  }
}
