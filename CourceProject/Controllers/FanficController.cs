using CourceProject.Data.Repository;
using CourceProject.Models;
using CourceProject.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Controllers {
  public class FanficController : Controller {
    private IRepository ctx;
    public FanficController(IRepository repo) {
      ctx = repo;

    }
    [HttpGet]
    public IActionResult AddFanfic() {
      return View();
    }
    [HttpGet]
    public IActionResult AddChapter(int id) {
      Chapter chapter = new Chapter { Fanfic_Id = id };
      return View(chapter);
    }
    [HttpGet]
    public IActionResult FanficDetails(int id) {
      (Fanfic fanfic, List<Chapter> chapters) tuple = (ctx.GetFanfic(id), ctx.GetChapters(id));
      return View(tuple);
    }
    [HttpGet]
    public IActionResult ChapterDetails(int id) {
      return View(ctx.GetChapter(id));
    }
    [HttpGet]
    public IActionResult EditFanfic(int id) {
      return View(ctx.GetFanfic(id));
    }
    [HttpGet]
    public IActionResult EditChapter(int id) {
      return View(ctx.GetChapter(id));
    }
    [HttpGet]
    public IActionResult AllFanfics() {
      ViewBag.Fanfics = ctx.GetAllFanfics();
      ViewBag.Chapters = ctx.GetAllChapters();
      return View();
    }
    [HttpGet]
    public IActionResult UserFanfics() {
      return View(ctx.GetUserFanfics(User.Identity.GetUserId()));
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
    public async Task<IActionResult> EditChapter(Chapter chapter) {
      ctx.UpdateChapter(chapter);
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic", new { id = chapter.Fanfic_Id });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<IActionResult> AddChapter(Chapter c) {
      var chapter = new Chapter { Title = c.Title, Body = c.Body, Fanfic_Id = c.Fanfic_Id,Number= ctx.GetAllChapters().Where(x => x.Fanfic_Id == c.Fanfic_Id).ToList().Count + 1 };
      ctx.AddChapter(chapter);
      if(await ctx.SaveChangesAsync()) {
        return RedirectToAction("FanficDetails", "Fanfic",new { id = chapter.Fanfic_Id });
      }
      return Content("Fail");
    }
    [HttpPost]
    public async Task<IActionResult> AddFanfic(AddFanficViewModel addFanficViewModel) {
      if(ModelState.IsValid) {
        var fanfic = new Fanfic { Title = addFanficViewModel.Title, Description = addFanficViewModel.Description, Fandom = addFanficViewModel.Fandom, User_Id = User.Identity.GetUserId() };
        ctx.AddFanfic(fanfic);
        if(await ctx.SaveChangesAsync()) {
          return RedirectToAction("UserFanfics", "Fanfic");
        }
      }
      return RedirectToAction("Index", "Home");
    }
  }
}