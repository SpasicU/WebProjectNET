using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knjizara.Models.EFRepository;

namespace Knjizara.Controllers
{
    public class NarudzbenicaController : Controller
    {
        private KnjizaraEntities db = new KnjizaraEntities();


        [Authorize(Roles ="menadzer")]
        // GET: Narudzbenica
        public ActionResult Index()
        {
            return View(db.Narudzbenica.ToList());
        }
        [Authorize(Roles = "menadzer")]
        // GET: Narudzbenica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbenica narudzbenica = db.Narudzbenica.Find(id);
            if (narudzbenica == null)
            {
                return HttpNotFound();
            }
            return View(narudzbenica);
        }
        [Authorize(Roles = "menadzer")]
        // GET: Narudzbenica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Narudzbenica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNarudzbenice,datum")] Narudzbenica narudzbenica)
        {
            if (db.Narudzbenica.Any(part => part.IDNarudzbenice == narudzbenica.IDNarudzbenice))
            {
                ModelState.AddModelError("", "Ne mozete uneti identifikacioni broj koji vec postoji");
            }
            else if (ModelState.IsValid)
            {
                    db.Narudzbenica.Add(narudzbenica);
                    db.SaveChanges();
                    return RedirectToAction("Create", "StavkaNarudzbenice");
            }

            return View(narudzbenica);
        }
        [Authorize(Roles = "menadzer")]
        // GET: Narudzbenica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbenica narudzbenica = db.Narudzbenica.Find(id);
            if (narudzbenica == null)
            {
                return HttpNotFound();
            }
            return View(narudzbenica);
        }

        // POST: Narudzbenica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNarudzbenice,datum")] Narudzbenica narudzbenica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(narudzbenica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(narudzbenica);
        }
        [Authorize(Roles = "menadzer")]
        // GET: Narudzbenica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbenica narudzbenica = db.Narudzbenica.Find(id);
            if (narudzbenica == null)
            {
                return HttpNotFound();
            }
            return View(narudzbenica);
        }

        // POST: Narudzbenica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narudzbenica narudzbenica = db.Narudzbenica.Find(id);
            db.Narudzbenica.Remove(narudzbenica);
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
