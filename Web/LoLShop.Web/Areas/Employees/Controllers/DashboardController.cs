namespace LoLShop.Web.Areas.Employees.Controllers
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.Areas.Employees.ViewModels;
    using LoLShop.Web.ViewModels.Coaching;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : EmployeesController
    {
        private readonly ICoachingService coachingService;
        private readonly IBoostingService boostingService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ICoachingService coachingService, IBoostingService boostingService, UserManager<ApplicationUser> userManager)
        {
            this.coachingService = coachingService;
            this.boostingService = boostingService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var coach = await this.userManager.GetUserAsync(this.User);
            var order = this.coachingService.GetFirstOrder(coach);
            var orderModel = new CoachOrderViewModel();

            var boostingOrders = this.boostingService.GetAllBoostOrders();

            if (order != null)
            {
                var buyer = await this.userManager.FindByIdAsync(order.BuyerId);

                orderModel = new CoachOrderViewModel
                {
                    CoachId = coach.Id,
                    ImageUrl = buyer.AvatarImageUrl,
                    GameName = order.GameName,
                    Region = order.Region,
                    DiscordTag = order.DiscordTag,
                    Hours = order.Hours,
                };
            }

            var viewModel = new DashboardViewModel
            {
                CoachOrder = orderModel,
                BoostOrders = boostingOrders,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> CoachOrderFinish()
        {
            var coach = await this.userManager.GetUserAsync(this.User);

            await this.coachingService.FinishFirstOrderAsync(coach);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("Employees/Dashboard/BoostOrderAccept/{Username}")]
        public async Task<IActionResult> BoostOrderAccept(string username)
        {
            var booster = await this.userManager.GetUserAsync(this.User);

            await this.boostingService.AcceptOrderAsync(booster, username);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("Employees/Dashboard/BoostOrderComplete/{Username}")]
        public async Task<IActionResult> BoostOrderComplete(string username)
        {
            var booster = await this.userManager.GetUserAsync(this.User);

            await this.boostingService.CompleteOrderAsync(username);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
