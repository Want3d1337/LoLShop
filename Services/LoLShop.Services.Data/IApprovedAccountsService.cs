namespace LoLShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Accounts;

    public interface IApprovedAccountsService
    {
        IEnumerable<AllAccountsRegionModel> GetAllAccountsRegion();

        Task<ApprovedAccount> PurchaseAccountAsync(Regions region);
    }
}
