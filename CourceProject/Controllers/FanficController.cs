using CourceProject.Data.Repository;
using CourceProject.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Controllers {
  public class FanficController : Controller {
    private IRepository ctx;
    private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
    public FanficController(IRepository repo, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager) {
      _userManager = userManager;
      ctx = repo;
    }
    [HttpGet]
    public IActionResult AddFanfic() {
      if(TempData["UserId"] != null) {
         ViewBag.UserId = TempData["UserId"].ToString();
      }
      ViewData["Id"] = new SelectList(ctx.GetAllFandoms(), "Id", "Name");
      return View();
    }
    [HttpGet]
    public IActionResult AddChapter(int id) {
      Chapter chapter = new Chapter { Fanfic_Id = id };
      return View(chapter);
    }
    [HttpGet]
    public IActionResult FanficDetails(int id) {
      int sum = 0;
      foreach(Rating rating in ctx.GetFanficRatings(id)) {
        sum += rating.Mark;
      }
      decimal averageRating = 0;
      try {
        averageRating = Math.Round((decimal)sum / (decimal)ctx.GetFanficRatings(id).Count, 2);
      }
      catch {
        averageRating = 0;
      }
      (Fanfic, Fandom, List<Chapter>, List<Comment>, List<IdentityUser>, decimal) tuple = (ctx.GetFanfic(id),
                                                                                          ctx.GetFandom(ctx.GetFanfic(id).Fandom_Id),
                                                                                          ctx.GetChapters(id), ctx.GetFanficComments(id),
                                                                                          _userManager.Users.ToList(),
                                                                                          averageRating);
      return View(tuple);
    }
    [HttpGet]
    public IActionResult ChapterDetails(int chapterId) {
      (Chapter, int) data = (ctx.GetChapter(chapterId), ctx.GetChapterLikes(chapterId).Count);
      ViewBag.Id = ctx.GetFanfic(ctx.GetChapter(chapterId).Fanfic_Id).User_Id;
      return View(data);
    }
    [HttpGet]
    public IActionResult EditFanfic(int id) {
      ViewData["Id"] = new SelectList(ctx.GetAllFandoms(), "Id", "Name");
      return View(ctx.GetFanfic(id));
    }
    [HttpGet]
    public IActionResult EditChapter(int id) {
      Debug.WriteLine(id);
      return View(ctx.GetChapter(id));
    }
    [HttpGet]
    public IActionResult AllFanfics() {
      if(ctx.GetPreferences(User.Identity.GetUserId()).Count == 0 && User.IsInRole("User")) {
        return RedirectToAction("SetPreferences", "Account");
      }
      ViewBag.Fanfics = ctx.GetAllFanfics();
      ViewBag.Fandoms = ctx.GetAllFandoms();
      return View();
    }
    [HttpGet]
    public IActionResult UserBookmarks() {
      List<Fanfic> fanfics = new List<Fanfic>();
      foreach(Bookmark b in ctx.GetBookmarks(User.Identity.GetUserId())) {
        var fanfic = ctx.GetFanfic(b.FanficId);
        if(fanfic != null) {
          fanfics.Add(fanfic);
        }
      }
      (List<Fanfic>, List<Fandom>) data = (fanfics, ctx.GetAllFandoms());
      return View(data);
    }
    [HttpGet]
    public IActionResult UserFanfics(string sortBy = "") {
      (List<Fanfic> Fanfics, List<Fandom> Fandoms) tuple;
      switch(sortBy) {
        case "titleAsc":
          tuple = (ctx.GetUserFanfics(User.Identity.GetUserId()).OrderBy(x => x.Title).ToList(), ctx.GetAllFandoms());
          break;
        case "titleDesc":
          tuple = (ctx.GetUserFanfics(User.Identity.GetUserId()).OrderByDescending(x => x.Title).ToList(), ctx.GetAllFandoms());
          break;
        default:
          tuple = (ctx.GetUserFanfics(User.Identity.GetUserId()), ctx.GetAllFandoms());
          break;
      }
      return View(tuple);
    }
    [HttpGet]
    public IActionResult ChapterNavigate(int chapterId, string param) {
      var currentChapter = ctx.GetChapter(chapterId);
      Chapter chapter;
      if(param == "Back") {
        chapter = ctx.GetChapters(currentChapter.Fanfic_Id).FirstOrDefault(x => x.Number == currentChapter.Number - 1);
      } else {
        chapter = ctx.GetChapters(currentChapter.Fanfic_Id).FirstOrDefault(x => x.Number == currentChapter.Number + 1);
      }
      if(chapter == null) {
        return RedirectToAction("ChapterDetails", "Fanfic", new { chapterId = chapterId });
      }
      return RedirectToAction("ChapterDetails", "Fanfic", new { chapterId = chapter.Id });
    }
    [HttpPost]
    public async Task<ActionResult> AddLike(int chapterId, string userId) {
      ctx.AddLike(new Like { ChapterId = chapterId, UserId = userId });
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("ChapterDetails", "Fanfic", new { chapterId = chapterId });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<ActionResult> AddComment(int fanficId, string text) {
      ctx.AddComment(new Comment { Fanfic_Id = fanficId, Body = text, User_Id = User.Identity.GetUserId() });
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = fanficId });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<ActionResult> AddRating(int fanficId, string mark) {
      ctx.AddRating(new Rating { FanficId = fanficId, Mark = Convert.ToInt32(mark), UserId = User.Identity.GetUserId() });
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = fanficId });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<ActionResult> RemoveChapter(int id) {
      Chapter removedChapter = ctx.GetChapter(id);
      List<Chapter> list = ctx.GetChapters(removedChapter.Fanfic_Id);
      for(int i = 0; i < list.Count; i++) {
        if(i > list.IndexOf(removedChapter)) {
          list[i].Number--;
          ctx.UpdateChapter(list[i]);
        }
      }
      ctx.RemoveChapter(id);
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = removedChapter.Fanfic_Id });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<IActionResult> EditFanfic(Fanfic fanfic) {
      ctx.UpdateFanfic(fanfic);
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = fanfic.Id });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<IActionResult> ChapterUp(int id) {
      Chapter chapter = ctx.GetChapter(id);
      Chapter previousChapter = ctx.GetAllChapters().FirstOrDefault(x => x.Number == chapter.Number - 1);
      if(previousChapter != null) {
        previousChapter.Number = chapter.Number;
        chapter.Number = chapter.Number - 1;
        ctx.UpdateChapter(previousChapter);
        ctx.UpdateChapter(chapter);
        if(await ctx.SaveChangesAsync()) {
          return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
        }
        return Content("Fail");
      } else {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
      }
    }
    public async Task<IActionResult> ChapterDown(int id) {
      Chapter chapter = ctx.GetChapter(id);
      Chapter previousChapter = ctx.GetAllChapters().FirstOrDefault(x => x.Number == chapter.Number + 1);
      if(previousChapter != null) {
        previousChapter.Number = chapter.Number;
        chapter.Number = chapter.Number + 1;
        ctx.UpdateChapter(previousChapter);
        ctx.UpdateChapter(chapter);
        if(await ctx.SaveChangesAsync()) {
          return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
        }
        return Content("Fail");
      } else {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
      }
    }
    [HttpPost]
    public async Task<IActionResult> EditChapter(Chapter chapter) {
      ctx.UpdateChapter(chapter);
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<IActionResult> AddChapter(Chapter c) {
      var chapter = new Chapter { Title = c.Title, Body = c.Body, Fanfic_Id = c.Fanfic_Id, Number = ctx.GetAllChapters().Where(x => x.Fanfic_Id == c.Fanfic_Id).ToList().Count + 1 };
      ctx.AddChapter(chapter);
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<IActionResult> AddFanfic(Fanfic fanfic, string userId) {
      if(ModelState.IsValid) {
        Debug.WriteLine(userId);
        if(String.IsNullOrEmpty(userId)) {

          userId = User.Identity.GetUserId();
        }
        var fan = new Fanfic { Title = fanfic.Title, Description = fanfic.Description, Fandom_Id = fanfic.Fandom_Id, User_Id = userId };
        ctx.AddFanfic(fan);
        if(await ctx.SaveChangesAsync()) {
          return RedirectToAction("UserFanfics", "Fanfic");
        }
      }
      return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> AddFanficToFavourite(int fanficId) {
      var fanfic = ctx.GetFanfic(fanficId);
      if(fanfic != null) {
        ctx.AddBookmark(new Bookmark { FanficId = fanficId, UserId = User.Identity.GetUserId() });
      }
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("UserBookmarks", "Fanfic");
      }
      return RedirectToAction("AllFanfics", "Fanfic");
    }
    [HttpPost]
    public async Task<IActionResult> RemoveBookmark(int fanficId) {
      var fanfic = ctx.GetFanfic(fanficId);
      var bookmark = ctx.GetBookmark(User.Identity.GetUserId(), fanficId);
      if(fanfic != null && bookmark != null) {
        ctx.RemoveBookmark(bookmark.Id);
      }
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("UserBookmarks", "Fanfic");
      }
      return RedirectToAction("UserBookmarks", "Fanfic");
    }
  }
}