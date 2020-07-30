using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ECommerce.Api.Helpers.File
{


    public static class UploudFile
    {

        public static String Base64ToImage(string base64string,IWebHostEnvironment _env)
        {
            var base64array = Convert.FromBase64String(base64string);

            string fileName = Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.Millisecond + ".jpg";

            var filePath = Path.Combine($"{_env.WebRootPath}/img/{fileName}");
            System.IO.File.WriteAllBytes(filePath, base64array);
            return fileName;
        }

    }
}
