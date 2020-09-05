using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace AdminPresentation.Controllers
{
    public class ArAssetsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ArAssets
        public ActionResult Index()
        {
            var arAssets = db.ArAssets.Include(a => a.Magzine).Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate);
            return View(arAssets.ToList());
        }

        // GET: ArAssets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArAsset arAsset = db.ArAssets.Find(id);
            if (arAsset == null)
            {
                return HttpNotFound();
            }
            return View(arAsset);
        }

        // GET: ArAssets/Create
        public ActionResult Create()
        {
            ViewBag.MagzineId = new SelectList(db.Magzines, "Id", "Title");
            return View();
        }

        // POST: ArAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArAsset arAsset, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/ar/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    arAsset.InputImageUrl = newFilenameUrl;

                  


                    Image img = Image.FromFile(Server.MapPath(newFilenameUrl));
                    int width = img.Width;
                    int height = img.Height;

                    arAsset.InputSize = width + "x" + height;

                }


                if (fileUploadFile != null)
                {
                    string filename = Path.GetFileName(fileUploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/ar/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadFile.SaveAs(physicalFilename);

                    arAsset.OutputFileUrl = newFilenameUrl;
                }
                #endregion
                arAsset.IsDeleted=false;
				arAsset.CreationDate= DateTime.Now; 
                arAsset.Id = Guid.NewGuid();
                db.ArAssets.Add(arAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MagzineId = new SelectList(db.Magzines, "Id", "Title", arAsset.MagzineId);
            return View(arAsset);
        }

        // GET: ArAssets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArAsset arAsset = db.ArAssets.Find(id);
            if (arAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.MagzineId = new SelectList(db.Magzines, "Id", "Title", arAsset.MagzineId);
            return View(arAsset);
        }

        // POST: ArAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArAsset arAsset, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/ar/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    arAsset.InputImageUrl = newFilenameUrl;

                    Image img = Image.FromFile(Server.MapPath(newFilenameUrl));
                    int width = img.Width;
                    int height = img.Height;

                    arAsset.InputSize = width + "x" + height;

                }


                if (fileUploadFile != null)
                {
                    string filename = Path.GetFileName(fileUploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/ar/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadFile.SaveAs(physicalFilename);

                    arAsset.OutputFileUrl = newFilenameUrl;
                }
                #endregion
                arAsset.IsDeleted = false;
				arAsset.LastModifiedDate = DateTime.Now;
                db.Entry(arAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MagzineId = new SelectList(db.Magzines, "Id", "Title", arAsset.MagzineId);
            return View(arAsset);
        }

        // GET: ArAssets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArAsset arAsset = db.ArAssets.Find(id);
            if (arAsset == null)
            {
                return HttpNotFound();
            }
            return View(arAsset);
        }

        // POST: ArAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ArAsset arAsset = db.ArAssets.Find(id);
			arAsset.IsDeleted=true;
			arAsset.DeletionDate=DateTime.Now;
 
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
