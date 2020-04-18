namespace LoLShop.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using LoLShop.Data;
    using LoLShop.Data.Models;
    using LoLShop.Data.Repositories;
    using LoLShop.Services.Data.Tests.Common;
    using LoLShop.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    public class UsersServiceTests
    {
        [Fact]
        public async Task UpdateAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "UsersService UpdateAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfRepository<ApplicationUser>(context);

            var cloudinaryService = new Mock<ICloudinaryService>();

            var usersService = new UsersService(usersRepository, cloudinaryService.Object);

            var userStore = new ApplicationUserStore(context);

            var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);

            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "want3d1337",
            };

            var userModel = new EditInputModel
            {
                UserId = user.Id,
                Rank = "Challenger",
                Champions = "Katarina",
                AvatarImageUrl = "https://res.cloudinary.com/lolshop-cloud/image/upload/v1586952164/avatar_yswuru.jpg",
                NewImage = null,
            };

            // Act
            await userManager.CreateAsync(user);
            await usersService.UpdateAsync(userModel);

            var actualResult = usersRepository.All().First();
            var expectedResult = userModel;

            // Assert
            Assert.True(expectedResult.UserId == actualResult.Id, errorMessage);
            Assert.True(expectedResult.AvatarImageUrl == actualResult.AvatarImageUrl, errorMessage);
            Assert.True(expectedResult.Rank == actualResult.Rank, errorMessage);
            Assert.True(expectedResult.Champions == actualResult.Champions, errorMessage);
        }

        [Fact]
        public async Task AddFundsAsync_WithCorrectData_ShouldSuccessfullyUpdate()
        {
            var errorMessage = "UsersService AddFundsAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfRepository<ApplicationUser>(context);

            var cloudinaryService = new Mock<ICloudinaryService>();

            var usersService = new UsersService(usersRepository, cloudinaryService.Object);

            var userStore = new ApplicationUserStore(context);

            var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);

            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "want3d1337",
            };

            // Act
            await userManager.CreateAsync(user);

            var funds = usersRepository.All().First().Funds;
            await usersService.AddFundsAsync(user, 5);

            var actualResult = usersRepository.All().First().Funds;
            var expectedResult = funds + 5;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }

        [Fact]
        public async Task RemoveFundsAsync_WithCorrectData_ShouldSuccessfullyUpdate()
        {
            var errorMessage = "UsersService RemoveFundsAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfRepository<ApplicationUser>(context);

            var cloudinaryService = new Mock<ICloudinaryService>();

            var usersService = new UsersService(usersRepository, cloudinaryService.Object);

            var userStore = new ApplicationUserStore(context);

            var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);

            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "want3d1337",
            };

            // Act
            await userManager.CreateAsync(user);

            var funds = usersRepository.All().First().Funds;
            await usersService.RemoveFundsAsync(user, 5);

            var actualResult = usersRepository.All().First().Funds;
            var expectedResult = funds - 5;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }
    }
}
