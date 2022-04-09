using haris_edin_rs1.Data;
using haris_edin_rs1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SlikaController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        public SlikaController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            this._dbContext = dbContext;
            webHostEnvironment = hostEnvironment;
            httpContextAccessor = _httpContextAccessor;

        }

        [HttpPost]
        public IActionResult Upload(IFormFile[] files)
        {
            try
            {
                var baseURL = httpContextAccessor.HttpContext.Request.Scheme + "://" +
                   httpContextAccessor.HttpContext.Request.Host +
                   httpContextAccessor.HttpContext.Request.PathBase;
                var fileUploadInfos = new List<FileUploadInfo>();
                foreach (var file in files)
                {
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", file.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    fileUploadInfos.Add(new FileUploadInfo
                    {
                        Name = baseURL + "/uploads/" + file.FileName
                    });
                }



                return Ok(fileUploadInfos) ;

            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
