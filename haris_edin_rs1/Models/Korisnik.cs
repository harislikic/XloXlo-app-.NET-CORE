using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.Models
{
    [Table("Korisnik")]
    public class Korisnik : KorisnickiNalog
    {
        
       
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-0-9-]+\.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        [ForeignKey(nameof(grad))]
        public int Grad_id { get; set; }
        public Grad grad { get; set; }


     
    }
}
