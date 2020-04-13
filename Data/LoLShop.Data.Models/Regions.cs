namespace LoLShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum Regions
    {
        [Display(Name = "EUW")]
        EUW = 1,

        [Display(Name = "EUNE")]
        EUNE = 2,

        [Display(Name = "NA")]
        NA = 3,

        [Display(Name = "RU")]
        RU = 4,
    }
}
