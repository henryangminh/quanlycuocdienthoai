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
    public class SIMController : Controller
    {
        private PostageContext db = new PostageContext();

        // GET: SIM
        public ActionResult Index()
        {
            return View(db.SIMs.Include(p => p.PhoneNumberFKNavigation).ToList());
        }

        // GET: SIM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIM sIM = db.SIMs.Find(id);
            if (sIM == null)
            {
                return HttpNotFound();
            }
            return View(sIM);
        }

        // GET: SIM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SIM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyId,PhoneNumberFK,Status")] SIM sIM)
        {
            if (ModelState.IsValid)
            {
                db.SIMs.Add(sIM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sIM);
        }

        // GET: SIM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIM sIM = db.SIMs.Find(id);
            if (sIM == null)
            {
                return HttpNotFound();
            }
            return View(sIM);
        }

        // POST: SIM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyId,PhoneNumberFK,Status")] SIM sIM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sIM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sIM);
        }

        // GET: SIM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIM sIM = db.SIMs.Find(id);
            if (sIM == null)
            {
                return HttpNotFound();
            }
            return View(sIM);
        }

        // POST: SIM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SIM sIM = db.SIMs.Find(id);
            db.SIMs.Remove(sIM);
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
