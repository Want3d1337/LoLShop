namespace LoLShop.Web.ViewModels.Jobs
{
    using System.ComponentModel.DataAnnotations;

    using LoLShop.Data.Models;
    using LoLShop.Services.Mapping;

    public class ApplicationInputModel : IMapTo<Application>
    {
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
