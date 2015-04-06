using System.Web.Mvc;
using Core.Examples.Services;

namespace Core.Examples.Controllers
{
    public class KendoController : Controller
    {
        public ActionResult Grid()
        {
            var markup = KendoService.GetKendoGrid();
            ViewBag.Markup = markup;
            return View();
        }
    }
}