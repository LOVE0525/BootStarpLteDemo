using LteDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LteDemo.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            Models.Pager<UserModels> pager = new Models.Pager<UserModels>();
            pager.CurrentPageIndex = 1;
            pager.PageSize = 10;
            pager.TotalItemCount = 100000;
            pager.ShowItemNum = 20;
          
            return View(pager);
        }
    }
}