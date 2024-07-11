using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Business.Interfaces
{
    public interface ICourseRepository
    {
        public List<Course> GetCourses(DataTableParams dataTable, int bootcampId);

        public List<Course> GetCourses(int bootcampId);

        public Skill GetSkill(string skillId);

        public Trainer GetTrainer(int trainerId);
    }
}
