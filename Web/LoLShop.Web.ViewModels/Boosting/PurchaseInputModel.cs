namespace LoLShop.Web.ViewModels.Boosting
{
    using System.ComponentModel.DataAnnotations;

    public class PurchaseInputModel
    {
        [Required]
        public string CurrentRank { get; set; }

        [Required]
        [Range(1, 10)]
        public int Ranks { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
