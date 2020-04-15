namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Users;

    public interface IUsersService
    {
        ApplicationUser GetById(string userId);

        Task UpdateAsync(EditInputModel inputModel);
    }
}
