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
    public class MetaDataCSVController : Controller
    {

        public String Index()
        {
            return "No Defult View Specified";
        }

        [HttpPost]
        public IActionResult MetaDataConfirm(IFormCollection form)
        {

            ViewBag.Message = "File extension does not match file type,";

            return View();
        }




    }
}