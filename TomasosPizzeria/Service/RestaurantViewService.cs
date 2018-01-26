using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models.ViewModels;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Service
{
    public class RestaurantViewService : IRestaurantViewService
    {
        private IRestaurantRepository _repository;

        public RestaurantViewService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public EditDishViewModel GetMatratt(int matrattId)
        {
            var matrattprodukter = _repository.GetProductsByMattrattId(matrattId).ToList();

            var matratt = _repository.GetMatrattById(matrattId);

            var productViewList = _repository.GetAllProducts().Select(v => new ProductViewModel
            {
                ProduktNamn = v.ProduktNamn,
                ProduktId = v.ProduktId
            }).ToList();

            foreach (var item in productViewList)
            {
                item.IsSelected = matrattprodukter.Exists(d => d.ProduktId == item.ProduktId);
            }

            var matrattView = new EditDishViewModel
            {
                MatrattNamn = matratt.MatrattNamn,
                MatrattTyp = matratt.MatrattTypNavigation.Beskrivning,
                Pris = matratt.Pris,
                Beskrivning = matratt.Beskrivning,
                MatrattId = matratt.MatrattId
            };

            matrattView.Produkter = productViewList;

            return matrattView;
        }

       
    }
}
