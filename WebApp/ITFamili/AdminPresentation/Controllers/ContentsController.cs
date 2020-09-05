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
    public class ContentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid id)
        {
            var contents = db.Contents.Include(c => c.ContentType).Include(x=>x.ContentGroup)
                .Where(c => c.ContentTypeId == id && c.IsDeleted == false).OrderByDescending(c => c.CreationDate);

            return View(contents.ToList());
        }


        public ActionResult Create(Guid id)
        {
            ViewBag.ContentTypeId = id;
            ViewBag.ContentGroupId = new SelectList(db.ContentGroups.Where(x => x.IsDeleted == false).ToList(), "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content content, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadFile, Guid id)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/content/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    content.ImageUrl = newFilenameUrl;
                }


                if (fileUploadFile != null)
                {
                    string filename = Path.GetFileName(fileUploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/content/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadFile.SaveAs(physicalFilename);

                    content.FileUrl = newFilenameUrl;
                }
                #endregion

                content.LikeCount = 0;
                content.CommentCount = 0;
                content.ContentTypeId = id;
                content.IsDeleted = false;
                content.CreationDate = DateTime.Now;
                content.Id = Guid.NewGuid();
                db.Contents.Add(content);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.ContentTypeId = id;
            ViewBag.ContentGroupId = new SelectList(db.ContentGroups.Where(x => x.IsDeleted == false).ToList(), "Id", "Title",content.ContentGroupId);

            return View(content);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContentTypeId = content.ContentTypeId;
            ViewBag.ContentGroupId = new SelectList(db.ContentGroups.Where(x => x.IsDeleted == false).ToList(), "Id", "Title", content.ContentGroupId);

            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Content content, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/content/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    content.ImageUrl = newFilenameUrl;
                }


                if (fileUploadFile != null)
                {
                    string filename = Path.GetFileName(fileUploadFile.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/content/" + newFilename;

                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadFile.SaveAs(physicalFilename);

                    content.FileUrl = newFilenameUrl;
                }
                #endregion
                content.IsDeleted = false;
                content.LastModifiedDate = DateTime.Now;
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = content.ContentTypeId });
            }
            ViewBag.ContentTypeId = content.ContentTypeId;
            ViewBag.ContentGroupId = new SelectList(db.ContentGroups.Where(x => x.IsDeleted == false).ToList(), "Id", "Title", content.ContentGroupId);

            return View(content);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContentTypeId = content.ContentTypeId;
            ViewBag.ContentGroupId = new SelectList(db.ContentGroups.Where(x => x.IsDeleted == false).ToList(), "Id", "Title", content.ContentGroupId);

            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Content content = db.Contents.Find(id);
            content.IsDeleted = true;
            content.DeletionDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = content.ContentTypeId });
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
