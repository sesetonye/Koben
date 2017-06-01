using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Koben.DAL;
using Koben.Models;
using System.IO;

namespace Koben.Controllers
{
    public class StaffsController : Controller
    {
        private KobenContext db = new KobenContext();


        // GET: Staffs
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            TempData["Error"] = "";
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {


                if (image != null)
                {
                    if (image.ContentType != "image/png" && image.ContentType != "image/jpg" && image.ContentType != "image/jpeg")
                    {
                        TempData["Error"] = "Invalid Image type. Allowed files png,jpg";
                        return View(staff);
                    }
                    staff.Image = this.uploadimage(image);
                }
                else
                {

                    staff.Image = HttpContext.Request.Url.Authority + "/Images/Default.jpeg";
                }
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            TempData["Error"] = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Title,Phone,Image")] Staff staff, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image.ContentType != "image/png" && image.ContentType != "image/jpg" && image.ContentType != "image/jpeg")
                {
                    TempData["Error"] = "Invalid Image type. Allowed files png,jpg";
                    return View(staff);
                }
                staff.Image = this.uploadimage(image);
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public JsonResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Staff staff = db.Staffs.Find(id);
            //if (staff == null)
            //{
            //    return HttpNotFound();
            //}
            var JsonResult = Json(staff, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private string uploadimage(HttpPostedFileBase image)
        {

            string[] ext = image.ContentType.ToString().Split('/');
            var filename = DateTime.Now.ToString("yyyyMMddHHmmss") +"." + ext[1];
            var filePathOriginal = Server.MapPath("/Images");
            string savedFileName = Path.Combine(filePathOriginal, filename);
            string Imagepath =/* HttpContext.Request.Url.Authority +*/ "../Images/" + filename;
            image.SaveAs(savedFileName);
            return Imagepath;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
