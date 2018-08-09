using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Microsoft.Extensions.Options;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
         private IOptions<ConfigServer> IConfigServerData { get; set; }

         public HomeController(IOptions<ConfigServer> configServerData)
         {
             IConfigServerData = configServerData;
         }
        public IActionResult Index()
        {
            var configData = IConfigServerData.Value;
            ViewData["Message"] = configData?.Message ?? "No Message!";
            ViewData["Version"] = configData?.Info?.Version ?? "No Version!";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
