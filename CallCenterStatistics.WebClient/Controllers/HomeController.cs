using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallCenterStatistics.WebClient.Models;
using Microsoft.AspNetCore.Http;

namespace CallCenterStatistics.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private const string FileExtension = ".csv";
        
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                ModelState.AddModelError(string.Empty, "File should be chosen");
            
            if(Path.GetExtension(file?.FileName) != FileExtension)
                ModelState.AddModelError(string.Empty, $"File extension should be '{FileExtension}'");

            if (!ModelState.IsValid) return View();

            var filePath = Path.GetTempFileName();
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            ViewData["FileName"] = Path.GetFileName(filePath);
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
