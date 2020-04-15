namespace LoLShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoLShop.Web.ViewModels.Coaching;

    public interface ICoachingService
    {
        public Task<IEnumerable<CoachViewModel>> GetAllCoaches();

        Task AddAsync(OrderInputModel inputModel);
    }
}
