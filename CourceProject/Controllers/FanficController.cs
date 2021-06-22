using CourceProject.Data.Repository;
using CourceProject.Models;
using CourceProject.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
    [HttpPost]
    public async Task<IActionResult> AddFanfic(AddFanficViewModel addFanficViewModel) {
      if(ModelState.IsValid) {
        var fanfic = new Fanfic { Title = addFanficViewModel.Title, Description = addFanficViewModel.Description, Fandom = addFanficViewModel.Fandom,User_Id=User.Identity.GetUserId() };
        ctx.AddFanfic(fanfic);
        if(await ctx.SaveChangesAsync()){
          Debug.WriteLine(fanfic.Id);
          return RedirectToAction("Index", "Home");
        } else {
          Debug.WriteLine("FUCK YOU");
        }

      }
      return RedirectToAction("Index", "Home");
    }
  }
}