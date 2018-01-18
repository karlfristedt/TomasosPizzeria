using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public class UserRepository : IUsers
    {
        private TomasosIdentityDBContext identitycontext;

        public UserRepository(TomasosIdentityDBContext ctx) { identitycontext = ctx; }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return identitycontext.Users;
        }
    }
}
