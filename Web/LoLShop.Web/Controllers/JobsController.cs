namespace LoLShop.Web.Controllers
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Jobs;
    using LoLShop.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class JobsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobsService jobsService;
        private readonly IUsersService usersService;

        public JobsController(UserManager<ApplicationUser> userManager, IJobsService jobsService, IUsersService usersService)
        {
            this.userManager = userManager;
            this.jobsService = jobsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            var userId = this.userManager.GetUserId(this.User);

            this.ViewBag.IsUserApplied = this.jobsService.IsUserApplied(userId);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplicationInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.userManager.GetUserId(this.User);

            var editModel = new EditInputModel
            {
                UserId = userId,
                Champions = inputModel.Champions,
                Rank = inputModel.Rank,
            };

            await this.jobsService.CreateAsync(inputModel, userId);

            await this.usersService.UpdateAsync(editModel);

            return this.Redirect("/");
        }
    }
}
