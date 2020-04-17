namespace LoLShop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LoLShop.Common;
    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Users;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(IUsersService usersService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> AccountDetails()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new DetailsViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                AvatarImageUrl = user.AvatarImageUrl,
                ApprovedAccounts = user.ApprovedAccounts,
                Rank = user.Rank,
                Champions = user.Champions,
                Funds = user.Funds,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new EditInputModel
            {
                UserId = user.Id,
                AvatarImageUrl = user.AvatarImageUrl,
                Rank = user.Rank,
                Champions = user.Champions,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Edit));
            }

            inputModel.UserId = this.userManager.GetUserId(this.User);

            await this.usersService.UpdateAsync(inputModel);

            return this.RedirectToAction(nameof(this.AccountDetails));
        }
    }
}
