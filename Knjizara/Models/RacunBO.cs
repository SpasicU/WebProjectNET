using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class RacunBO
    {
        // U Inteface kreiramo metode
        // Repository ih implementiramo
        // Controller pozivamo metode
        // View prikazujemo metode

        public int IDRacuna { get; set; }
        public DateTime datumVreme { get; set; }
        public float ukupnaVrednost { get; set; }
        public int obradjen { get; set; }
        public StavkaRacunaBO StavkeRacuna { get; set; }
        
    }
}