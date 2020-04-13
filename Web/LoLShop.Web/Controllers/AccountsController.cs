namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Accounts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AccountsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountsService accountService;

        public AccountsController(UserManager<ApplicationUser> userManager, IAccountsService accountService)
        {
            this.userManager = userManager;
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Sell()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Sell(SellAccountInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.accountService.CreateAsync(inputModel, user);

            return this.Redirect("/Accounts/All");
        }
    }
}
