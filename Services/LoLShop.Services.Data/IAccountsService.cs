﻿namespace LoLShop.Services.Data
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Accounts;

    public interface IAccountsService
    {
        Task CreateAsync(SellAccountInputModel inputModel, ApplicationUser user);

        Account GetFirstAccount();

        Task RejectAccountAsync(string username);

        Task ApproveAccountAsync(string username);
    }
}
