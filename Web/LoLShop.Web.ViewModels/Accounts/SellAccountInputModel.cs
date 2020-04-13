namespace LoLShop.Web.ViewModels.Accounts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LoLShop.Data.Models;
    using LoLShop.Services.Mapping;

    public class SellAccountInputModel : IMapTo<Account>
    {
        [Required]
        public int ChampionsCount { get; set; }

        [Required]
        public int SkinsCount { get; set; }

        [Required]
        public int BlueEssence { get; set; }

        [Required]
        public int RiotPoints { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Regions Region { get; set; }

    }
}
