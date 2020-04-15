namespace LoLShop.Web.Controllers
{
    using System.Threading.Tasks;

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

        public UsersController(IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
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
            };

            viewModel.AvatarImageUrl = viewModel.AvatarImageUrl == null
                ? "https://res.cloudinary.com/lolshop-cloud/image/upload/v1586952164/avatar_yswuru.jpg"
                : user.AvatarImageUrl;

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new EditViewModel
            {
                UserId = user.Id,
                AvatarImageUrl = user.AvatarImageUrl,
                Rank = user.Rank,
                Champions = user.Champions,
            };

            viewModel.AvatarImageUrl = viewModel.AvatarImageUrl == null
                ? "https://res.cloudinary.com/lolshop-cloud/image/upload/v1586952164/avatar_yswuru.jpg"
                : user.AvatarImageUrl;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel inputModel)
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
