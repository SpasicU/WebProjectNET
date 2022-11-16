using Knjizara.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models.EFRepository
{
    public class ukCenaUpdateRepository : IukCenaUpdateRepository
    {
        private KnjizaraEntities _db = new KnjizaraEntities();


        public void UpdateCena(StavkaRacuna sr)
        {
            int ID = sr.IDRacuna;
            Racun racunUpdate = _db.Racun.SingleOrDefault(t=> t.IDRacuna == ID);

            float ukCena = (float)(Convert.ToInt32(sr.jedCena) * sr.kolicina);

            racunUpdate.ukVrednost += ukCena;
            if (racunUpdate.IDRacuna == ID)
            {
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Neuspesan update ukupne cene: " + e);
                }
            }
            else
            {
                Console.WriteLine("ID se ne podudara: ");
            }
        }
    }
}