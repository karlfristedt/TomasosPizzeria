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

        public IQueryable<Matratt> GetMatratter()
        {
            return context.Matratt;
        }

        public IQueryable<Kund> GetCustomers()
        {
            return context.Kund;
        }
        public IQueryable<Bestallning> GetOrders()
        {
            return context.Bestallning;
        }
        public void SaveOrder(Bestallning best)
        {
            context.Bestallning.Add(best);
            context.SaveChanges();
        }
    }
}
