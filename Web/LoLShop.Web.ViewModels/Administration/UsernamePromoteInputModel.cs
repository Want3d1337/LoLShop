namespace LoLShop.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class UsernamePromoteInputModel
    {
        [Required]
        public string Username { get; set; }

        public string Role { get; set; }
    }
}
