namespace LoLShop.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data;
    using LoLShop.Data.Models;
    using LoLShop.Data.Repositories;
    using LoLShop.Services.Data.Tests.Common;
    using LoLShop.Web.ViewModels.Coaching;
    using Microsoft.AspNetCore.Identity;

    using Moq;

    using Xunit;

    public class CoachingServiceTests
    {
        [Fact]
        public async Task AddAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "CoachingService AddAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var coachOrdersRepository = new EfRepository<CoachOrder>(context);

            var userManager = this.GetUserManagerMock();

            var coachingService = new CoachingService(userManager.Object, coachOrdersRepository);

            var buyerId = Guid.NewGuid().ToString();
            var coachId = Guid.NewGuid().ToString();

            var coachOrderModel = new OrderInputModel
            {
                BuyerId = buyerId,
                CoachId = coachId,
                GameName = "want3d1337",
                Region = "EUW",
                DiscordTag = "1337#0000",
                Hours = 2,
            };

            // Act
            await coachingService.AddAsync(coachOrderModel);

            var actualResult = coachOrdersRepository.All().First();
            var expectedResult = coachOrderModel;

            // Assert
            Assert.True(expectedResult.BuyerId == actualResult.BuyerId, errorMessage);
            Assert.True(expectedResult.CoachId == actualResult.CoachId, errorMessage);
            Assert.True(expectedResult.GameName == actualResult.GameName, errorMessage);
            Assert.True(expectedResult.Region == actualResult.Region, errorMessage);
            Assert.True(expectedResult.DiscordTag == actualResult.DiscordTag, errorMessage);
            Assert.True(expectedResult.Hours == actualResult.Hours, errorMessage);
        }

        [Fact]
        public async Task FinishFirstOrderAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "CoachingService FinishFirstOrderAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var coachOrdersRepository = new EfRepository<CoachOrder>(context);

            var userManager = this.GetUserManagerMock();

            var coachingService = new CoachingService(userManager.Object, coachOrdersRepository);

            var buyerId = Guid.NewGuid().ToString();
            var coach = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "want3d1337",
            };

            var coachOrderModel = new OrderInputModel
            {
                BuyerId = buyerId,
                CoachId = coach.Id,
                GameName = "want3d1337",
                Region = "EUW",
                DiscordTag = "1337#0000",
                Hours = 2,
            };

            // Act
            await coachingService.AddAsync(coachOrderModel);
            var coachOrdersCount = coachOrdersRepository.All().Count();

            await coachingService.FinishFirstOrderAsync(coach);
            var actualResult = coachOrdersRepository.All().Count();
            var expectedResult = coachOrdersCount - 1;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }

        [Fact]
        public async Task GetFirstOrder_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "CoachingService GetFirstOrder() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var coachOrdersRepository = new EfRepository<CoachOrder>(context);

            var userManager = this.GetUserManagerMock();

            var coachingService = new CoachingService(userManager.Object, coachOrdersRepository);

            var buyerId = Guid.NewGuid().ToString();
            var coach = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "want3d1337",
            };

            var coachOrderModel = new OrderInputModel
            {
                BuyerId = buyerId,
                CoachId = coach.Id,
                GameName = "want3d1337",
                Region = "EUW",
                DiscordTag = "1337#0000",
                Hours = 2,
            };

            // Act
            await coachingService.AddAsync(coachOrderModel);

            var actualResult = coachingService.GetFirstOrder(coach);
            var expectedResult = coachOrderModel;

            // Assert
            Assert.True(expectedResult.BuyerId == actualResult.BuyerId, errorMessage);
            Assert.True(expectedResult.CoachId == actualResult.CoachId, errorMessage);
            Assert.True(expectedResult.GameName == actualResult.GameName, errorMessage);
            Assert.True(expectedResult.Region == actualResult.Region, errorMessage);
            Assert.True(expectedResult.DiscordTag == actualResult.DiscordTag, errorMessage);
            Assert.True(expectedResult.Hours == actualResult.Hours, errorMessage);
        }

        [Fact]
        public async Task GetAllCoachesAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "CoachingService GetAllCoachesAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var coachOrdersRepository = new EfRepository<CoachOrder>(context);

            var userStore = new ApplicationUserStore(context);
            var roleStore = new ApplicationRoleStore(context);
            var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);
            var roleManager = new RoleManager<ApplicationRole>(roleStore, null, null, null, null);
            var coachingService = new CoachingService(userManager, coachOrdersRepository);

            var roleName = GlobalConstants.CoachRoleName;

            var role = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
            };

            var coach = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "want3d1337",
            };

            // Act
            await roleManager.CreateAsync(role);
            await userManager.CreateAsync(coach);
            await userManager.AddToRoleAsync(coach, roleName);

            var actualResult = await coachingService.GetAllCoachesAsync();
            var expectedResult = coach;

            // Assert
            Assert.True(actualResult.First().UserId == expectedResult.Id, errorMessage);
            Assert.True(actualResult.First().Username == expectedResult.UserName, errorMessage);
        }

        private Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            return userManagerMock;
        }
    }
}
