namespace LoLShop.Services.Data
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Jobs;

    public class JobsService : IJobsService
    {
        private readonly IRepository<Application> applicationRepository;

        public JobsService(IRepository<Application> applicationRepository)
        {
            this.applicationRepository = applicationRepository;
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
    }
}
