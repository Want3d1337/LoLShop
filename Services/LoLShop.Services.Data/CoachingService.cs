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

        public Task<IEnumerable<CoachViewModel>> GetAllCoaches()
        {
            throw new System.NotImplementedException();
        }
    }
}
