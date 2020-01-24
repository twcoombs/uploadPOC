using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using uploadPOC;

namespace uploadPOC.Controllers
{
    public class UploadController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getAllFormFields(IFormCollection form)
        {
            string principle = form["fileType"].ToString();

            ViewBag.Message = principle;
            return Content(principle);

        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, IFormCollection form)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);

            string principle = form["fileType"].ToString();

            string typeCheck = Properties.Resources.ResourceManager.GetString(principle + "Extension");
            int typeCheckLength = typeCheck.Length;

            if (fileName.Substring(fileName.Length - typeCheckLength, typeCheckLength) == typeCheck)
            {
                // If file with same name exists delete it
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                // Create new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(fileName))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }

                ViewBag.Message = "File successfully uploaded" + principle;
            }

            else ViewBag.Message = "File extension does not match file type," + fileName.Substring((fileName.Length) - typeCheckLength, typeCheckLength) + "," + typeCheck;

            return View();
        }
    }
}