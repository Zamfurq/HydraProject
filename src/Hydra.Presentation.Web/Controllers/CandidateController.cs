using Hydra.Presentation.Web.Models;
using Hydra.Presentation.Web.Models.DataTable;
using Hydra.Presentation.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hydra.Presentation.Web.Controllers
{
    [Authorize]
    [Route("Candidate")]
    public class CandidateController : Controller
    {
        private readonly CandidateServices _services;

        public CandidateController(CandidateServices services)
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
            return View("Upsert", new CandidateViewModel { BootcampList = _services.GetBootcamp()});
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpPost("Insert")]
        public IActionResult Insert(CandidateViewModel vm) 
        {
            vm.BootcampList = _services.GetBootcamp();
            if(!ModelState.IsValid)
            {
                return View("Upsert",vm);
            }
            _services.InsertCandidate(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpGet("Update/{id}")]
        public IActionResult Update(int id)
        {
            CandidateViewModel vm = _services.GetCandidate(id);
            return View("Upsert",vm);
        }

        [Authorize(Roles = "Administrator,Recruiter")]
        [HttpPost("Update/{id}")]
        public IActionResult Update(CandidateViewModel vm)
        {
            vm.BootcampList = _services.GetBootcamp();
            if (!ModelState.IsValid)
            {
                return View("Upsert", vm);
            }
            _services.UpdateCandidate(vm);
            return RedirectToAction("Index");
        }

        [HttpPost("CandidateList")]
        public JsonResult CandidateList([FromBody] DataTableParams dataTable)
        {
            List<CandidateViewModel> candidates = _services.GetCandidates(dataTable);

            int totalRecord = _services.CountCandidates();
            int filterRecord = _services.CountCandidates();
            return Json(new
            {
                draw = dataTable.Draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = candidates
            });
        }

    }
}
