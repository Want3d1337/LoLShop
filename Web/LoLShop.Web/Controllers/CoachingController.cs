namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        private readonly UserManager<ApplicationUser> userManager;

        public CoachingController(ICoachingService coachingService, UserManager<ApplicationUser> userManager)
        {
            this.coachingService = coachingService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Coaches()
        {
            var viewModel = await this.coachingService.GetAllCoaches();

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

            var buyerId = this.userManager.GetUserId(this.User);

            inputModel.BuyerId = buyerId;

            await this.coachingService.AddAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
