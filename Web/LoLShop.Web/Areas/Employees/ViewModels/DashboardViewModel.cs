namespace LoLShop.Web.Areas.Employees.ViewModels
{
    using LoLShop.Web.ViewModels.Boosting;
    using LoLShop.Web.ViewModels.Coaching;

    public class DashboardViewModel
    {
        public CoachOrderViewModel CoachOrder { get; set; }

        public BoostOrderViewModel[] BoostOrders { get; set; }
    }
}
