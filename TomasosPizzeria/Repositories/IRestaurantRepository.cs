using System.Linq;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Repositories
{
    public interface IRestaurantRepository
    {
        IQueryable<Matratt> GetAllMatratter();
        IQueryable<Kund> GetAllCustomers();
        IQueryable<Bestallning> GetAllOrders();
        IQueryable<MatrattTyp> GetAllMatrattTyp();

        Bestallning GetOrderById(int id);
        Kund GetCustomerById(int id);
        Matratt GetMatrattById(int id);

        void AddCustomer(Kund user);
        void SaveOrder();

        //IQueryable<Produkt> Produkter { get; }
        //IQueryable<MatrattProdukt> MatrattProdukt { get; }
    }
}
