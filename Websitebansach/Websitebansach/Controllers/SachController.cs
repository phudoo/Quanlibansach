﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websitebansach.Models;
namespace Websitebansach.Controllers
{
	
    public class SachController : Controller
    {
		QuanLyBanSachEntities db = new QuanLyBanSachEntities();
		// GET: Sach
		public PartialViewResult SachmenuPartial()
		{
			var listsach = db.Saches.Take(3).ToList();
			return PartialView(listsach);
		}
		public ViewResult XemChiTiet(int MaSach=0)
		{
			Sach sach = db.Saches.SingleOrDefault(n=>n.MaSach == MaSach);
			if (sach == null)
			{
				Response.StatusCode = 404;
				return null;
			}
			ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
			ViewBag.NhaXuatBan = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
			return View(sach);
		}
	}
}