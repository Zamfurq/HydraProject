using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models;
using Hydra.Presentation.Web.Models.DataTable;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hydra.Presentation.Web.Services
{
    public class CandidateServices
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IBootcampRepository _bootcampRepository;

        public CandidateServices(ICandidateRepository candidateRepository, IBootcampRepository bootcampRepository)
        {
            _candidateRepository = candidateRepository;
            _bootcampRepository = bootcampRepository;
        }

        public List<CandidateViewModel> GetCandidates(DataTableParams dataTable)
        {
            List<CandidateViewModel> candidates = _candidateRepository.GetCandidates(dataTable)
                .Select(c => new CandidateViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    BirthDate = c.BirthDate,
                    BootcampClassId = c.BootcampClassId,
                    Domicile = c.Domicile,
                    Gender = c.Gender,
                    PhoneNumber = c.PhoneNumber
                }).ToList();

            return candidates;
        }

        public int CountCandidates()
        {
            return _candidateRepository.GetCandidates().Count();
        }

        public void InsertCandidate(CandidateViewModel vm)
        {
            var candidate = new Candidate
            {
                BootcampClassId = vm.BootcampClassId,
                Domicile = vm.Domicile,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Address = vm.Address,
                Gender = vm.Gender,
                PhoneNumber = vm.PhoneNumber,
                BirthDate = vm.BirthDate
            };
            _candidateRepository.InsertCandidate(candidate);
        }

        public void UpdateCandidate(CandidateViewModel vm)
        {
            Candidate newCandidate = new Candidate
            {
                Id = (int)vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Address = vm.Address,
                Gender = vm.Gender,
                PhoneNumber = vm.PhoneNumber,
                BirthDate = vm.BirthDate,
                Domicile = vm.Domicile,
                BootcampClassId=vm.BootcampClassId,
            };
            _candidateRepository.UpdateCandidate(newCandidate);
        }

        public List<SelectListItem> GetBootcamp()
        {
            List<BootcampClass> bootcampClasses = _bootcampRepository.GetBootcamp("Planned");
            List<SelectListItem> result = new List<SelectListItem>();

            foreach(BootcampClass bootcamp in bootcampClasses)
            {
                result.Add(new SelectListItem
                {
                    Text = bootcamp.Id.ToString(),
                    Value = bootcamp.Id.ToString()
                });
            }
            return result;
        }

        public CandidateViewModel GetCandidate(int id)
        {
            Candidate candidate = _candidateRepository.GetCandidate(id);
            CandidateViewModel vm = new CandidateViewModel
            {
                Id = id,
                BootcampList = GetBootcamp(),
                BootcampClassId = candidate.BootcampClassId,
                Address = candidate.Address,
                BirthDate = candidate.BirthDate,
                Gender = candidate.Gender,
                Domicile = candidate.Domicile,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                PhoneNumber = candidate.PhoneNumber
            };
            return vm;
        }
    }
}
