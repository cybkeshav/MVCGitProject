using Business.Contracts;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCGITProject.Controllers
{
    public class HomeController : Controller
    {
        IGithub _gitHub;

        //*** Open it if we use a DI frameworks like Ninject, Unity, Autofac,
        //public HomeController(IGithub gitHub)
        //{
        //    _gitHub = gitHub;
        //}

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("GetData")]
        //[Route()]
        public async Task<ActionResult> GetGithubRepos(string SearchQuery)
        {
            _gitHub = new Github();
            var result = await _gitHub.GetGithubRepos(SearchQuery);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}