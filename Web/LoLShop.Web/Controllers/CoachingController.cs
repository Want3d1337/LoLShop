namespace LoLShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CoachingController : BaseController
    {
        [HttpGet]
        public IActionResult Coaches()
        {
            return this.View();
        }
    }
}
