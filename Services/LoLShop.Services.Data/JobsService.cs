namespace LoLShop.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Identity;

    public class JobsService : IJobsService
    {
        private readonly IRepository<Application> applicationsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public JobsService(IRepository<Application> applicationsRepository, UserManager<ApplicationUser> userManager)
        {
            this.applicationsRepository = applicationsRepository;
            this.userManager = userManager;
        }

        public async Task CreateAsync(ApplicationInputModel inputModel, string userId)
        {
            var application = new Application
            {
                UserId = userId,
                Age = inputModel.Age,
                Name = inputModel.Name,
                Country = inputModel.Country,
                Position = inputModel.Position,
                Rank = inputModel.Rank,
                Champions = inputModel.Champions,
                Description = inputModel.Description,
            };

            await this.applicationsRepository.AddAsync(application);
            await this.applicationsRepository.SaveChangesAsync();
        }

        public Application GetFirstApplication()
        {
            return this.applicationsRepository.All().FirstOrDefault();
        }

        public async Task ApproveApplicationAsync(string userId, string position)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            await this.userManager.AddToRoleAsync(user, position);

            await this.RejectApplicationAsync(userId);
        }

        public async Task RejectApplicationAsync(string userId)
        {
            var application = this.applicationsRepository.All().FirstOrDefault(x => x.UserId == userId);

            this.applicationsRepository.Delete(application);

            await this.applicationsRepository.SaveChangesAsync();
        }

        public bool IsUserApplied(string userId)
        {
            var returnValue = false;
            if (this.applicationsRepository.All().FirstOrDefault(x => x.UserId == userId) != null)
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}
