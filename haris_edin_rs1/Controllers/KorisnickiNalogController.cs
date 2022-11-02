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
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading;
using Microsoft.Extensions.FileProviders;

namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorisnickiNalogController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public KorisnickiNalogController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public  ActionResult<List<Korisnik>> GetAll()
        {

            var Nalozi =  _dbContext.KorisnickiNalog.ToList();
            return Ok(Nalozi);

        }
    
    }
}
