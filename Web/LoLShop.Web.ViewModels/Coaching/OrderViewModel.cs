namespace LoLShop.Web.ViewModels.Coaching
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrderViewModel
    {
        public string CoachId { get; set; }
        public string ImageUrl { get; set; }

        public string GameName { get; set; }

        public string Region { get; set; }

        public string DiscordTag { get; set; }

        public int Hours { get; set; }

    }
}
