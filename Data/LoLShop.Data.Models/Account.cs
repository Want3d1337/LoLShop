namespace LoLShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LoLShop.Data.Common.Models;

    public class Account : BaseModel<int>
    {
        public Account()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public int ChampionsCount { get; set; }

        [Required]
        public int SkinsCount { get; set; }

        [Required]
        public int BlueEssence { get; set; }

        [Required]
        public int RiotPoints { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]

        public Regions Region { get; set; }
    }
}
