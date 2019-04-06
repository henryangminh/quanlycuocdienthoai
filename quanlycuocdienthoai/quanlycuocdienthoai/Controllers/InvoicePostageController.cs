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
    public class InvoicePostageController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: InvoicePostage
        public ActionResult Index()
        {
            return View(db.InvoicePostages.ToList());
        }

        // GET: InvoicePostage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoicePostage invoicePostage = db.InvoicePostages.Find(id);
            if (invoicePostage == null)
            {
                return HttpNotFound();
            }
            return View(invoicePostage);
        }

        // GET: InvoicePostage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoicePostage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,Month,Year,PhoneNumberFK,Total,PaidPostage")] InvoicePostage invoicePostage)
        {
            if (ModelState.IsValid)
            {
                db.InvoicePostages.Add(invoicePostage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoicePostage);
        }

        // GET: InvoicePostage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoicePostage invoicePostage = db.InvoicePostages.Find(id);
            if (invoicePostage == null)
            {
                return HttpNotFound();
            }
            return View(invoicePostage);
        }

        // POST: InvoicePostage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,Month,Year,PhoneNumberFK,Total,PaidPostage")] InvoicePostage invoicePostage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoicePostage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoicePostage);
        }

        // GET: InvoicePostage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoicePostage invoicePostage = db.InvoicePostages.Find(id);
            if (invoicePostage == null)
            {
                return HttpNotFound();
            }
            return View(invoicePostage);
        }

        // POST: InvoicePostage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoicePostage invoicePostage = db.InvoicePostages.Find(id);
            db.InvoicePostages.Remove(invoicePostage);
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
