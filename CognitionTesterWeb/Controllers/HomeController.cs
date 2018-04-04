using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CognitionTesterWeb.Controllers
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
            _logger.LogInformation("Home Action Clicked");

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot root = builder.Build();

            ViewData["Environment"] = root.GetValue<string>(
                "MongoDb:Environment"); 
            ViewData["Environment_TextClassification"] = root.GetValue<string>(
                "CognitionService:TextClassification:Environment");

            ViewData["Environment_TextKeyword"] = "";
            ViewData["Environment_TextAbstract"] = "";
            ViewData["Environment_OCR"] = "";
            ViewData["Environment_ImageRecognition"] = "";
            ViewData["Environment_VideoRecognition"] = "";

            return View();
        }

        public IActionResult TextClassification()
        {
            ViewData["Title"] = "稿件自动归类";
            ViewData["Message"] = "稿件自动归类";

            return View();
        }

        public IActionResult TextKeyword()
        {
            ViewData["Title"] = "提取关键词（文章打标签）";
            ViewData["Message"] = "提取关键词（文章打标签）";

            return View("PageIsInConstruction");
        }

        public IActionResult TextAbstract()
        {
            ViewData["Title"] = "文本提取摘要";
            ViewData["Message"] = "文本提取摘要";

            return View("PageIsInConstruction");
        }

        public IActionResult OCR()
        {
            ViewData["Title"] = "OCR（图片文字识别）";
            ViewData["Message"] = "OCR（图片文字识别）";

            return View("PageIsInConstruction");

        }

        public IActionResult ImageRecognition()
        {
            ViewData["Title"] = "图片内容识别";
            ViewData["Message"] = "图片内容识别";

            return View("PageIsInConstruction");
        }

        public IActionResult VideoRecognition()
        {
            ViewData["Title"] = "视频内容识别";
            ViewData["Message"] = "视频内容识别";

            return View("PageIsInConstruction");
        }
    }
}
