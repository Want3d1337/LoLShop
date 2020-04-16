namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task AcceptOrderAsync(ApplicationUser booster, string username)
        {
            var order = this.boostOrdersRepository.All().FirstOrDefault(x => x.Username == username);

            order.BoosterId = booster.Id;

            this.boostOrdersRepository.Update(order);
            await this.boostOrdersRepository.SaveChangesAsync();
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

        public BoostOrderViewModel[] GetAllBoostOrders()
        {
            var models = this.boostOrdersRepository.All().Select(x => new BoostOrderViewModel
            {
                BoosterId = x.BoosterId,
                CurrentRank = x.CurrentRank,
                Ranks = x.Ranks,
                Username = x.Username,
                Password = x.Password,
            }).ToArray();

            return models;
        }
    }
}
