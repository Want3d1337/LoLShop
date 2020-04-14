namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Web.ViewModels.Jobs;

    public interface IJobsService
    {
        Task CreateAsync(ApplicationInputModel inputModel, ApplicationUser user);

        Application GetFirstApplication();

        Task RejectApplication(string userId);

        Task ApproveApplication(string userId, string position);

        Task RegisterBoosterAsync(string userId);

        Task RegisterCoachAsync(string userId);
    }
}
