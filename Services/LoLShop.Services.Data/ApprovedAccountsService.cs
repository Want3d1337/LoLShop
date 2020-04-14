
namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Accounts;

    public class ApprovedAccountsService : IApprovedAccountsService
    {
        private readonly IRepository<ApprovedAccount> approvedAccountsRepository;

        public ApprovedAccountsService(IRepository<ApprovedAccount> approvedAccountsRepository)
        {
            this.approvedAccountsRepository = approvedAccountsRepository;
        }

        public IEnumerable<AllAccountsRegionModel> GetAllAccountsRegion()
        {
            var accounts = this.approvedAccountsRepository.All().Select(x => new AllAccountsRegionModel
            {
                Region = x.Region,
            }).ToArray();

            return accounts;
        }
    }
}
