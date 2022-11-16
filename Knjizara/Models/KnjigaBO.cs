using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class KnjigaBO
    {
        public int IdKnjige { get; set; }
        public string autor { get; set; }
        public string naziv { get; set; }
        public int tiraz { get; set; }
        public string izdavastvo { get; set; }
        public int godinaIzdanja { get; set; }
        public int jedCena { get; set; }

        public string nazivAutor
        {
            get { return autor + " " + naziv; }
        }
    }
}