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
        private readonly IApprovedAccountsService approvedAccountsService;

        public AccountsController(UserManager<ApplicationUser> userManager, IAccountsService accountService, IApprovedAccountsService approvedAccountsService)
        {
            this.userManager = userManager;
            this.accountService = accountService;
            this.approvedAccountsService = approvedAccountsService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var accounts = this.approvedAccountsService.GetAllAccountsRegion();
            return this.View(accounts);
        }

        [HttpGet]
        public IActionResult Sell()
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
