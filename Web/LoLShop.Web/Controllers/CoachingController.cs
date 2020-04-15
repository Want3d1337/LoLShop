namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoLShop.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class CoachingController : BaseController
    {
        private readonly ICoachingService coachingService;

        public CoachingController(ICoachingService coachingService)
        {
            this.coachingService = coachingService;
        }

        [HttpGet]
        public async Task<IActionResult> Coaches()
        {
            var viewModel = await this.coachingService.GetAllCoaches();

            return this.View(viewModel);
        }

        [HttpGet("/Coaching/Order/{UserId}")]
        public async Task<IActionResult> Order()
        {
            return this.View();
        }
    }
}
