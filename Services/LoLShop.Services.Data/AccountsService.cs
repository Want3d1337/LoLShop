namespace LoLShop.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Accounts;

    public class AccountsService : IAccountsService
    {
        private readonly IRepository<Account> accountsRepository;
        private readonly IRepository<ApprovedAccount> approvedAccountsRepository;

        public AccountsService(IRepository<Account> accountsRepository, IRepository<ApprovedAccount> approvedAccountsRepository)
        {
            this.accountsRepository = accountsRepository;
            this.approvedAccountsRepository = approvedAccountsRepository;
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

        public async Task RejectAccountAsync(string username)
        {
            var account = this.accountsRepository.All().FirstOrDefault(x => x.Username == username);
            this.accountsRepository.Delete(account);
            await this.accountsRepository.SaveChangesAsync();
        }

        public async Task ApproveAccountAsync(string username)
        {
            var account = this.accountsRepository.All().FirstOrDefault(x => x.Username == username);
            var approvedAccount = new ApprovedAccount
            {
                SellerId = account.SellerId,
                Username = account.Username,
                Password = account.Password,
                Region = account.Region,
            };

            this.accountsRepository.Delete(account);
            await this.accountsRepository.SaveChangesAsync();

            await this.approvedAccountsRepository.AddAsync(approvedAccount);
            await this.approvedAccountsRepository.SaveChangesAsync();
        }

        public Account GetFirstAccount()
        {
            return this.accountsRepository.All().FirstOrDefault();
        }
    }
}
