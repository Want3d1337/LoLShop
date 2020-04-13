namespace LoLShop.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Accounts;

    public class AccountsService : IAccountsService
    {
        private readonly IRepository<Account> accountsRepository;

        public AccountsService(IRepository<Account> accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }

        public async Task CreateAsync(SellAccountInputModel inputModel, ApplicationUser user)
        {
            var account = new Account
            {
                ChampionsCount = inputModel.ChampionsCount,
                SkinsCount = inputModel.SkinsCount,
                BlueEssence = inputModel.BlueEssence,
                RiotPoints = inputModel.RiotPoints,
                SellerId = user.Id,
                Username = inputModel.Username,
                Password = inputModel.Password,
                Region = inputModel.Region,
            };

            await this.accountsRepository.AddAsync(account);
            await this.accountsRepository.SaveChangesAsync();
        }
    }
}
