namespace LoLShop.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsernameFundsInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Range(1.00, 1000.00)]
        public double Funds { get; set; }
    }
}
