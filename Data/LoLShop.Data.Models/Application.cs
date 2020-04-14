namespace LoLShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LoLShop.Data.Common.Models;

    public class Application : BaseModel<int>
    {
        public Application()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        public string Champions { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
