using System;
using System.ComponentModel.DataAnnotations.Schema;
using haris_edin_rs1.Models;
using Microsoft.AspNetCore.Http;

namespace haris_edin_rs1.ViewModels
{
    public class KorisniciAddVMM : KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        [ForeignKey(nameof(grad))]
        public int Grad_id { get; set; }
        public Grad grad { get; set; }

        [ForeignKey(nameof(spol))]
        public int Spol_id { get; set; }
        public Spol spol { get; set; }

        public string KontaktTelefon { get; set; }
        public IFormFile SlikaProfila { get; set; }

        public bool Twoway { get; set; }


    }
}
