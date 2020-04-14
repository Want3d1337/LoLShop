
namespace LoLShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LoLShop.Data.Common.Models;

    public class ApprovedAccount : BaseModel<int>
    {
        public ApprovedAccount()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

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
