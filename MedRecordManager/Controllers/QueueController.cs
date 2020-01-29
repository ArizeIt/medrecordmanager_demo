using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedRecordManager.Models.QueueRecord;
using MedRecordManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrgentCareData;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedRecordManager.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class QueueController : Controller
    {
        private readonly UrgentCareContext _urgentCareContext;
        private readonly IViewRenderService _viewRenderService;

        public QueueController(UrgentCareContext urgentData, IViewRenderService viewRenderService)
        {
            _urgentCareContext = urgentData;
            _viewRenderService = viewRenderService;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var queuens = _urgentCareContext.CodeReviewQueue.Where(x => x.CreatedBy == HttpContext.User.Identity.Name).Select(x => new ReviewQueue
            {
                Name = x.Name,
                CreatedBy = x.CreatedBy,
                CreatedTime = x.CreatedTime, 
                HasChild = x.ChildrenQueue.Any()
                
            });

            return View();
        }
    }
}
