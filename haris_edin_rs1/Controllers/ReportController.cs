using AspNetCore.Reporting;
using haris_edin_rs1.Data;
using haris_edin_rs1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TempReport;

namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private  ApplicationDbContext _dbContext;
        
        public ReportController(ApplicationDbContext db)
        {
            _dbContext = db;
        }
        public static List<Report1Row> getArtikli(ApplicationDbContext db)
        {
            List<Report1Row> podaci = db.Artikal.Select(s => new Report1Row
            {
                NazivArtikla = s.NazivArtikla,
                Cijena = s.Cijena,
                DatumObjave = s.DatumObjave,
                Godiste = s.Godiste,
            }).ToList();
            return podaci;
        }

        [HttpGet]
        public IActionResult Report()
        {
            LocalReport localReport = new LocalReport("Reports/Report1.rdlc");
            List<Report1Row> podaci = getArtikli(_dbContext);
            localReport.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportSastavio", "Administrator");

            ReportResult result = localReport.Execute(RenderType.Pdf, parameters: parameters);
            var stream = new FileStream(@"Reports/Report1.rdlc", FileMode.Open);
            return File(result.MainStream,  "application/pdf", "report.pdf");
            //return Ok(podaci);
        }
    }

}
