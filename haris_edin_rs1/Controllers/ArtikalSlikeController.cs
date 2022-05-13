using haris_edin_rs1.Data;
using haris_edin_rs1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArtikalSlikeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        // private object webHostEnvironment;
        private readonly IWebHostEnvironment webHostEnvironment;


        private IHttpContextAccessor httpContextAccessor;

        public ArtikalSlikeController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            this._dbContext = dbContext;
            webHostEnvironment = hostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }
        public class ProImages
        {
            public IFormFile Photos { get; set; }
            //public Artikal Artikal { get; set; }
            public int Artikal_id { get; set; }
        }

        //[HttpPost]
        //public IActionResult AddPhotos(Artikal artikal ,ProImages vm)
        //{
        //    foreach (var item in vm.Images)
        //    {
        //        string stringFileNmae = UploadFile(item);
        //        var ArtikalSlika = new ArtikalSlika
        //        {
        //            ImageName = stringFileNmae,
        //            Artikal_id = vm.Artikal_id
        //        };
        //        _dbContext.ArtikalSlika.Add(ArtikalSlika);
        //    }
        //    _dbContext.SaveChanges();
        //    return Ok();
        //}
        //private string UploadFile(IFormFile[] file)
        //{
        //    string fileName = null;
        //    if (file != null)
        //    {
        //        string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
        //        fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
        //        string filePath = Path.Combine(uploadDir, fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyTo(fileStream);
        //        }
        //    }
        //    return fileName;
        //}

        [HttpPost]
        public IActionResult DodajSlike(List<IFormFile> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(file.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        // Combines two strings into a path.
                        var filepath =
                         new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")).Root + $@"\{newFileName}";

                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                    }
                }
            }
            return Ok();
        }





    }

}

