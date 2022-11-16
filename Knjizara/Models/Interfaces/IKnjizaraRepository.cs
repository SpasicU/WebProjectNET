using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Models.Interfaces
{
    interface IKnjizaraRepository
    {
        List<KnjigaBO> GetAll();
        void Add(KnjigaBO knjigaBo);
        KnjigaBO GetByID(int IdKnjige);
        void Update(KnjigaBO knjigaBo);
    }
}
