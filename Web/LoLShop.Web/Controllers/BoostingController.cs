namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Boosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BoostingController : BaseController
    {
        private readonly IBoostingService boostingService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public BoostingController(IBoostingService boostingService, IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.boostingService = boostingService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var price = GlobalConstants.BoostingPricePerRank * inputModel.Ranks;

            var isUsernameOrdered = this.boostingService.GetAllBoostOrders().Any(x => x.Username == inputModel.Username);

            if (user.Funds < price || isUsernameOrdered)
            {
                return this.View();
            }

            await this.usersService.RemoveFundsAsync(user, price);
            await this.boostingService.AddAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
