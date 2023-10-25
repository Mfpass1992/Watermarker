using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tester.Models;
using WaterMarker.Interfaces;

namespace Tester.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWatermarkGenerator _watermark;
        public HomeController(ILogger<HomeController> logger, IWatermarkGenerator watermark)
        {
            _logger = logger;
            _watermark = watermark;
        }

        public IActionResult Index()
        {
            var file = System.IO.File.ReadAllBytes("./input.wav");
            var wm = System.IO.File.ReadAllBytes("./watermark_.mp3");
            var wf = _watermark.OnFile(file)
                .WithFile(wm)
                //.WithDefaultWatermark()
                .AsByteArray();
            System.IO.File.WriteAllBytes("./input_w.wav", wf);
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