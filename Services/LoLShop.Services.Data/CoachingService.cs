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

        public CoachingService(UserManager<ApplicationUser> userManager, IRepository<CoachOrder> coachOrderRepository)
        {
            this.userManager = userManager;
            this.coachOrderRepository = coachOrderRepository;
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

        public async Task<IEnumerable<CoachViewModel>> GetAllCoaches()
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

    }
}
