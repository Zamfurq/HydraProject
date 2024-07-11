using Hydra.Presentation.Web.Models.DataTable;
using Hydra.Presentation.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hydra.Presentation.Web.Services;

namespace Hydra.Presentation.Web.Controllers
{
    [Authorize(Roles = "Trainer")]
    [Route("Course")]
    public class CourseController : Controller
    {
        private readonly CourseServices _services;
        private readonly BootcampServices _bootcampServices;

        public CourseController(CourseServices services, BootcampServices bootcampServices)
        {
            _services = services;
            _bootcampServices = bootcampServices;
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            BootcampViewModel vm = _bootcampServices.GetBootcamp(id);
            if (vm.Progress != 2)
            {
                return RedirectToAction("Index", "Bootcamp");
            }
            return View(new CourseViewModel { BootcampClassId = id });
        }

        [HttpPost("CourseList/{id}")]
        public JsonResult CourseList([FromBody] DataTableParams dataTable, int id)
        {
            List<CourseViewModel> courses = _services.GetCourses(dataTable, id);

            int totalRecord = _services.CountCourses(id);
            int filterRecord = _services.CountCourses(id);
            return Json(new
            {
                draw = dataTable.Draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = courses
            });
        }

        [HttpGet("{id}/Insert")]
        public IActionResult Insert(int id)
        {
            return View("Upsert");
        }

        [HttpGet("{id}/Complete")]
        public IActionResult Complete(int id)
        {
            return View("Complete");
        }
    }
}
