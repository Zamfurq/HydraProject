using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Hydra.Business.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly HydraContext _dbContext;
        public CourseRepository(HydraContext dbContext) {
            _dbContext = dbContext;
        }

        public List<Course> GetCourses(DataTableParams dataTable, int bootcampId)
        {
            var query = from course in _dbContext.Courses
                        where course.BootcampClassId == bootcampId
                        select course;
            string ? sortColumnDir = dataTable.Order?[0].Dir;
            string sortColumn = dataTable.Columns[dataTable.Order[0].Column]?.Name;
            int pageSize = dataTable.Length != null ? Convert.ToInt32(dataTable.Length) : 0;
            int skip = dataTable.Start != null ? Convert.ToInt32(dataTable.Start) : 0;
            if (!string.IsNullOrEmpty(dataTable.Search.Value))
            {
                var searchValue = dataTable.Search.Value.ToLower();
                query = query.Where(c =>
                    c.SkillId.ToLower().Contains(searchValue.ToLower()) ||
                    c.StartDate.ToString().ToLower().Contains(searchValue.ToLower()) ||
                    c.EndDate.ToString().ToLower().Contains(searchValue.ToLower()) ||
                    c.TrainerId.ToString().ToLower().Contains(searchValue.ToLower()));
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDir);
            }


            return query.Skip(skip).Take(pageSize).ToList(); 
        }

        public List<Course> GetCourses(int bootcampId)
        {
            return _dbContext.Courses.Where(courses => courses.BootcampClassId == bootcampId).ToList();
        }

        public Skill GetSkill(string skillId)
        {
            return _dbContext.Skills.Where(skill => skill.Id == skillId).FirstOrDefault();
        }

        public Trainer GetTrainer(int trainerId)
        {
            return _dbContext.Trainers.Where(trainer => trainer.Id == trainerId).FirstOrDefault();
        }
    }
}
