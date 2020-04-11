namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UsersController : BaseController
    {
        [HttpGet]
        [Route("/Users/Account")]
        public async Task<IActionResult> AccountDetails()
        {
            return this.View();
        }
    }
}
