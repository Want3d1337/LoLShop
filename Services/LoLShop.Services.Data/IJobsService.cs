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
        Task CreateAsync(ApplicationInputModel inputModel, string userId);

        Application GetFirstApplication();

        Task RejectApplicationAsync(string userId);

        Task ApproveApplicationAsync(string userId, string position);

        bool IsUserApplied(string userId);
    }
}
