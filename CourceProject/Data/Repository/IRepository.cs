using CourceProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourceProject.Data.Repository {
  public interface IRepository {
    Fanfic GetFanfic(int id);
    List<Fanfic> GetAllFanfics();
    List<Fanfic> GetUserFanfics(string id);
    void AddFanfic(Fanfic fanfic);
    void RemoveFanfic(int id);
    void UpdateFanfic(Fanfic fanfic);
    void AddChapter(Chapter chapter);
    Chapter GetChapter(int id);
    List<Chapter> GetChapters(int fanficId);
    List<Chapter> GetAllChapters();
    void RemoveChapter(int id);
    void UpdateChapter(Chapter chapter);
    Fandom GetFandom(int id);
    List<Fandom> GetAllFandoms();
    void AddTag(Tag tag);
    Tag GetTag(int tagId);
    Tag GetTagByName(string name);
    List<Tag> GetAllTags();
    void AddFanficTag(FanficTag fanficTag);
    FanficTag GetFanficTag(int fanficTagId);
    List<FanficTag> GetFanficTags();
    Task<bool> SaveChangesAsync();
    List<Comment> GetFanficComments(int fanficId);
    List<Comment> GetAllComments();
    void AddComment(Comment comment);
    List<Rating> GetFanficRatings(int fanficId);
    Rating GetRating(int fanficId, string userId);
    List<Rating> GetRatings();
    void AddRating(Rating rating);
    List<Like> GetChapterLikes(int chapterId);
    Like GetLike(int chapterId, string userId);
    void AddLike(Like like);
    List<Bookmark> GetBookmarks(string userId);
    void AddBookmark(Bookmark bookmark);
    void RemoveBookmark(int bookmarkId);
    Bookmark GetBookmark(string userId, int fanficId);
    List<Preference> GetPreferences(string userId);
    void AddPreference(Preference preference);
    void RemovePreference(int preferenceId);
    Preference GetPreference(string userId, int fandomId);
    Preference GetPreference(int preferenceId);

  }
}
