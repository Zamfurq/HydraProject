using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models;
using Hydra.Presentation.Web.Models.DataTable;

namespace Hydra.Presentation.Web.Services
{
    public class BootcampServices
    {
        private readonly IBootcampRepository _bootcampRepository;

        public BootcampServices(IBootcampRepository bootcampRepository)
        {
            _bootcampRepository = bootcampRepository;
        }

        public List<BootcampViewModel> GetAllBootcamp(DataTableParams dataTable, string type)
        {
            List<BootcampViewModel> bootcamps = _bootcampRepository.GetBootcamp(dataTable, type)
                .Select(i => new BootcampViewModel
                    {
                        Description = i.Description,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        Id = i.Id,
                        Progress = i.Progress,
                    }).ToList();
            return bootcamps;
        }

        public int CountBootcamp(string type)
        {
            return _bootcampRepository.GetBootcamp(type).Count();
        }

        public void InsertBootcamp(BootcampViewModel vm)
        {
            var bootcamp = new BootcampClass
            {
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Progress = 1
            };
            _bootcampRepository.InsertBootcamp(bootcamp);

        }

        public void UpdateBootcamp(BootcampViewModel vm) {
            BootcampClass bootcamp = new BootcampClass {
                Id = (int)vm.Id,
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Progress = vm.Progress
            };
            _bootcampRepository.UpdateBootcamp(bootcamp);
        }

        public BootcampViewModel GetBootcamp(int id)
        {
            BootcampClass bootcamp = _bootcampRepository.GetBootcamp(id);
            BootcampViewModel vm = new BootcampViewModel
            {
                Id = id,
                Description = bootcamp.Description,
                StartDate = bootcamp.StartDate,
                EndDate = bootcamp.EndDate,
                Progress = bootcamp.Progress
            };
            return vm;
        }
    }
}
