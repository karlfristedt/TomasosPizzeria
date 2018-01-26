using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Entities;
using TomasosPizzeria.Models;
using TomasosPizzeria.Models.ViewModels;


namespace TomasosPizzeria.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private TomasosDBContext _context;

        private Kundvagn _kundvagn;

        public RestaurantRepository(TomasosDBContext ctx, Kundvagn kundvagnService)
        {
            _context = ctx;
            _kundvagn = kundvagnService;
        }



        public IQueryable<Matratt> GetAllMatratter()
        {
            return _context.Matratt;
        }

        public IQueryable<Kund> GetAllCustomers()
        {
            return _context.Kund;
        }

        public IQueryable<Bestallning> GetAllOrders()
        {
            return _context.Bestallning;
        }

        public IQueryable<MatrattTyp> GetAllMatrattTyp()
        {
            return _context.MatrattTyp;
        }

        public IQueryable<Produkt> GetAllProducts()
        {
            return _context.Produkt;
        }

        public Bestallning GetOrderById(int id)
        {
            var order = _context.Bestallning.SingleOrDefault(o => o.BestallningId == id);
            return order;
        }

        public Kund GetCustomerById(int id)
        {
            var cust = _context.Kund.SingleOrDefault(k => k.KundId == id);
            return cust;
        }
        public Matratt GetMatrattById(int id)
        {
            var matratt = _context.Matratt
                .Include(a => a.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .Include(t => t.MatrattTypNavigation)
                .SingleOrDefault(m => m.MatrattId == id);
            
            return matratt;
        }
        
        public void SaveOrder(string username)
        {
            Kundvagn vagn = _kundvagn;

            Bestallning nybest = new Bestallning();
            nybest.BestallningDatum = DateTime.Now;
            nybest.Totalbelopp = vagn.ComputeTotalValue();
            nybest.KundId = _context.Kund.SingleOrDefault(u => u.AnvandarNamn == username).KundId;
            

            _context.Add(nybest);
            _context.SaveChanges();

            var senastebest = _context.Bestallning.OrderByDescending(id => id.BestallningId).First();


            var bestmatrattjointables = from a in vagn.GetOrderrader()
                              select new BestallningMatratt
                              {
                                  Antal = a.Antal,
                                  BestallningId = senastebest.BestallningId,
                                  MatrattId = a.Matratt.MatrattId
                              };

            _context.AddRange(bestmatrattjointables);
            _context.SaveChanges();
            _kundvagn.Clear();

        }

        public void AddCustomer(Kund user)
        {
            if (_context.Kund.Any(c => c.AnvandarNamn == user.AnvandarNamn))
            {
                return;
            }
            _context.Add(user);
            _context.SaveChanges();
        }

        public Kund GetCustomerByUserName(string username)
        {
            return _context.Kund.SingleOrDefault(c => c.AnvandarNamn == username);
        }

        private IQueryable<Bestallning> GetOrdersByUserName(string username)
        {
            var customerorders = _context.Bestallning.Where(c => c.Kund.AnvandarNamn == username);
            return customerorders;
        }
        private IQueryable<BestallningMatratt> GetBestallMatrattByUserName(string username)
        {
            var mattrattbestallning = _context.BestallningMatratt.Where(c => c.Bestallning.Kund.AnvandarNamn == username);
            return mattrattbestallning;
        }
        public void UpdateCustomer(Kund user)
        {

        }

        public void DeleteCustomer(string username)
        {
            var user = GetCustomerByUserName(username);
            var userorders = GetOrdersByUserName(username);
            var matrattprodukter = GetBestallMatrattByUserName(username);
            _context.BestallningMatratt.RemoveRange(matrattprodukter);
            _context.Bestallning.RemoveRange(userorders);
            _context.Kund.Remove(user);
            _context.SaveChanges();
        }

        public bool ChangeOrderStatus(int id, bool status)
        {
            var order = GetOrderById(id);
            if (order.Levererad != status)
            {
                order.Levererad = status;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            var matrattbest = _context.BestallningMatratt.Where(b => b.BestallningId == id);
            _context.BestallningMatratt.RemoveRange(matrattbest);
            _context.Bestallning.Remove(order);
            _context.SaveChanges();
        }

        public void UpdateMatrattProdukter(EditDishViewModel model)
        {
            var oldmattrattprodukt = _context.MatrattProdukt.Where(prod => prod.MatrattId == model.MatrattId);
            var matrattProdukts = model.Produkter.Where(prod => prod.IsSelected == true).Select(s => new MatrattProdukt
            {
                MatrattId = model.MatrattId,
                ProduktId = s.ProduktId,
            }).AsQueryable().Include(x => x.Matratt).Include(y => y.Produkt);

            _context.MatrattProdukt.RemoveRange(oldmattrattprodukt);
            _context.SaveChanges();
            _context.MatrattProdukt.AddRange(matrattProdukts);
            _context.SaveChanges();
        }

        public void UpdateMatratt(EditDishViewModel model)
        {
            var matratt = GetMatrattById(model.MatrattId);
            matratt.Beskrivning = model.Beskrivning;
            matratt.MatrattNamn = model.MatrattNamn;
            matratt.Pris = model.Pris;
            _context.SaveChanges();
        }


        public IQueryable<Produkt> GetProductsByMattrattId(int matrattId)
        {
            var matratt = GetMatrattById(matrattId);
            var fg = matratt.MatrattProdukt.Where(x => x.MatrattId == matrattId).Select(y => y.Produkt).AsQueryable();
            return fg;
        }
    }
}
