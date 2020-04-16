namespace LoLShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Coaching;

    using Microsoft.AspNetCore.Identity;

    public class CoachingService : ICoachingService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<CoachOrder> coachOrderRepository;
        private readonly IUsersService usersService;

        public CoachingService(UserManager<ApplicationUser> userManager, IRepository<CoachOrder> coachOrderRepository, IUsersService usersService)
        {
            this.userManager = userManager;
            this.coachOrderRepository = coachOrderRepository;
            this.usersService = usersService;
        }

        public async Task AddAsync(OrderInputModel inputModel)
        {
            var coachOrder = new CoachOrder
            {
                BuyerId = inputModel.BuyerId,
                CoachId = inputModel.CoachId,
                GameName = inputModel.GameName,
                Region = inputModel.Region,
                DiscordTag = inputModel.DiscordTag,
                Hours = inputModel.Hours,
            };

            await this.coachOrderRepository.AddAsync(coachOrder);
            await this.coachOrderRepository.SaveChangesAsync();
        }

        public async Task FinishFirstOrderAsync(ApplicationUser user)
        {
            var order = this.coachOrderRepository.All().FirstOrDefault(x => x.CoachId == user.Id);

            var price = order.Hours * GlobalConstants.CoachingPricePerHour;

            await this.usersService.AddFundsAsync(user, price);

            this.coachOrderRepository.Delete(order);

            await this.coachOrderRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoachViewModel>> GetAllCoachesAsync()
        {
            var users = await this.userManager.GetUsersInRoleAsync(GlobalConstants.CoachRoleName);

            var models = users.Select(x => new CoachViewModel
            {
                UserId = x.Id,
                Username = x.UserName,
                Rank = x.Rank,
                Champions = x.Champions,
                ImageUrl = x.AvatarImageUrl,
            }).ToArray();

            return models;
        }

        public CoachOrder GetFirstOrder(ApplicationUser coach)
        {
            return this.coachOrderRepository.All().FirstOrDefault(x => x.CoachId == coach.Id);
        }
    }
}
