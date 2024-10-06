using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websitebansach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.UI.WebControls;

namespace Websitebansach.Controllers
{
	public class QuanLiSanPhamController : Controller
	{
		// GET: QuanLiSanPham
		QuanLyBanSachEntities db = new QuanLyBanSachEntities();
		public ActionResult Index(int? page)
		{

			int pageNumber = (page ?? 1);
			int pageSize = 10;
			return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
		}
		//thêm mới
		[HttpGet]
		public ActionResult ThemMoi()
		{
			//Đưa dữ liệu vào
			ViewBag.MaChude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
			ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
			return View();
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
		{

			//Đưa dữ liệu vào
			ViewBag.MaChude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
			ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

			//thêm dữ liệu vào
			if (ModelState.IsValid)
			{
				//Lưu tên của file
				var fileName = Path.GetFileName(fileUpload.FileName);
				//Lưu đường dẫn của file
				var path = Path.Combine(Server.MapPath("~/HinhAnh"), fileName);
				if (System.IO.File.Exists(path))
				{
					ViewBag.ThongBao = "Hình ảnh đã tồn tại";
				}
				else
				{
					fileUpload.SaveAs(path);
				}
				sach.AnhBia = fileUpload.FileName;
				db.Saches.Add(sach);
				db.SaveChanges();
			}

			return View();
		}

		//chỉnh sửa
		[HttpGet]
		public ActionResult ChinhSua(int MaSach)
		{
			Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
			if (sach == null)
			{
				Response.StatusCode = 404;
				return null;
			}
			//Đưa dữ liệu vào
			ViewBag.MaChude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
			ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
			return View(sach);
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult ChinhSua(Sach sach, FormCollection f)
		{
			//thêm vào cở sở dữ liệu


			if (ModelState.IsValid)
			{
				//Thực hiện cập nhật model
				db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
			}
			//Đưa dữ liệu vào
			ViewBag.MaChude = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
			ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

			return RedirectToAction("Index");
		}
		public ActionResult HienThi(int MaSach)
		{
			Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
			if (sach == null)
			{
				Response.StatusCode = 404;
				return null;
			}

			return View(sach);
		}
		//xóa sản phẩm 
		[HttpGet]
		public ActionResult Xoa(int MaSach)
		{
			Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
			if (sach == null)
			{
				Response.StatusCode = 404;
				return null;
			}

			return View(sach);
		}
		[HttpPost, ActionName("Xoa")]
		public ActionResult XacNhanXoa(int MaSach)
		{
			Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
			if (sach == null)
			{
				Response.StatusCode = 404;
				return null;
			}
			db.Saches.Remove(sach);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}

}