using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace AdminPresentation.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MagzinesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Magzines.Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magzine magzine = db.Magzines.Find(id);
            if (magzine == null)
            {
                return HttpNotFound();
            }
            return View(magzine);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Magzine magzine, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/magzine/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    magzine.ImageUrl = newFilenameUrl;
                }


                if (fileUploadFile != null)
                {
                    string filename = Path.GetFileName(fileUploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/magzine/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadFile.SaveAs(physicalFilename);

                    magzine.FileUrl = newFilenameUrl;
                }
                #endregion
                magzine.LikeCount = 0;
                magzine.CommentCount = 0;
                magzine.IsDeleted = false;
                magzine.CreationDate = DateTime.Now;
                magzine.Id = Guid.NewGuid();
                db.Magzines.Add(magzine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magzine);
        }

        // GET: Magzines/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magzine magzine = db.Magzines.Find(id);
            if (magzine == null)
            {
                return HttpNotFound();
            }
            return View(magzine);
        }

        // POST: Magzines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Magzine magzine, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/magzine/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    magzine.ImageUrl = newFilenameUrl;
                }


                if (fileUploadFile != null)
                {
                    string filename = Path.GetFileName(fileUploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/magzine/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadFile.SaveAs(physicalFilename);

                    magzine.FileUrl = newFilenameUrl;
                }
                #endregion
                magzine.IsDeleted = false;
                magzine.LastModifiedDate = DateTime.Now;
                db.Entry(magzine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(magzine);
        }

        // GET: Magzines/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magzine magzine = db.Magzines.Find(id);
            if (magzine == null)
            {
                return HttpNotFound();
            }
            return View(magzine);
        }

        // POST: Magzines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Magzine magzine = db.Magzines.Find(id);
            magzine.IsDeleted = true;
            magzine.DeletionDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index");
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
