using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;

namespace quanlycuocdienthoai.Controllers
{
    public class PostageDetailController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: PostageDetail
        public ActionResult Index()
        {
            return View(db.PostageDetails.ToList());
        }

        // GET: PostageDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostageDetail postageDetail = db.PostageDetails.Find(id);
            if (postageDetail == null)
            {
                return HttpNotFound();
            }
            return View(postageDetail);
        }

        // GET: PostageDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostageDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,PostageFK,HourMark,Cost")] PostageDetail postageDetail)
        {
            if (ModelState.IsValid)
            {
                db.PostageDetails.Add(postageDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postageDetail);
        }

        // GET: PostageDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostageDetail postageDetail = db.PostageDetails.Find(id);
            if (postageDetail == null)
            {
                return HttpNotFound();
            }
            return View(postageDetail);
        }

        // POST: PostageDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,PostageFK,HourMark,Cost")] PostageDetail postageDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postageDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postageDetail);
        }

        // GET: PostageDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostageDetail postageDetail = db.PostageDetails.Find(id);
            if (postageDetail == null)
            {
                return HttpNotFound();
            }
            return View(postageDetail);
        }

        // POST: PostageDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostageDetail postageDetail = db.PostageDetails.Find(id);
            db.PostageDetails.Remove(postageDetail);
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
