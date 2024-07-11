using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models.DataTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Business.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly HydraContext _dbContext;

        public CandidateRepository(HydraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Candidate> GetCandidates(DataTableParams dataTable)
        {
            var query = from candidate in _dbContext.Candidates
                        select candidate;
            string? sortColumnDir = dataTable.Order?[0].Dir;
            string sortColumn = dataTable.Columns[dataTable.Order[0].Column]?.Name;
            int pageSize = dataTable.Length != null ? Convert.ToInt32(dataTable.Length) : 0;
            int skip = dataTable.Start != null ? Convert.ToInt32(dataTable.Start) : 0;
            if (!string.IsNullOrEmpty(dataTable.Search.Value))
            {
                var searchValue = dataTable.Search.Value.ToLower();
                query = query.Where(c =>
                    c.FirstName.ToLower().Contains(searchValue.ToLower()) ||
                    c.BootcampClassId.ToString().ToLower().Contains(searchValue.ToLower()) ||
                    c.PhoneNumber.ToLower().Contains(searchValue.ToLower()) ||
                    c.Domicile.ToLower().Contains(searchValue.ToLower()));
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDir);
            }


            return query.Skip(skip).Take(pageSize).ToList();
        }

        public List<Candidate> GetCandidates()
        {
            return _dbContext.Candidates.ToList();
        }


        public void InsertCandidate(Candidate candidate)
        {
            _dbContext.Candidates.Add(candidate);
            _dbContext.SaveChanges();
        }

        public void UpdateCandidate(Candidate candidate) { 
            if(candidate.Id == 0)
            {
                throw new ArgumentNullException("This ID did not exist");
            }
            _dbContext.Candidates.Update(candidate);
            _dbContext.SaveChanges();
        }

        public Candidate GetCandidate(int id)
        {
            return _dbContext.Candidates.Where(candidate => candidate.Id == id).FirstOrDefault();
        }
    }
}
