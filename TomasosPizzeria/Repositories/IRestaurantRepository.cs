using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
        Kund GetCustomerByUserName(string username);

        void DeleteCustomer(string username);
        void AddCustomer(Kund user);
        void UpdateCustomer(Kund user);
        void SaveOrder(string username);

    }
}
