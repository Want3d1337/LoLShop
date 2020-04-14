
namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Accounts;

    public interface IAccountsService
    {
        Task CreateAsync(SellAccountInputModel inputModel, ApplicationUser user);

        Account GetFirstAccount();
    }
}
