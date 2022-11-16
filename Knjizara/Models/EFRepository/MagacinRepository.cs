using Knjizara.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models.EFRepository
{
    public class MagacinRepository : IMagacinRepository
    {
        private KnjizaraEntities _knjizaraEntities = new KnjizaraEntities();

        public IEnumerable<MagacinBO> GetAll()
        {
            List<MagacinBO> magacins = new List<MagacinBO>();

            foreach (Magacin magacin in _knjizaraEntities.Magacin)
            {
                MagacinBO magacinBo = new MagacinBO();
                magacinBo.IDKnjige = magacin.IDKnjige;
                magacinBo.redBroj = magacin.redBroj;
                magacinBo.stanje = magacin.stanje;
                magacins.Add(magacinBo);
            }
            return magacins;
        }

        public IEnumerable<KnjigaBO> GetAllKnjigaBOs()
        {
            List<KnjigaBO> knjige = new List<KnjigaBO>();
            foreach (Knjiga knjiga in _knjizaraEntities.Knjiga)
            {
                KnjigaBO knjigaBo = new KnjigaBO();
                knjigaBo.IdKnjige = knjiga.IDKnjige;
                knjigaBo.autor = knjiga.autor;
                knjigaBo.naziv = knjiga.naziv;
                knjige.Add(knjigaBo);
            }
            return knjige;

        }
    }

}