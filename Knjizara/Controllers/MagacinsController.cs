using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Knjizara.Models.EFRepository;

namespace Knjizara.Controllers
{
    public class MagacinsController : Controller
    {
        private KnjizaraEntities db = new KnjizaraEntities();

        // GET: Magacins
        [Authorize(Roles = "menadzer")]
        public ActionResult Index()
        {
            var magacin = db.Magacin.Include(m => m.Knjiga);
            return View(magacin.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Index(string Search)
        {
            ViewData["GetDetails"] = Search;

            var query = from x in db.Magacin select x;
            int id = 0;
            bool uspeh = Int32.TryParse(Search, out id);
            if (!String.IsNullOrEmpty(Search))
            {
                query = query.Where(x => x.IDKnjige == id);
                int kolicina = query.ToList().Count();
                if (kolicina == 0)
                {
                    TempData["nema"] = "uspeh";
                }
            }
            return View(await query.AsNoTracking().ToListAsync());
        }
        [Authorize(Roles = "menadzer")]
        // GET: Magacins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magacin magacin = db.Magacin.FirstOrDefault(x => x.redBroj == id);
            if (magacin == null)
            {
                return HttpNotFound();
            }
            return View(magacin);
        }
        
        [Authorize(Roles = "menadzer")]
        // GET: Magacins/Create
        public ActionResult Create()
        {
            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "naziv");
            return View();
        }

        // POST: Magacins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "redBroj,IDKnjige,stanje")] Magacin magacin)
        {
            if (ModelState.IsValid)
            {
                if (db.Magacin.Any(part => part.redBroj == magacin.redBroj))
                {
                    ModelState.AddModelError("", "Ne mozete uneti redni broj koji vec postoji");
                }
                else
                {
                    db.Magacin.Add(magacin);
                    TempData["Message"] = "uspeh";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "naziv", magacin.IDKnjige);
            return View(magacin);
        }
        [Authorize(Roles = "menadzer")]
        // GET: Magacins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magacin magacin = db.Magacin.FirstOrDefault(x => x.redBroj == id);
            if (magacin == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "autor", magacin.IDKnjige);
            return View(magacin);
        }

        // POST: Magacins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "redBroj,IDKnjige,stanje")] Magacin magacin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magacin).State = EntityState.Modified;
                TempData["Save"] = "uspeh";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "autor", magacin.IDKnjige);
            return View(magacin);
        }
        [Authorize(Roles = "menadzer")]
        // GET: Magacins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magacin magacin = db.Magacin.SingleOrDefault(x => x.redBroj == id);
            if (magacin == null)
            {
                return HttpNotFound();
            }
            return View(magacin);
        }

        // POST: Magacins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magacin magacin = db.Magacin.SingleOrDefault(x => x.redBroj == id);
            db.Magacin.Remove(magacin);
            TempData["brisanje"] = "uspeh";
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
