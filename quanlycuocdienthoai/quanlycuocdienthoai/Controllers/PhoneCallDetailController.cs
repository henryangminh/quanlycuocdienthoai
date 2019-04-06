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
    public class PhoneCallDetailController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: PhoneCallDetail
        public ActionResult Index()
        {
            return View(db.PhoneCallDetails.ToList());
        }

        // GET: PhoneCallDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCallDetail phoneCallDetail = db.PhoneCallDetails.Find(id);
            if (phoneCallDetail == null)
            {
                return HttpNotFound();
            }
            return View(phoneCallDetail);
        }

        // GET: PhoneCallDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneCallDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,PhoneNumberFK,TimeStart,TimeFinish,SubTotal")] PhoneCallDetail phoneCallDetail)
        {
            if (ModelState.IsValid)
            {
                db.PhoneCallDetails.Add(phoneCallDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phoneCallDetail);
        }

        // GET: PhoneCallDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCallDetail phoneCallDetail = db.PhoneCallDetails.Find(id);
            if (phoneCallDetail == null)
            {
                return HttpNotFound();
            }
            return View(phoneCallDetail);
        }

        // POST: PhoneCallDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,PhoneNumberFK,TimeStart,TimeFinish,SubTotal")] PhoneCallDetail phoneCallDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneCallDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phoneCallDetail);
        }

        // GET: PhoneCallDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCallDetail phoneCallDetail = db.PhoneCallDetails.Find(id);
            if (phoneCallDetail == null)
            {
                return HttpNotFound();
            }
            return View(phoneCallDetail);
        }

        // POST: PhoneCallDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhoneCallDetail phoneCallDetail = db.PhoneCallDetails.Find(id);
            db.PhoneCallDetails.Remove(phoneCallDetail);
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
