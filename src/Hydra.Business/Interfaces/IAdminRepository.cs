using Hydra.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Business.Interfaces
{
    public interface IAdminRepository
    {
        public User? GetUser(string username);
        public void RegisterUser(User user);

        public Role GetRole(string roleName);

        public List<Role> GetRoles();
    }
}
