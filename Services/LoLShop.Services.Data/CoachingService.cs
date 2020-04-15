namespace LoLShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Coaching;

    using Microsoft.AspNetCore.Identity;

    public class CoachingService : ICoachingService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CoachingService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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
