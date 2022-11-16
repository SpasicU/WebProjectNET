using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Knjizara.Models
{
    public class MagacinBO
    {
        [Required]
        public int redBroj { get; set; }
        public int IDKnjige { get; set; }
        public int stanje { get; set; }
        public KnjigaBO knjigaBo { get; set; }
    }
}