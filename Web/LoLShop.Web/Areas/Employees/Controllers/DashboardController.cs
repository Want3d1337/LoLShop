namespace LoLShop.Web.Areas.Employees.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : EmployeesController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
