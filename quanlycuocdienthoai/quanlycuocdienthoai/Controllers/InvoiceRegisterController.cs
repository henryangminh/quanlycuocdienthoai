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
    public class InvoiceRegisterController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: InvoiceRegister
        public ActionResult Index()
        {
            return View(db.InvoiceRegisters.ToList());
        }

        // GET: InvoiceRegister/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceRegister invoiceRegister = db.InvoiceRegisters.Find(id);
            if (invoiceRegister == null)
            {
                return HttpNotFound();
            }
            return View(invoiceRegister);
        }

        // GET: InvoiceRegister/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceRegister/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,CustomerFK,PhoneNumberFK,CostRegister")] InvoiceRegister invoiceRegister)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceRegisters.Add(invoiceRegister);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceRegister);
        }

        // GET: InvoiceRegister/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceRegister invoiceRegister = db.InvoiceRegisters.Find(id);
            if (invoiceRegister == null)
            {
                return HttpNotFound();
            }
            return View(invoiceRegister);
        }

        // POST: InvoiceRegister/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,CustomerFK,PhoneNumberFK,CostRegister")] InvoiceRegister invoiceRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceRegister);
        }

        // GET: InvoiceRegister/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceRegister invoiceRegister = db.InvoiceRegisters.Find(id);
            if (invoiceRegister == null)
            {
                return HttpNotFound();
            }
            return View(invoiceRegister);
        }

        // POST: InvoiceRegister/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceRegister invoiceRegister = db.InvoiceRegisters.Find(id);
            db.InvoiceRegisters.Remove(invoiceRegister);
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
