using System.Linq;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Repositories
{
    public interface IMatrattRepository
    {
        IQueryable<Matratt> GetAllMatratter();
        IQueryable<Kund> GetAllCustomers();
        IQueryable<Bestallning> GetAllOrders();

        IQueryable<Bestallning> GetOrdersById(int id);
        IQueryable<Kund> GetCustomersById(int id);
        IQueryable<Matratt> GetMatratterById(int id);
        
   

        void SaveOrder(Bestallning bestallning);

        //IQueryable<Produkt> Produkter { get; }
        //IQueryable<MatrattProdukt> MatrattProdukt { get; }
    }
}
