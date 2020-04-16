namespace LoLShop.Web.Areas.Employees.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Coaching;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : EmployeesController
    {
        private readonly ICoachingService coachingService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ICoachingService coachingService, UserManager<ApplicationUser> userManager)
        {
            this.coachingService = coachingService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var coach = await this.userManager.GetUserAsync(this.User);

            var order = this.coachingService.GetFirstOrder(coach);

            var viewModel = new OrderViewModel();

            if (order != null)
            {
                var buyer = await this.userManager.FindByIdAsync(order.BuyerId);

                viewModel = new OrderViewModel
                {
                    CoachId = coach.Id,
                    ImageUrl = buyer.AvatarImageUrl,
                    GameName = order.GameName,
                    Region = order.Region,
                    DiscordTag = order.DiscordTag,
                    Hours = order.Hours,
                };
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> CoachOrderFinish()
        {
            var coach = await this.userManager.GetUserAsync(this.User);

            await this.coachingService.FinishFirstOrderAsync(coach);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
