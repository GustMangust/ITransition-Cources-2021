using CourceProject.Data.Repository;
using CourceProject.Models;
using CourceProject.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult EditFanfic(int id) {
      return View(ctx.GetFanfic(id));
    }
    [HttpGet]
    public IActionResult AllFanfics() {
      return View(ctx.GetAllFanfics());
    }
    [HttpGet]
    public IActionResult UserFanfics() {
      return View(ctx.GetUserFanfics(User.Identity.GetUserId()));
    }
    [HttpPost]
    public async Task<IActionResult> AddFanfic(AddFanficViewModel addFanficViewModel) {
      if(ModelState.IsValid) {
        var fanfic = new Fanfic { Title = addFanficViewModel.Title, Description = addFanficViewModel.Description, Fandom = addFanficViewModel.Fandom,User_Id=User.Identity.GetUserId() };
        ctx.AddFanfic(fanfic);
        if(await ctx.SaveChangesAsync()){
          return RedirectToAction("UserFanfics", "Fanfic");
        }
      }
      return RedirectToAction("Index", "Home");
    }
  }
}