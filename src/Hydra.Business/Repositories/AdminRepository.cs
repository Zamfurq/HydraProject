using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Business.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        private readonly HydraContext _dbContext;

        public AdminRepository( HydraContext dbContext )
        {
            _dbContext = dbContext;
        }
        public User? GetUser(string username)
        {
            var userAdmin = from user in _dbContext.Users
                       where user.Username == username
                       select user;
            var userRoles = from role in _dbContext.Roles
                            where role.Usernames.Any(user => user.Username == username)
                            select role;
            User theUser = userAdmin.FirstOrDefault();
            theUser.Roles.Add(userRoles.FirstOrDefault());
            return theUser;
        }

        public void RegisterUser(User user)
        {
            _dbContext.Users.Add( user );
            _dbContext.SaveChanges();
        }

        public List<Role> GetRoles()
        {
            return _dbContext.Roles.Where(role => role.Name != "Candidate" && role.Name != "TrainingManager").ToList();
        }

        public Role GetRole(string  roleName)
        {
            return _dbContext.Roles.Where(role => role.Name == roleName).FirstOrDefault() ?? null;
        }
    }
}
