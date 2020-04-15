namespace LoLShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using LoLShop.Data.Common.Models;

    public class CoachOrder : BaseModel<int>
    {
        public CoachOrder()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string BuyerId { get; set; }

        [Required]
        public string CoachId { get; set; }

        [Required]
        public string GameName { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string DiscordTag { get; set; }

        [Required]
        [Range(1, 5)]
        public int Hours { get; set; }
    }
}
