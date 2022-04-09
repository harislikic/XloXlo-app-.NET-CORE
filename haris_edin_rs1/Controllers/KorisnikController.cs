using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using haris_edin_rs1.Data;

using haris_edin_rs1.Models;
using haris_edin_rs1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran) potrebno odobriti komunikaciju izmedju frontenda i backenda da bi ovo raidlo
            //    return Forbid("poruka");
            return Ok(_dbContext.Korisnici.Include(g=>g.grad).FirstOrDefault(s => s.Id == id));
        }

        //add
        [HttpPost]
        public Korisnik Add([FromBody] KorisniciAddVM x)
        {

            //Provjera da li je korisnik logiran, ako je logiran moci ce dodavati nove ako nije vratit ce se null
            //ova provjera nesto ne radi dobro pa  treba popraviti 

            //   KorisnickiNalog korisnickinalog = ControllerContext.HttpContext.GetKorisnikOfAuthToken();

            //if (korisnickinalog == null)
            //    return null;
            

            var novikorisnik = new Korisnik()
            {

                Ime = x.ime,
                Prezime = x.prezime,
                Email = x.email,
                DatumRodjenja = x.dtumRodjenja,
                Adresa = x.adresa,
                KorisnickoIme = x.korisnickoime,
                Lozinka = x.lozinka,
                Grad_id = x.grad_id


            };


            _dbContext.Add(novikorisnik);
            _dbContext.SaveChanges();
            return novikorisnik;
        }

        //update

        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] KorisniciUpdateAddVM x)
        {

            //ako je administrator ima opciju da edituje korisnicke racune 
            //if (!HttpContext.GetLoginInfo().isPermisijaAdmin || !HttpContext.GetLoginInfo().isPermisijaKorisnik)
              //  return Forbid();
            //takodjer zakometarisano jer nema dozvole da se komunicira

            Korisnik korisnik = _dbContext.Korisnici.FirstOrDefault(s => s.Id == id);

            if (korisnik == null)
                return BadRequest("Pogresan ID");

            korisnik.Ime = x.Ime;
            korisnik.Prezime = x.Prezime;
            korisnik.Email = x.Email;
            korisnik.DatumRodjenja = x.DatumRodjenja;
            korisnik.Adresa = x.Adresa;

            _dbContext.SaveChanges();
            return Get(id);
        }



        /*[HttpGet]
        public ActionResult<List<Korisnik>> GetAll(string ime_prezime)
        {



            var data = _dbContext.Korisnici.Where(x => ime_prezime == null || (x.Ime + " " + x.Prezime)
            .StartsWith(ime_prezime) || (x.Prezime + " " + x.Ime).StartsWith(ime_prezime))
                .OrderByDescending(s => s.Prezime).ThenByDescending(s => s.Ime)
                .AsQueryable();
            return data.Take(100).ToList();

        }*/
        [HttpGet]
        public  ActionResult<List<Korisnik>> GetAll2()
        {



            var country =  _dbContext.Korisnici.Include(i => i.grad).ToList();
            return country;

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            //ako je administrator  ima opciju da brise korisnicke racune 
            if (!HttpContext.GetLoginInfo().isPermisijaAdmin)
                return Forbid();


            Korisnik korisnik = _dbContext.Korisnici.Find(id);

            if (korisnik == null)
                return BadRequest("Pogresan ID");

            _dbContext.Remove(korisnik);
            _dbContext.SaveChanges();
            return Ok(korisnik);
        }




    }
}
