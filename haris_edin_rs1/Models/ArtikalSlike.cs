using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.Models
{
    public class ArtikalSlike
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public Artikal Artikal { get; set; }
    }
}
