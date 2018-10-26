using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MedRecordManager.Models;
using UrgentCareData;
using System.Linq;
using System;
using System.Collections.Generic;
using UrgentCareData.Models;

namespace MedRecordManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrgentCareContext _urgentCareContext;
        public HomeController(UrgentCareContext urgentData)
        {
            _urgentCareContext = urgentData;
        }
        public IActionResult Index()
        {
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


        public IActionResult GetJobProcess(int? page, int? limit)
        {
            var empty = new List<SourceProcessLog>();
            var total = 0;
            try
            {
                var records = _urgentCareContext.SourceProcessLog
                .Where(x => x.ProcessedDate >= DateTime.Today.AddDays(-8))
                .Select(x => new {
                    processId = x.ProcessId,
                    processedDate = x.ProcessedDate.Value.ToString("yyyy-MM-dd HH:MM:ss"),
                    sourceFileName = x.SourceFileName.Substring(x.SourceFileName.LastIndexOf("\\")+1),
                    processResult = x.ProcessResult,
                    successFlag = x.SuccessFlag
                }).OrderByDescending(x => x.processedDate).ToList().Take(15);

               

                total = records.Count();

                if (page.HasValue && limit.HasValue)
                {
                    var start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value).ToList();
                }
                return Json(new { records, total });
            }
            
            catch
            {
                return Json(new { empty, total });
            }
        }

    }
}
