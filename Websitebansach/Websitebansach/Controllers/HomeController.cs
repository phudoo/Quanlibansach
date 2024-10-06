using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websitebansach.Models;

namespace Websitebansach.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
		public ActionResult Index(int? page)
		{
			int pageSize = 9;
			int pageNumber = (page ?? 1);
			return View(db.Saches.Where(n => n.Moi == 1).OrderBy(n => n.GiaBan).ToPagedList(pageNumber, pageSize));
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