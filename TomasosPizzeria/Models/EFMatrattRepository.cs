using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models
{
    public class EFMatrattRepository : IMatrattRepository
    {
        private TomasosContext context;

        public EFMatrattRepository(TomasosContext ctx) { context = ctx; }

        public IQueryable<Matratt> Matratter => context.Matratt;

    }
}
