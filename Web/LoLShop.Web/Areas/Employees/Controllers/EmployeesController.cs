namespace LoLShop.Web.Areas.Employees.Controllers
{
    using LoLShop.Common;
    using LoLShop.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.BoosterRoleName + "," + GlobalConstants.CoachRoleName)]
    [Area("Employees")]
    public class EmployeesController : BaseController
    {
    }
}
