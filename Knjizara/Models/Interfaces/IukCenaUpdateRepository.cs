using Knjizara.Models.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Models.Interfaces
{
    interface IukCenaUpdateRepository
    {
        void UpdateCena(StavkaRacuna sr);
    }
}
