using Hydra.Presentation.Web.Models;
using Hydra.Presentation.Web.Models.DataTable;
using Hydra.Presentation.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hydra.Presentation.Web.Controllers
{
    [Authorize]
    [Route("Bootcamp")]
    public class BootcampController : Controller
    {
        private readonly BootcampServices _services;

        public BootcampController(BootcampServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpGet("Insert")]
        public IActionResult Insert()
        {
            return View("Upsert");
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpPost("Insert")]
        public IActionResult Insert(BootcampViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Upsert", vm);
            }
            _services.InsertBootcamp(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpGet("Update/{id}")]
        public IActionResult Update(int id)
        {
            BootcampViewModel vm = _services.GetBootcamp(id);
            return View("Upsert",vm);
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpPost("Update/{id}")]
        public IActionResult Update(BootcampViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Upsert", vm);
            }
            _services.UpdateBootcamp(vm);
            return RedirectToAction("Index");
        }

        [HttpPost("BootcampList/{type}")]
        public JsonResult BootcampList([FromBody]DataTableParams dataTable, string type)
        {
            List<BootcampViewModel> bootcamp = _services.GetAllBootcamp(dataTable, type);

            int totalRecord = _services.CountBootcamp(type);
            int filterRecord = _services.CountBootcamp(type);
            return Json(new
            {
                draw = dataTable.Draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = bootcamp
            });
        }
    }
}
