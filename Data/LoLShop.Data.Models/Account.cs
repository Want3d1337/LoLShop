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
        public string SellerId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]

        public Regions Region { get; set; }
    }
}
