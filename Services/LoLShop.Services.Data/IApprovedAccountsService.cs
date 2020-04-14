namespace LoLShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoLShop.Web.ViewModels.Accounts;

    public interface IApprovedAccountsService
    {
        IEnumerable<AllAccountsRegionModel> GetAllAccountsRegion();
    }
}
