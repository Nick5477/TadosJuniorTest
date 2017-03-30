using System.Web.Mvc;
using Domain.Commands.Contexts;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public static string DatabasePath { get; protected set; }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            DatabasePath = HttpContext.Server.MapPath("App_data/database.db");
            return View();
        }
    }
}
