using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quanlycuocdienthoai.DAL;
using quanlycuocdienthoai.Models;

namespace quanlycuocdienthoai.Controllers
{
    public class PostageController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: Postage
        public ActionResult Index()
        {
            return View(db.Postages.ToList());
        }

        // GET: Postage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postage postage = db.Postages.Find(id);
            if (postage == null)
            {
                return HttpNotFound();
            }
            return View(postage);
        }

        // GET: Postage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Postage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,DateApplied")] Postage postage)
        {
            if (ModelState.IsValid)
            {
                db.Postages.Add(postage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postage);
        }

        // GET: Postage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postage postage = db.Postages.Find(id);
            if (postage == null)
            {
                return HttpNotFound();
            }
            return View(postage);
        }

        // POST: Postage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,DateApplied")] Postage postage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postage);
        }

        // GET: Postage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postage postage = db.Postages.Find(id);
            if (postage == null)
            {
                return HttpNotFound();
            }
            return View(postage);
        }

        // POST: Postage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Postage postage = db.Postages.Find(id);
            db.Postages.Remove(postage);
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
