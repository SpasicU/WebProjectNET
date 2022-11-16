using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Models.Interfaces
{
    interface IMagacinRepository
    {
        // U Inteface kreiramo metode
        // Repository ih implementiramo
        // Controller pozivamo metode
        // View prikazujemo metode

        IEnumerable<MagacinBO> GetAll();
        IEnumerable<KnjigaBO> GetAllKnjigaBOs();
    }
}
