namespace LoLShop.Web.Areas.Employees.ViewModels
{
    using System.Collections.Generic;

    using LoLShop.Web.ViewModels.Boosting;
    using LoLShop.Web.ViewModels.Coaching;

    public class DashboardViewModel
    {
        public CoachOrderViewModel CoachOrder { get; set; }

        public IEnumerable<BoostOrderViewModel> BoostOrders { get; set; }
    }
}
