using TomasosPizzeria.Models.ViewModels;

namespace TomasosPizzeria.Service
{
    public interface IRestaurantViewService
    {
        EditDishViewModel GetMatratt(int matrattId);
    }
}