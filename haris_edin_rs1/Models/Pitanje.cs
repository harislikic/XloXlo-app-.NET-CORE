using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.Models
{
    public class Pitanje
    {
        public int Id { get; set; }
        public string Pitanja { get; set; }


        [ForeignKey(nameof(artikal))]
        public int Artikal_id { get; set; }
        public Artikal artikal { get; set; }

      

    }
}
