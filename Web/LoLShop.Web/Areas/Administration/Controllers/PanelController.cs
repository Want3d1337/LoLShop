namespace LoLShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Services.Data;
    using LoLShop.Web.ViewModels.Accounts;
    using Microsoft.AspNetCore.Mvc;

    public class PanelController : AdministrationController
    {
        private readonly IAccountsService accountsService;

        public PanelController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        public IActionResult Index()
        {
            var account = this.accountsService.GetFirstAccount();
            var accountModel = new SellAccountInputModel
            {
                ChampionsCount = account.ChampionsCount,
                SkinsCount = account.SkinsCount,
                BlueEssence = account.BlueEssence,
                RiotPoints = account.RiotPoints,
                Username = account.Username,
                Password = account.Password,
                Region = account.Region,
            };
            return this.View(accountModel);
        }
    }
}