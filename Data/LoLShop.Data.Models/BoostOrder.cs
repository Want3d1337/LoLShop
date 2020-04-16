namespace LoLShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using LoLShop.Data.Common.Models;

    public class BoostOrder : BaseModel<int>
    {
        public BoostOrder()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string CurrentRank { get; set; }

        [Required]
        [Range(1, 10)]
        public int Ranks { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
