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
    public class PhoneNumberController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: PhoneNumber
        public ActionResult Index(string txtPhoneNumberSearch)
        {
            if (txtPhoneNumberSearch == "" || txtPhoneNumberSearch == null)
                return View(db.PhoneNumbers.ToList());
            return View(GetByPhoneNumber(txtPhoneNumberSearch));
        }

        public List<PhoneNumber> GetByPhoneNumber(string number)
        {
            return db.PhoneNumbers.Where(x => x.PhoneNo.Contains(number)).ToList();
        }

        //GET: PhoneNumber/ShowPhoneCallDetail/5
        public ActionResult ShowPhoneCallDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCallDetailController phoneCallDetailController = new PhoneCallDetailController();
            List<PhoneCallDetail> phoneCallDetails = phoneCallDetailController.GetPhoneCallDetailByPhoneNumberId((int)id);

            return View(phoneCallDetails);
        }

        // GET: PhoneNumber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneNumber phoneNumber = db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return HttpNotFound();
            }
            return View(phoneNumber);
        }

        // GET: PhoneNumber/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneNumber/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,PhoneNo,Status")] PhoneNumber phoneNumber)
        {
            if (ModelState.IsValid)
            {
                db.PhoneNumbers.Add(phoneNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phoneNumber);
        }

        // GET: PhoneNumber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneNumber phoneNumber = db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return HttpNotFound();
            }
            return View(phoneNumber);
        }

        // POST: PhoneNumber/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,PhoneNo,Status")] PhoneNumber phoneNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phoneNumber);
        }

        // GET: PhoneNumber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneNumber phoneNumber = db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return HttpNotFound();
            }
            return View(phoneNumber);
        }

        // POST: PhoneNumber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhoneNumber phoneNumber = db.PhoneNumbers.Find(id);
            db.PhoneNumbers.Remove(phoneNumber);
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
