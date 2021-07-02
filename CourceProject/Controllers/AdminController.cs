using CourceProject.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Controllers {
  public class AdminController : Controller {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private IRepository ctx;
    public AdminController(IRepository repo, UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager) {
      _userManager = userManager;
      _signInManager = signInManager;
      ctx = repo;
      _roleManager = roleManager;
    }
    [HttpGet]
    public IActionResult AdminPage() {
      return View(_userManager.Users.ToList());
    }
    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId) {
      var user = await _userManager.FindByIdAsync(userId);
      var result = await _userManager.DeleteAsync(user);
      return RedirectToAction("AdminPage");
    }
    [HttpPost]
    public async Task<IActionResult> BlockUser(string userId) {
      var user = await _userManager.FindByIdAsync(userId);
      var result = await _userManager.SetLockoutEndDateAsync(user, new DateTime().AddYears(5000));
      return RedirectToAction("AdminPage");
    }
    [HttpPost]
    public async Task<IActionResult> UnblockUser(string userId) {
      var user = await _userManager.FindByIdAsync(userId);
      var result = await _userManager.SetLockoutEndDateAsync(user, null);
      return RedirectToAction("AdminPage");
    }
    [HttpPost]
    public async Task<IActionResult> GiveAdmin(string userId) {
      var user = await _userManager.FindByIdAsync(userId);
      await _userManager.AddToRoleAsync(user, "Admin");
      await _signInManager.SignInAsync(user,false);
      return RedirectToAction("AdminPage");
    }
    [HttpGet]
    public IActionResult AddFanficAdmin(string userId) {
      TempData["UserId"] = userId;
      return RedirectToAction("AddFanfic", "Fanfic");
    }
  }
}