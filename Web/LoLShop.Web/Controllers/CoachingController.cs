namespace LoLShop.Web.Controllers
{
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Coaching;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CoachingController : BaseController
    {
        private readonly ICoachingService coachingService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoachingController(ICoachingService coachingService, IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.coachingService = coachingService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Coaches()
        {
            var viewModel = await this.coachingService.GetAllCoachesAsync();

            return this.View(viewModel);
        }

        [HttpGet("/Coaching/Order/{coachId}")]
        public IActionResult Order(string coachId)
        {
            var inputModel = new OrderInputModel { CoachId = coachId };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var price = GlobalConstants.CoachingPricePerHour * inputModel.Hours;

            if (user.Funds < price)
            {
                return this.View();
            }

            inputModel.BuyerId = user.Id;

            await this.usersService.RemoveFundsAsync(user, price);
            await this.coachingService.AddAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
