using CourceProject.Data.Repository;
using CourceProject.Models;
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
      /*Debug.WriteLine(sum);
      Debug.WriteLine(ctx.GetFanficRatings(id).Count);
      Debug.WriteLine((decimal)(sum / ctx.GetFanficRatings(id).Count));*/
      (Fanfic , Fandom , List<Chapter>, List<Comment>,List<IdentityUser>,decimal) tuple = (ctx.GetFanfic(id),
                                                                                          ctx.GetFandom(ctx.GetFanfic(id).Fandom_Id), 
                                                                                          ctx.GetChapters(id),ctx.GetFanficComments(id),
                                                                                          _userManager.Users.ToList(),
                                                                                          Math.Round((decimal)sum / (decimal)ctx.GetFanficRatings(id).Count,2));
      return View(tuple);
    }
    [HttpGet]
    public IActionResult ChapterDetails(int id) {
      var chapter = ctx.GetChapter(id);
      ViewBag.Id = ctx.GetFanfic(chapter.Fanfic_Id).User_Id;
      return View(chapter);
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
      ViewBag.Fanfics = ctx.GetAllFanfics();
      ViewBag.Fandoms = ctx.GetAllFandoms();
      return View();
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

    [HttpPost]
    public async Task<ActionResult> AddComment(int fanficId,string text) {
      ctx.AddComment(new Comment { Fanfic_Id = fanficId, Body = text,User_Id = User.Identity.GetUserId() });
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
    public async Task<IActionResult> AddFanfic(Fanfic fanfic) {
      if(ModelState.IsValid) {
        var fan = new Fanfic { Title = fanfic.Title, Description = fanfic.Description, Fandom_Id = fanfic.Fandom_Id, User_Id = User.Identity.GetUserId() };
        ctx.AddFanfic(fan);
        if(await ctx.SaveChangesAsync()) {
          return RedirectToAction("UserFanfics", "Fanfic");
        }
      }
      return RedirectToAction("Index", "Home");
    }
  }
}