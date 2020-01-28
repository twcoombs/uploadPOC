using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using uploadPOC;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using uploadPOC.Classes;
using Microsoft.Extensions.Hosting.Internal;

namespace uploadPOC.Controllers
{
    public class UploadController : Controller
    {

        public String Index()
        {
            return "No Defult View Specified";
        }

        public IActionResult Upload()
        {
            return View();
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

                ViewBag.Message = "File successfully uploaded: " + fileName;

                if(principle == "excelnew")
                {
                    ViewBag.Link = "MetaData?file=" + System.IO.Path.GetFullPath(fileName);
                }
                else if (principle == "csv")
                {
                    ViewBag.Link = "MetaDataCSV/MetaDataCSV?file=" + System.IO.Path.GetFullPath(fileName);
                }

            }

            else
                ViewBag.Message = "File extension does not match file type," + fileName.Substring((fileName.Length) - typeCheckLength, typeCheckLength) + "," + typeCheck;

            return View();
        }


        public IActionResult MetaData(string file)
        {
            // Extract file name from whatever was posted by browser
            var fileName = file;

            ViewBag.Message = "View Meta-data Definition for Current File: " + fileName;

            ExcelRead er = new ExcelRead(fileName);
            er.GetExcel(file);

            
            string one = "The name of the first worksheet in this workbook is: " + er.getNameOfSheet();
            //string two = er.ReadCell(1, 1);

            //ViewBag.ReadCell = two;

            return View();
        }

        public IActionResult MetaDataCSV(string file)
        {
            // Extract file name from whatever was posted by browser
            var fileName = file;

            CSVRead cs = new CSVRead();
            List<string> listA = cs.readCSV(file);

            ViewBag.Message = "View Meta-data Definition for Current File: " + fileName;

            ViewBag.List = listA;

            return View();
        }

        public IActionResult FinaliseDefinitions()
        {

            ViewBag.Message = "View Meta-data Definition for Current File: ";


            return View();
        }


    }
}