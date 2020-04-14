namespace LoLShop.Web.Areas.Administration.ViewModels
{
    using LoLShop.Web.ViewModels.Accounts;
    using LoLShop.Web.ViewModels.Jobs;

    public class AdministrationViewModel
    {
        public SellAccountInputModel Account { get; set; }

        public ApplicationViewModel Application { get; set; }
    }
}
