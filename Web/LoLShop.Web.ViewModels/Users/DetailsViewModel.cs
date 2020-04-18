namespace LoLShop.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DetailsViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string AvatarImageUrl { get; set; }

        public string Rank { get; set; }

        public string Champions { get; set; }

        public double Funds { get; set; }
    }
}
