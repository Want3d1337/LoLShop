namespace LoLShop.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class JobsController : BaseController
    {
        public IActionResult Apply()
        {
            return this.View();
        }
    }
}
