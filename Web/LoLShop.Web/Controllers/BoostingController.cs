namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Boosting;

    using Microsoft.AspNetCore.Mvc;

    public class BoostingController : BaseController
    {
        private readonly IBoostingService boostingService;

        public BoostingController(IBoostingService boostingService)
        {
            this.boostingService = boostingService;
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseInputModel inputModel)
        {
            await this.boostingService.AddAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
