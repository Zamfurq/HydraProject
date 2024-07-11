using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hydra.Business.Repositories
{
    public class BootcampRepository : IBootcampRepository
    {
        private readonly HydraContext _dbContext;

        public BootcampRepository(HydraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BootcampClass> GetBootcamp(DataTableParams dataTable, string type)
        {
            IQueryable<BootcampClass> query;
            if (type == "Planned")
            {
                query = from bootcamp in _dbContext.BootcampClasses
                        where bootcamp.Progress == 1
                        select bootcamp;
            } else if (type == "Completed")
            {
                query = from bootcamp in _dbContext.BootcampClasses
                        where bootcamp.Progress == 3
                        select bootcamp;
            }
            else if (type == "Active")
            {
                query = from bootcamp in _dbContext.BootcampClasses
                        where bootcamp.Progress == 2
                        select bootcamp;
            }
            else
            {
                query = from bootcamp in _dbContext.BootcampClasses
                        select bootcamp;
            }
            
            string? sortColumnDir = dataTable.Order?[0].Dir;
            string sortColumn = dataTable.Columns[dataTable.Order[0].Column]?.Name;
            int pageSize = dataTable.Length != null ? Convert.ToInt32(dataTable.Length) : 0;
            int skip = dataTable.Start != null ? Convert.ToInt32(dataTable.Start) : 0;
            if (!string.IsNullOrEmpty(dataTable.Search.Value))
            {
                var searchValue = dataTable.Search.Value.ToLower();
                query = query.Where(c =>
                    c.Id.ToString().ToLower().Contains(searchValue.ToLower()) ||
                    c.Description.ToString().ToLower().Contains(searchValue.ToLower()) ||
                    c.StartDate.ToString().ToLower().Contains(searchValue.ToLower()) ||
                    c.EndDate.ToString().ToLower().Contains(searchValue.ToLower()));
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDir);
            }


            return query.Skip(skip).Take(pageSize).ToList();
        }

        public List<BootcampClass> GetBootcamp(string type)
        {
            if (type == "Planned")
            {
                return _dbContext.BootcampClasses.Where(bootcamp => bootcamp.Progress == 1).ToList();
            }
            else if (type == "Active")
            {
                return _dbContext.BootcampClasses.Where(bootcamp => bootcamp.Progress == 2).ToList();
            }
            else if (type == "Completed")
            {
                return _dbContext.BootcampClasses.Where(bootcamp => bootcamp.Progress == 3).ToList();
            }
            else
            {
                return _dbContext.BootcampClasses.ToList();
            }
            
        }

        
        public void InsertBootcamp(BootcampClass bootcampClass)
        {
            _dbContext.BootcampClasses.Add(bootcampClass);
            _dbContext.SaveChanges();
        }

        public void UpdateBootcamp(BootcampClass bootcampClass)
        {
            if(bootcampClass.Id == 0)
            {
                throw new ArgumentNullException("This ID did not exist");
            }
            _dbContext.BootcampClasses.Update(bootcampClass);
            _dbContext.SaveChanges();
        }

        public BootcampClass GetBootcamp(int bootcampId)
        {
            return _dbContext.BootcampClasses.Where(bootcamp => bootcamp.Id == bootcampId).FirstOrDefault();
        }
    }
}
