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
using Knjizara.Models.Interfaces;

namespace Knjizara.Controllers
{
    public class StavkaRacunaController : Controller
    {
        private KnjizaraEntities db = new KnjizaraEntities();
        private IukCenaUpdateRepository _ukCenaRacun;
        
        public StavkaRacunaController()
        {
            _ukCenaRacun = new ukCenaUpdateRepository();
        }
        [Authorize(Roles = "radnik")]
        // GET: StavkaRacuna
        public ActionResult Index()
        {
            var stavkaRacuna = db.StavkaRacuna.Include(s => s.Knjiga).Include(s => s.Racun);
            return View(stavkaRacuna.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Index(string Search)
        {
            ViewData["GetDetails"] = Search;

            var query = from x in db.StavkaRacuna select x;
            int id = 0;
            bool uspeh = Int32.TryParse(Search, out id);
            if (!String.IsNullOrEmpty(Search))
            {
                query = query.Where(x => x.IDRacuna == id);
                int kolicina = query.ToList().Count();
                if (kolicina == 0)
                {
                    TempData["nema"] = "uspeh";
                }
            }
            return View(await query.AsNoTracking().ToListAsync());
        }
        [Authorize(Roles = "radnik")]
        // GET: StavkaRacuna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkaRacuna.FirstOrDefault(x => x.IDStavkaRacuna == id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }
        [Authorize(Roles = "radnik")]
        // GET: StavkaRacuna/Create
        public ActionResult Create()
        {
            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "autor");
            ViewBag.IDRacuna = new SelectList(db.Racun, "IDRacuna", "IDRacuna");
            return View();
        }

        // POST: StavkaRacuna/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDRacuna,IDKnjige,kolicina,jedCena")] StavkaRacuna stavkaRacuna)
        {
            if (ModelState.IsValid)
            {
                db.StavkaRacuna.Add(stavkaRacuna);
                db.SaveChanges();
                _ukCenaRacun.UpdateCena(stavkaRacuna);
                return RedirectToAction("Index");
            }
            

            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "autor", stavkaRacuna.IDKnjige);
            ViewBag.IDRacuna = new SelectList(db.Racun, "IDRacuna", "IDRacuna", stavkaRacuna.IDRacuna);
            return View(stavkaRacuna);
        }

        [Authorize(Roles = "radnik")]
        // GET: StavkaRacuna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkaRacuna.FirstOrDefault(x => x.IDStavkaRacuna == id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "autor", stavkaRacuna.IDKnjige);
            ViewBag.IDRacuna = new SelectList(db.Racun, "IDRacuna", "IDRacuna", stavkaRacuna.IDRacuna);
            return View(stavkaRacuna);
        }

        // POST: StavkaRacuna/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRacuna,IDKnjige,kolicina,jedCena")] StavkaRacuna stavkaRacuna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stavkaRacuna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKnjige = new SelectList(db.Knjiga, "IDKnjige", "autor", stavkaRacuna.IDKnjige);
            ViewBag.IDRacuna = new SelectList(db.Racun, "IDRacuna", "IDRacuna", stavkaRacuna.IDRacuna);
            return View(stavkaRacuna);
        }
        [Authorize(Roles = "radnik")]
        // GET: StavkaRacuna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkaRacuna.SingleOrDefault(x => x.IDStavkaRacuna == id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        // POST: StavkaRacuna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StavkaRacuna stavkaRacuna = db.StavkaRacuna.SingleOrDefault(x => x.IDStavkaRacuna == id);
            db.StavkaRacuna.Remove(stavkaRacuna);
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
