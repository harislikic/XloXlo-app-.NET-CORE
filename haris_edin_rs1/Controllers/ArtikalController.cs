using haris_edin_rs1.Data;
using haris_edin_rs1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using haris_edin_rs1.ViewModels;

namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArtikalController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
       // private object webHostEnvironment;
        private readonly IWebHostEnvironment webHostEnvironment;

        
        private IHttpContextAccessor httpContextAccessor;

        public ArtikalController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            this._dbContext = dbContext;
            webHostEnvironment = hostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
                .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje).Include(k=>k.korisnik).Include(g=>g.korisnik.grad).FirstOrDefault(s => s.Id == id));
        }



        public class ArtikalAddVM
        {

            public int Kategorija_Produkta_id { get; set; }
            public int Brend_id { get; set; }
            public int Spol_id { get; set; }
            public int Korisnik_id { get; set; }
            public string NazivArtikla { get; set; }
            public double Cijena { get; set; }

            public bool Aktivan { get; set; }
            public DateTime DatumObjave { get; set; }
            public int Stanje { get; set; }
         
        }
        //add
        [HttpPost]
        public Artikal Add([FromBody] ArtikalAddVM x)
        {
            //string uniqueFileName = Upload(x.SlikaArtikla);
            var noviArtikal = new Artikal()
            {
                Kategorija_Produkta_id = x.Kategorija_Produkta_id,
                Brend_id = x.Brend_id,
                Spol_id = x.Spol_id,
                korisnik_id = x.Korisnik_id,
                NazivArtikla = x.NazivArtikla,
                Cijena = x.Cijena,
                Aktivan = x.Aktivan,
                Stanje_id = x.Stanje,
                DatumObjave = x.DatumObjave,
                SlikaArtikla = "https://localhost:44326/" + "uploads/" + "empty.png"


            };
            _dbContext.Artikal.Add(noviArtikal);
            _dbContext.SaveChanges();
            return noviArtikal;
        }

        [HttpPost]
        public IActionResult Upload(int id, [FromForm]  ArtikalSlikaAddVM x)
        {
            try
            {
                Artikal artikal = _dbContext.Artikal.FirstOrDefault(a => a.Id == id);


                var baseURL = httpContextAccessor.HttpContext.Request.Scheme + "://" +
                   httpContextAccessor.HttpContext.Request.Host +
                   httpContextAccessor.HttpContext.Request.PathBase;
             
                        string ekstenzija = Path.GetExtension(x.SlikaArtikla.FileName);

                        var filename = $"{Guid.NewGuid()}{ekstenzija}";

                        x.SlikaArtikla.CopyTo(new FileStream("wwwroot/" + "uploads/" + filename, FileMode.Create));                  
                       artikal.SlikaArtikla = filename ;                                          
                        _dbContext.SaveChanges();
                
                return Ok(artikal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }
      
            //update

            [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] ArtikalAddVM x)
        {


            Artikal artikal = _dbContext.Artikal.FirstOrDefault(s => s.Id == id);

            if (artikal == null)
                return BadRequest("Pogresan ID");

            artikal.Kategorija_Produkta_id = x.Kategorija_Produkta_id;
            artikal.Brend_id = x.Brend_id;
            artikal.Spol_id = x.Spol_id;
            artikal.korisnik_id = x.Korisnik_id;
            artikal.NazivArtikla = x.NazivArtikla;
            artikal.Cijena = x.Cijena;
            artikal.Aktivan = x.Aktivan;
            artikal.DatumObjave = x.DatumObjave;
            artikal.Stanje_id = x.Stanje;

                
            _dbContext.SaveChanges();
            return Get(id);
        }
        [HttpGet]
        public ActionResult<List<Artikal>> GetAll()
        {

            var data = _dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
                .Include(s=>s.spol)
                .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje)
                .Include(k=>k.korisnik).ToList();
            return data;

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            Artikal artikal = _dbContext.Artikal.Find(id);

            if (artikal == null)
                return BadRequest("Pogresan ID");

            _dbContext.Remove(artikal);
            _dbContext.SaveChanges();
            return Ok(artikal);
        }
        [HttpGet("{id}")]
        public IActionResult GetPoKategoriji(int id)
        {
            return Ok(_dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
                .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje).Where(kp => kp.Kategorija_Produkta_id == id));
        }
        [HttpGet("{id}")]
        public IActionResult GetPoBrendu(int id)
        {
            return Ok(_dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
                .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje).Where(kp => kp.Brend_id == id));
        }
        [HttpGet("{id}")]
        public IActionResult GetPoStanju(int id)
        {
            return Ok(_dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
                .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje).Where(kp => kp.Stanje_id == id));
        }
        [HttpGet("{id}")]
        public IActionResult GetPoSpolu(int id)
        {
            return Ok(_dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
                .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje).Where(kp => kp.Spol_id == id));
        }
    [HttpGet("{id}")]
    public IActionResult GetPoKorisniku(int id)
    {
        return Ok(_dbContext.Artikal.Include(kp => kp.kategorijaprodukta)
            .Include(b => b.brend).Include(s => s.spol).Include(st => st.stanje).Include(k=>k.korisnik).Include(kg => kg.korisnik.grad).Where(kp => kp.korisnik_id == id));
    }
    }
}
