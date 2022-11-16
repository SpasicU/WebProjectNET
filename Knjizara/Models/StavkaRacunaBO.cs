using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class StavkaRacunaBO
    {
        // U Inteface kreiramo metode
        // Repository ih implementiramo
        // Controller pozivamo metode
        // View prikazujemo metode

        public int IDRacuna { get; set; }
        public int redBroj { get; set; }
        public int IDKnjige { get; set; }
        public int kolicina { get; set; }
        public int jedCena { get; set; }
        public RacunBO Racun { get; set;}
        public MagacinBO Magacin { get; set; }

    }
}