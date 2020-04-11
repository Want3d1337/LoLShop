namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class AccountsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return this.View();
        }
    }
}
