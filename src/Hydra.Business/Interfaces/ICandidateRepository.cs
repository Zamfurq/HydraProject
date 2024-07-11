using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Business.Interfaces
{
    public interface ICandidateRepository
    {
        public List<Candidate> GetCandidates(DataTableParams dataTable);

        public List<Candidate> GetCandidates();

        public Candidate GetCandidate(int id);

        public void InsertCandidate(Candidate candidate);

        public void UpdateCandidate(Candidate candidate);
    }
}
