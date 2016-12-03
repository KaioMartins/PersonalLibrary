using PersonalLibrary.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalLibrary.Controllers
{
    public class MultimidiaController : Controller
    {
        // GET: Multimidia
        public ActionResult Index()
        {
            return View();
        }

        [AuthFilter]
        public ActionResult Video()
        {
            return View();
        }

        [AuthFilter]
        public ActionResult Audio()
        {
            return View();
        }
    }
}