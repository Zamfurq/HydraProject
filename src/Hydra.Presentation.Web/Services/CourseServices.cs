using Hydra.Business.Interfaces;
using Hydra.Presentation.Web.Models;
using Hydra.Presentation.Web.Models.DataTable;

namespace Hydra.Presentation.Web.Services
{
    public class CourseServices
    {
        private readonly ICourseRepository _courseRepository;

        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<CourseViewModel> GetCourses(DataTableParams dataTable, int bootcampId)
        {
            List<CourseViewModel> courses = _courseRepository.GetCourses(dataTable, bootcampId)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    StartDate = c.StartDate,
                    CourseName = _courseRepository.GetSkill(c.SkillId).Name,
                    EndDate = c.EndDate,
                    TrainerName = _courseRepository.GetTrainer(c.TrainerId).FirstName + " " + _courseRepository.GetTrainer(c.TrainerId).LastName,
                }).ToList();
            return courses;
        }

        public int CountCourses(int bootcampId)
        {
            return _courseRepository.GetCourses(bootcampId).Count();
        }
    }
}
