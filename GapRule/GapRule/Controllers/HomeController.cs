using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GapRule.Services;

namespace GapRule.Controllers
{
    public class HomeController : Controller
    {
        IGapRuleService _service = new GapRuleService();
        // GET: Home
        public ActionResult Index()
        {
            _service.ParseJsonFile();
            return View();
        }
    }
}