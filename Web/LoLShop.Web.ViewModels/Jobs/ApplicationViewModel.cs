namespace LoLShop.Web.ViewModels.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApplicationViewModel
    {
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
