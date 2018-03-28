namespace TestMvc.Controllers
{
    using System.Web.Mvc;

    public class CatsController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}