namespace LoLShop.Services.Data
{
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task UpdateAsync(EditInputModel inputModel);

        Task AddFundsAsync(ApplicationUser user, double funds);

        Task RemoveFundsAsync(ApplicationUser user, double funds);
    }
}
