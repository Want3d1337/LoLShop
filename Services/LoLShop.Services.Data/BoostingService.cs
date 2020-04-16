namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Boosting;

    public class BoostingService : IBoostingService
    {
        private readonly IRepository<BoostOrder> boostOrdersRepository;

        public BoostingService(IRepository<BoostOrder> boostOrdersRepository)
        {
            this.boostOrdersRepository = boostOrdersRepository;
        }

        public async Task AddAsync(PurchaseInputModel inputModel)
        {
            var boostOrder = new BoostOrder
            {
                CurrentRank = inputModel.CurrentRank,
                Ranks = inputModel.Ranks,
                Username = inputModel.Username,
                Password = inputModel.Password,
            };

            await this.boostOrdersRepository.AddAsync(boostOrder);
            await this.boostOrdersRepository.SaveChangesAsync();
        }
    }
}
