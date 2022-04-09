using haris_edin_rs1.Data;
using haris_edin_rs1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PitanjeController : ControllerBase
    {


        private readonly ApplicationDbContext _dbContext;

        public PitanjeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.Pitanje.FirstOrDefault(x => x.Id == id));


        }
    

            public class PitanjeAddVM
            {

                public string Pitanja { get; set; }
                public int artikalID { get; set; }

        }
            //add
            [HttpPost]
            public Pitanje Add([FromBody] PitanjeAddVM x)
            {

            var novopitanje = new Pitanje()
            {
                Pitanja = x.Pitanja,
                Artikal_id = x.artikalID

                };
                _dbContext.Add(novopitanje);
                _dbContext.SaveChanges();
                return novopitanje;
            }

            //update

            [HttpPost("{id}")]
            public IActionResult Update(int id, [FromBody] PitanjeAddVM x)
            {


                Pitanje pitanje = _dbContext.Pitanje.FirstOrDefault(s => s.Id == id);

                if (pitanje == null)
                    return BadRequest("Pogresan ID");

                pitanje.Pitanja = x.Pitanja;
               

                _dbContext.SaveChanges();
                return Get(id);
            }
            [HttpGet]
            public ActionResult<List<Pitanje>> GetAll(string Naziv)
            {

                var data = _dbContext.Pitanje.Where(x => Naziv == null || (x.Pitanja)
                .StartsWith(Naziv))
                    .OrderByDescending(s => s.Pitanja).ThenByDescending(s => s.Pitanja)
                    .AsQueryable();
                return data.Take(100).ToList();

            }

            [HttpDelete("{id}")]
            public ActionResult Delete(int id)
            {

            Pitanje pitanje = _dbContext.Pitanje.Find(id);

                if (pitanje == null)
                    return BadRequest("Pogresan ID");

                _dbContext.Remove(pitanje);
                _dbContext.SaveChanges();
                return Ok(pitanje);
            }
        
    }
}
