using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Formatting.Elasticsearch;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApplication4.Models;
using static WebApplication4.Models.catalog;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           // string xml =System.IO.File.ReadAllText(@"C:\Users\zehra\OneDrive\Masaüstü\book.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\zehra\OneDrive\Masaüstü\book.xml");
            string xmlcontents = doc.InnerXml.ToString();
            XmlSerializer serializer = new XmlSerializer(typeof(catalog));
            using StringReader reader = new StringReader(xmlcontents);
            var data= (catalog)serializer.Deserialize(reader)!;

            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "adi yok";
            _logger.LogInformation("zehra log  {date}", DateTime.Now);
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
