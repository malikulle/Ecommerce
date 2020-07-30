using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploudsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploudsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]

        public string Post([FromForm] FileUploud objectFile)
        {
            try
            {
                if (objectFile.files.Length > 0)
                {
                    string path = _environment.WebRootPath + "\\img\\";

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.files.FileName))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "Uplouded";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return "";
        }
    }


    public class FileUploud
    {
        public IFormFile files { get; set; }
    }
}
