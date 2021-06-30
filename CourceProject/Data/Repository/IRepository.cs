﻿using CourceProject.Models;
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
    Tag GetTag(int id);
    List<Tag> GetAllTags();
    Task<bool> SaveChangesAsync();
    List<Comment> GetFanficComments(int fanficId);
    void AddComment(Comment comment);
    List<Rating> GetFanficRatings(int fanficId);
    Rating GetRating(int fanficId, string userId);
    void AddRating(Rating rating);
    List<Like> GetChapterLikes(int chapterId);
    Like GetLike(int chapterId, string userId);
    void AddLike(Like like);
  }
}
