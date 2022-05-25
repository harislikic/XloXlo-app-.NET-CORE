using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.ViewModels
{
    public class ArtikalAddVM
    {

        public int Kategorija_Produkta_id { get; set; }
        public int Brend_id { get; set; }

        public int Korisnik_id { get; set; }
        public string NazivArtikla { get; set; }
        public double Cijena { get; set; }

        public bool Aktivan { get; set; }
        public DateTime DatumObjave { get; set; }
        public int Stanje { get; set; }
        public IFormFile SlikaArtikla { get; set; }

        public string Godiste { get; set; }
        public string Kilometraza { get; set; }
        public bool Registrovan { get; set; }
        public bool Plin { get; set; }
        public bool Klima { get; set; }
        public bool ABS { get; set; }
        public string Gorivo { get; set; }
        public string Model { get; set; }
        public string DetaljanOpis { get; set; }


    }
}
