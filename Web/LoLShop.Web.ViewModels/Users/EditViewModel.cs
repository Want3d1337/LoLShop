namespace LoLShop.Web.ViewModels.Users
{
    using Microsoft.AspNetCore.Http;

    public class EditViewModel
    {
        public string UserId { get; set; }

        public string AvatarImageUrl { get; set; }

        public IFormFile NewImage { get; set; }

        public string Rank { get; set; }

        public string Champions { get; set; }
    }
}
