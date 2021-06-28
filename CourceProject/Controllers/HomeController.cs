using CourceProject.Models;
using CourceProject.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Westwind.AspNetCore.Markdown;

namespace CourceProject.Controllers {
  public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
      _logger = logger;
    }

    public IActionResult Index(string a = "") {
      string markdownText = "## This is a title of Markdown file ";
      string b = a.Trim();
      string htmltext = MarkDownParser.Parse(a);
      htmltext += MarkDownParser.Parse("");// for new line
      htmltext+= MarkDownParser.Parse(a);
      /*htmltext += MarkDownParser.Parse("__Strong text__");// for new line

      htmltext += MarkDownParser.Parse("  ");// for new line
      htmltext += MarkDownParser.Parse("* This is a bullet point");// bullet point*/
      /*ViewBag.HTMLText = htmltext;
      ViewBag.Text = a;*/
      ViewBag.Text = htmltext;
      return View();
    }
    [HttpPost]
    public IActionResult IndexC(string b) {
      Debug.WriteLine(b);
      return RedirectToAction("Index", "Home", new { a = b });
    }
    public IActionResult Privacy() {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
