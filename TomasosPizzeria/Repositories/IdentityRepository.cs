using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public class IdentityRepository:IIdentityRepository
    {
        private TomasosIdentityDBContext _context;

        public IdentityRepository(TomasosIdentityDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            return _context.Roles;
        }
    }
}
