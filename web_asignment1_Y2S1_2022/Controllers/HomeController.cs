using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web_asignment1_Y2S1_2022.Models;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace web_asignment1_Y2S1_2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //This controls the Index.cshtml. 
        {
            return View();
        }

        public IActionResult Forbidden() //This controls Forbidden.cshtml
        {
            //This page can only be accessed if the user is denied access
            return View(); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
