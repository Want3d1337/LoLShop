namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Common;
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
        private readonly IAccountsService accountsService;
        private readonly IApprovedAccountsService approvedAccountsService;
        private readonly IUsersService usersService;

        public AccountsController(UserManager<ApplicationUser> userManager, IAccountsService accountsService, IApprovedAccountsService approvedAccountsService, IUsersService usersService)
        {
            this.userManager = userManager;
            this.accountsService = accountsService;
            this.approvedAccountsService = approvedAccountsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult All()
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

            await this.accountsService.CreateAsync(inputModel, user);

            return this.Redirect("/Accounts/All");
        }

        [HttpGet("Accounts/PurchaseAccount/{region}")]
        public async Task<IActionResult> PurchaseAccount(Regions region)
        {
            var buyer = await this.userManager.GetUserAsync(this.User);

            var price = GlobalConstants.AccountPrice;

            var accountsCount = this.approvedAccountsService.GetAllAccountsRegion().Where(x => x.Region == region).Count();

            if (buyer.Funds < price || accountsCount == 0)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var account = await this.approvedAccountsService.PurchaseAccountAsync(region);

            var seller = await this.userManager.FindByIdAsync(account.SellerId);

            await this.usersService.AddFundsAsync(seller, price);

            await this.usersService.RemoveFundsAsync(buyer, price);

            var viewModel = new PurchaseAccountViewModel
            {
                Region = account.Region,
                Username = account.Username,
                Password = account.Password,
            };

            return this.View(viewModel);
        }
    }
}
