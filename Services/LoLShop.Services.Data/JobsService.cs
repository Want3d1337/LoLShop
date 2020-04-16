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
        private readonly IRepository<Application> applicationsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public JobsService(IRepository<Application> applicationsRepository, UserManager<ApplicationUser> userManager)
        {
            this.applicationsRepository = applicationsRepository;
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

            await this.applicationsRepository.AddAsync(application);
            await this.applicationsRepository.SaveChangesAsync();
        }

        public Application GetFirstApplication()
        {
            return this.applicationsRepository.All().FirstOrDefault();
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
            var application = this.applicationsRepository.All().FirstOrDefault(x => x.UserId == userId);
            this.applicationsRepository.Delete(application);
            await this.applicationsRepository.SaveChangesAsync();
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
