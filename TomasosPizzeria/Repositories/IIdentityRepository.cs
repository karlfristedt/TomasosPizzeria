using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TomasosPizzeria.Repositories
{
    public interface IIdentityRepository
    {
        IQueryable<IdentityRole> GetAllRoles();
    }
}
