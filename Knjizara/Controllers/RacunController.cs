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
    public class RacunController : Controller
    {
        private KnjizaraEntities db = new KnjizaraEntities();

        // GET: Racun
        [Authorize(Roles = "radnik")]
        public ActionResult Index()
        {
            return View(db.Racun.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Index(string Search)
        {
            ViewData["GetDetails"] = Search;

            var query = from x in db.Racun select x;
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
        // GET: Racun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racun.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }
        [Authorize(Roles = "radnik")]
        // GET: Racun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Racun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDRacuna,datumVreme,ukVrednost,obradjen")] Racun racun)
        {
            if (db.Racun.Any(part => part.IDRacuna == racun.IDRacuna))
            {
                ModelState.AddModelError("", "Ne mozete uneti redni broj koji vec postoji");
            }
            else if (ModelState.IsValid)
            {
                    db.Racun.Add(racun);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }

            return View(racun);
        }
        [Authorize(Roles = "radnik")]
        // GET: Racun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racun.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRacuna,datumVreme,ukVrednost,obradjen")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racun);
        }
        [Authorize(Roles = "radnik")]
        // GET: Racun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racun.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racun.Find(id);
            foreach(StavkaRacuna stavkaRacuna in db.StavkaRacuna)
            {
                if(stavkaRacuna.IDRacuna == racun.IDRacuna)
                {
                    db.StavkaRacuna.Remove(stavkaRacuna);
                }
            }
            db.SaveChanges();
            db.Racun.Remove(racun);
            TempData["BrisiRacun"] = "uspeh";
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
