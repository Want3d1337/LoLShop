namespace LoLShop.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using LoLShop.Common;
    using LoLShop.Data.Common.Repositories;
    using LoLShop.Data.Models;
    using LoLShop.Services;
    using LoLShop.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly ICloudinaryService cloudinaryService;

        public UsersService(IRepository<ApplicationUser> usersRepository, ICloudinaryService cloudinaryService)
        {
            this.usersRepository = usersRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public ApplicationUser GetById(string userId)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            return user;
        }

        public async Task UpdateAsync(EditInputModel inputModel)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == inputModel.UserId);

            user.Rank = inputModel.Rank;

            user.Champions = inputModel.Champions;

            if (inputModel.NewImage != null)
            {
                var imageUrl = await this.cloudinaryService.UploadPhotoAsync(inputModel.NewImage, inputModel.UserId, GlobalConstants.CloudFolderForUserImages);
                user.AvatarImageUrl = imageUrl;
            }

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
