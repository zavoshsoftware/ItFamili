using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace AdminPresentation.Controllers
{
    public class ContentGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ContentGroups
        public ActionResult Index()
        {
            return View(db.ContentGroups.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: ContentGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGroup contentGroup = db.ContentGroups.Find(id);
            if (contentGroup == null)
            {
                return HttpNotFound();
            }
            return View(contentGroup);
        }

        // GET: ContentGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,UrlParam,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] ContentGroup contentGroup)
        {
            if (ModelState.IsValid)
            {
				contentGroup.IsDeleted=false;
				contentGroup.CreationDate= DateTime.Now; 
                contentGroup.Id = Guid.NewGuid();
                db.ContentGroups.Add(contentGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contentGroup);
        }

        // GET: ContentGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGroup contentGroup = db.ContentGroups.Find(id);
            if (contentGroup == null)
            {
                return HttpNotFound();
            }
            return View(contentGroup);
        }

        // POST: ContentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,UrlParam,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] ContentGroup contentGroup)
        {
            if (ModelState.IsValid)
            {
				contentGroup.IsDeleted = false;
				contentGroup.LastModifiedDate = DateTime.Now;
                db.Entry(contentGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contentGroup);
        }

        // GET: ContentGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGroup contentGroup = db.ContentGroups.Find(id);
            if (contentGroup == null)
            {
                return HttpNotFound();
            }
            return View(contentGroup);
        }

        // POST: ContentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContentGroup contentGroup = db.ContentGroups.Find(id);
			contentGroup.IsDeleted=true;
			contentGroup.DeletionDate=DateTime.Now;
 
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
