
using System.Web.Mvc;

namespace AdminPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("login", "Account");
        }
    }
}