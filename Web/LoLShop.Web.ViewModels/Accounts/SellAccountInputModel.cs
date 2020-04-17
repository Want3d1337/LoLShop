namespace LoLShop.Web.ViewModels.Accounts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LoLShop.Data.Models;
    using LoLShop.Services.Mapping;

    public class SellAccountInputModel : IMapTo<Account>
    {
        [Required]
        [Range(1, 160)]
        public int ChampionsCount { get; set; }

        [Required]
        [Range(1, 1000)]
        public int SkinsCount { get; set; }

        [Required]
        [Range(1, 500000)]
        public int BlueEssence { get; set; }

        [Required]
        [Range(1, 15000)]
        public int RiotPoints { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Regions Region { get; set; }

    }
}
