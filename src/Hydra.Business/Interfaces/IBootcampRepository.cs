using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Business.Interfaces
{
    public interface IBootcampRepository
    {
        public List<BootcampClass> GetBootcamp(DataTableParams dataTable, string type);

        public List<BootcampClass> GetBootcamp(string type);

        public BootcampClass GetBootcamp(int bootcampId);

        public void InsertBootcamp(BootcampClass bootcampClass);

        public void UpdateBootcamp(BootcampClass bootcampClass);
    }
}
