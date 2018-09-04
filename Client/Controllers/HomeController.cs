using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private IOptionsSnapshot<ConfigServer> IConfigServerData { get; set; }
        private IConfigurationRoot Config { get; set; }

        public HomeController(IOptionsSnapshot<ConfigServer> configServerData, IConfigurationRoot config)
        {
            IConfigServerData = configServerData;
            Config = config;
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

        public IActionResult Reload()
        {
            Config.Reload();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
