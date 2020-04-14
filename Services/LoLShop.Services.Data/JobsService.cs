namespace LoLShop.Services.Data
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Identity;

    public class JobsService : IJobsService
    {
        private readonly IRepository<Application> applicationRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public JobsService(IRepository<Application> applicationRepository, UserManager<ApplicationUser> userManager)
        {
            this.applicationRepository = applicationRepository;
            this.userManager = userManager;
        }

        public async Task CreateAsync(ApplicationInputModel inputModel, ApplicationUser user)
        {
            var application = new Application
            {
                UserId = user.Id,
                Age = inputModel.Age,
                Name = inputModel.Name,
                Country = inputModel.Country,
                Position = inputModel.Position,
                Rank = inputModel.Rank,
                Champions = inputModel.Champions,
                Description = inputModel.Description,
            };

            await this.applicationRepository.AddAsync(application);
            await this.applicationRepository.SaveChangesAsync();
        }

        public Application GetFirstApplication()
        {
            return this.applicationRepository.All().FirstOrDefault();
        }

        public async Task ApproveApplication(string userId, string position)
        {
            if (position == GlobalConstants.BoosterRoleName)
            {
                await this.RegisterBoosterAsync(userId);
            }
            else
            {
                await this.RegisterCoachAsync(userId);
            }

            await this.RejectApplication(userId);
        }

        public async Task RejectApplication(string userId)
        {
            var application = this.applicationRepository.All().FirstOrDefault(x => x.UserId == userId);
            this.applicationRepository.Delete(application);
            await this.applicationRepository.SaveChangesAsync();
        }

        public async Task RegisterBoosterAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.BoosterRoleName);
        }

        public async Task RegisterCoachAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.CoachRoleName);
        }
    }
}
