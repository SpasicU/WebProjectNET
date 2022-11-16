using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Knjizara.Models;
using Knjizara.Models.EFRepository;
using Knjizara.Models.Interfaces;

namespace Knjizara.Controllers
{
    public class KnjigaController : Controller
    {
        private KnjizaraEntities db = new KnjizaraEntities();

        [Authorize(Roles = "radnik, menadzer")]
        // GET: Knjigas
        public ActionResult Index()
        {
            return View(db.Knjiga.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Index(string Search)
        {
            ViewData["GetDetails"] = Search;

            var query = from x in db.Knjiga select x;
            if (!String.IsNullOrEmpty(Search))
            {
                query = query.Where(x => x.autor.Contains(Search) || x.naziv.Contains(Search) || x.izdavastvo.Contains(Search));
                int kolicina = query.ToList().Count();
                if (kolicina == 0)
                {
                    TempData["nema"] = "uspeh";
                }
            }
            return View(await query.AsNoTracking().ToListAsync());
        }

        [Authorize(Roles = "radnik, menadzer")]
        // GET: Knjigas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjiga knjiga = db.Knjiga.Find(id);
            if (knjiga == null)
            {
                return HttpNotFound();
            }
            return View(knjiga);
        }

        [Authorize(Roles = "radnik, menadzer")]
        // GET: Knjigas/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Knjigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKnjige,autor,naziv,tiraz,izdavastvo,godinaIzdanja,jedCena")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                if (db.Knjiga.Any(part => part.IDKnjige == knjiga.IDKnjige))
                {
                    TempData["Error"] = "IDerror";
                    return RedirectToAction("Index");
                }
                else
                {
                    try
                    {
                        db.Knjiga.Add(knjiga);
                        TempData["Message"] = "uspeh";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Knjiga sa unetim ID vec postoji" + ex);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "IDerror";
            return View(knjiga);
        }



        [Authorize(Roles = "radnik, menadzer")]
        // GET: Knjigas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjiga knjiga = db.Knjiga.Find(id);
            if (knjiga == null)
            {
                return HttpNotFound();
            }
            return View(knjiga);
        }

        // POST: Knjigas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKnjige,autor,naziv,tiraz,izdavastvo,godinaIzdanja,jedCena")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knjiga).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Save"] = "saved";
                return RedirectToAction("Index");
            }
            return View(knjiga);
        }

        [Authorize(Roles = "radnik, menadzer")]
        // GET: Knjigas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjiga knjiga = db.Knjiga.Find(id);
            if (knjiga == null)
            {
                return HttpNotFound();
            }
            return View(knjiga);
        }

        // POST: Knjigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knjiga knjiga = db.Knjiga.Find(id);
            db.Knjiga.Remove(knjiga);
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
