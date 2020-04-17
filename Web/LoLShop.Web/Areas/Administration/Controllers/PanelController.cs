namespace LoLShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Services.Data;
    using LoLShop.Web.Areas.Administration.ViewModels;
    using LoLShop.Web.ViewModels.Accounts;
    using LoLShop.Web.ViewModels.Administration;
    using LoLShop.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PanelController : AdministrationController
    {
        private readonly IAccountsService accountsService;
        private readonly IJobsService jobsService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public PanelController(IAccountsService accountsService, IJobsService jobsService, IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.accountsService = accountsService;
            this.jobsService = jobsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var account = this.accountsService.GetFirstAccount();
            var accountModel = new SellAccountInputModel();

            var application = this.jobsService.GetFirstApplication();
            var applicationModel = new ApplicationViewModel();

            if (account != null)
            {
                accountModel = new SellAccountInputModel
                {
                    ChampionsCount = account.ChampionsCount,
                    SkinsCount = account.SkinsCount,
                    BlueEssence = account.BlueEssence,
                    RiotPoints = account.RiotPoints,
                    Username = account.Username,
                    Password = account.Password,
                    Region = account.Region,
                };
            }

            if (application != null)
            {
                applicationModel = new ApplicationViewModel
                {
                    UserId = application.UserId,
                    Age = application.Age,
                    Name = application.Name,
                    Country = application.Country,
                    Position = application.Position,
                    Rank = application.Rank,
                    Champions = application.Champions,
                    Description = application.Description,
                };
            }

            var viewModel = new AdministrationViewModel
            {
                Account = accountModel,
                Application = applicationModel,
            };

            return this.View(viewModel);
        }

        [HttpGet("Administration/Panel/Reject/{Username}")]
        public async Task<IActionResult> Reject(string username)
        {
            await this.accountsService.RejectAccount(username);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("Administration/Panel/Approve/{Username}")]
        public async Task<IActionResult> Approve(string username)
        {
            await this.accountsService.ApproveAccount(username);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("Administration/Panel/JobApprove/{UserId}/{Position}")]
        public async Task<IActionResult> JobApprove(string userId, string position)
        {
            await this.jobsService.ApproveApplication(userId, position);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet("Administration/Panel/JobReject/{UserId}")]
        public async Task<IActionResult> JobReject(string userId)
        {
            await this.jobsService.RejectApplication(userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]

        public async Task<IActionResult> AddFunds(UsernameFundsInputModel inputModel)
        {
            var user = await this.userManager.FindByNameAsync(inputModel.Username);

            if (user == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.usersService.AddFundsAsync(user, inputModel.Funds);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]

        public async Task<IActionResult> PromoteUser(UsernamePromoteInputModel inputModel)
        {
            var user = await this.userManager.FindByNameAsync(inputModel.Username);

            if (user == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.userManager.AddToRoleAsync(user, inputModel.Role);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}