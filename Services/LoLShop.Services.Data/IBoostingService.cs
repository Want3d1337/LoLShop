namespace LoLShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using LoLShop.Web.ViewModels.Boosting;

    public interface IBoostingService
    {
        Task AddAsync(PurchaseInputModel inputModel);
    }
}
