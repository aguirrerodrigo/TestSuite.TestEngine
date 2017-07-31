using System.Web.Mvc;

namespace TestSuite.TestManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult ResourceNotFound()
        {
            return View();
        }
    }
}