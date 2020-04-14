namespace LoLShop.Web.Controllers
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class JobsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJobsService jobsService;

        public JobsController(UserManager<ApplicationUser> userManager, IJobsService jobsService)
        {
            this.userManager = userManager;
            this.jobsService = jobsService;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplicationInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.jobsService.CreateAsync(inputModel, user);

            return this.Redirect("/");
        }
    }
}
