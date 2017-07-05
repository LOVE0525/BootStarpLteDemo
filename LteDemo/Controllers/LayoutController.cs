using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LteDemo.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// from表单demo
        /// </summary>
        /// <returns></returns>
        public ActionResult From()
        {
            return View();
        }
    }
}