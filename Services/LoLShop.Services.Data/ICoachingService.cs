namespace LoLShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Coaching;

    public interface ICoachingService
    {
        public Task<IEnumerable<CoachViewModel>> GetAllCoachesAsync();

        Task AddAsync(OrderInputModel inputModel);

        CoachOrder GetFirstOrder(ApplicationUser coach);

        Task FinishFirstOrderAsync(ApplicationUser coach);
    }
}
