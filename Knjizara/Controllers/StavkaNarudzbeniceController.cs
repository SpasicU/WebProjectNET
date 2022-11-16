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
    public class StavkaNarudzbeniceController : Controller
    {
        private KnjizaraEntities db = new KnjizaraEntities();
        [Authorize(Roles = "menadzer")]
        // GET: StavkaNarudzbenice
        public ActionResult Index()
        {
            var stavkaNarudzbenice = db.StavkaNarudzbenice.Include(s => s.Magacin).Include(s => s.Narudzbenica1);
            return View(stavkaNarudzbenice.ToList());
        }
        [Authorize(Roles = "menadzer")]
        // GET: StavkaNarudzbenice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaNarudzbenice stavkaNarudzbenice = db.StavkaNarudzbenice.SingleOrDefault(t => t.IDStavkaNarudzbenice == id);
            if (stavkaNarudzbenice == null)
            {
                return HttpNotFound();
            }
            return View(stavkaNarudzbenice);
        }
        [Authorize(Roles = "menadzer")]
        // GET: StavkaNarudzbenice/Create
        public ActionResult Create()
        {
            ViewBag.redBroj = new SelectList(db.Magacin, "redBroj", "redBroj");
            ViewBag.IDKnjige = new SelectList(db.Magacin, "IDKnjige", "IDKnjige");
            ViewBag.IDNarudzbenice = new SelectList(db.Narudzbenica, "IDNarudzbenice", "IDNarudzbenice");
            return View();
        }

        // POST: StavkaNarudzbenice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDStavkaNarudzbenice,IDNarudzbenice,redBroj,IDKnjige,kolicina")] StavkaNarudzbenice stavkaNarudzbenice)
        {
            if (ModelState.IsValid)
            {
                if (db.StavkaNarudzbenice.Any(part => part.IDStavkaNarudzbenice == stavkaNarudzbenice.IDStavkaNarudzbenice))
                {
                    ModelState.AddModelError("", "Uneli ste duplikat, unesite drugačiji ID!!");
                }
                else
                {
                    db.StavkaNarudzbenice.Add(stavkaNarudzbenice);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }

            ViewBag.redBroj = new SelectList(db.Magacin, "redBroj", "redBroj", stavkaNarudzbenice.redBroj);
            ViewBag.IDKnjige = new SelectList(db.Magacin, "IDKnjige", "IDKnjige", stavkaNarudzbenice.IDKnjige);
            ViewBag.IDNarudzbenice = new SelectList(db.Narudzbenica, "IDNarudzbenice", "IDNarudzbenice", stavkaNarudzbenice.IDNarudzbenice);
            return View(stavkaNarudzbenice);
        }
        [Authorize(Roles = "menadzer")]
        // GET: StavkaNarudzbenice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaNarudzbenice stavkaNarudzbenice = db.StavkaNarudzbenice.SingleOrDefault(t => t.IDStavkaNarudzbenice == id);
            if (stavkaNarudzbenice == null)
            {
                return HttpNotFound();
            }
            ViewBag.redBroj = new SelectList(db.Magacin, "redBroj", "redBroj", stavkaNarudzbenice.redBroj);
            ViewBag.IDNarudzbenice = new SelectList(db.Narudzbenica, "IDNarudzbenice", "IDNarudzbenice", stavkaNarudzbenice.IDNarudzbenice);
            return View(stavkaNarudzbenice);
        }

        // POST: StavkaNarudzbenice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDStavkaNarudzbenice,IDNarudzbenice,redBroj,IDKnjige,kolicina")] StavkaNarudzbenice stavkaNarudzbenice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stavkaNarudzbenice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.redBroj = new SelectList(db.Magacin, "redBroj", "redBroj", stavkaNarudzbenice.redBroj);
            ViewBag.IDNarudzbenice = new SelectList(db.Narudzbenica, "IDNarudzbenice", "IDNarudzbenice", stavkaNarudzbenice.IDNarudzbenice);
            return View(stavkaNarudzbenice);
        }
        [Authorize(Roles = "menadzer")]
        // GET: StavkaNarudzbenice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaNarudzbenice stavkaNarudzbenice = db.StavkaNarudzbenice.SingleOrDefault(t => t.IDStavkaNarudzbenice == id);
            if (stavkaNarudzbenice == null)
            {
                return HttpNotFound();
            }
            return View(stavkaNarudzbenice);
        }

        // POST: StavkaNarudzbenice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StavkaNarudzbenice stavkaNarudzbenice = db.StavkaNarudzbenice.SingleOrDefault(t => t.IDStavkaNarudzbenice == id);
            db.StavkaNarudzbenice.Remove(stavkaNarudzbenice);
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
