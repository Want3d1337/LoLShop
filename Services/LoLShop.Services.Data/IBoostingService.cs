namespace LoLShop.Services.Data
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Boosting;

    public interface IBoostingService
    {
        Task AddAsync(PurchaseInputModel inputModel);

        BoostOrderViewModel[] GetAllBoostOrders();

        Task AcceptOrderAsync(ApplicationUser booster, string username);

        Task CompleteOrderAsync(string username);
    }
}
