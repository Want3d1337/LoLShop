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
    using LoLShop.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Identity;

    using Moq;

    using Xunit;

    public class JobsServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "JobsService CreateAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var applicationsRepository = new EfRepository<Application>(context);

            var userManager = this.GetUserManagerMock();

            var jobsService = new JobsService(applicationsRepository, userManager.Object);

            var userId = Guid.NewGuid().ToString();

            var applicationModel = new ApplicationInputModel
            {
                Age = 18,
                Name = "Nikola",
                Country = "Bulgaria",
                Position = GlobalConstants.BoosterRoleName,
                Rank = "Diamond 2",
                Champions = "Katarina, Yasuo",
                Description = "Very good midlaner",
            };

            // Act
            await jobsService.CreateAsync(applicationModel, userId);

            var actualResult = applicationsRepository.All().First();
            var expectedResult = applicationModel;

            // Assert
            Assert.True(userId == actualResult.UserId, errorMessage);
            Assert.True(expectedResult.Age == actualResult.Age, errorMessage);
            Assert.True(expectedResult.Name == actualResult.Name, errorMessage);
            Assert.True(expectedResult.Country == actualResult.Country, errorMessage);
            Assert.True(expectedResult.Position == actualResult.Position, errorMessage);
            Assert.True(expectedResult.Rank == actualResult.Rank, errorMessage);
            Assert.True(expectedResult.Champions == actualResult.Champions, errorMessage);
            Assert.True(expectedResult.Description == actualResult.Description, errorMessage);
        }

        [Fact]
        public async Task GetFirstApplication_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "JobsService GetFirstApplication() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var applicationsRepository = new EfRepository<Application>(context);

            var userManager = this.GetUserManagerMock();

            var jobsService = new JobsService(applicationsRepository, userManager.Object);

            var userId = Guid.NewGuid().ToString();

            var applicationModel = new ApplicationInputModel
            {
                Age = 18,
                Name = "Nikola",
                Country = "Bulgaria",
                Position = GlobalConstants.BoosterRoleName,
                Rank = "Diamond 2",
                Champions = "Katarina, Yasuo",
                Description = "Very good midlaner",
            };

            // Act
            await jobsService.CreateAsync(applicationModel, userId);

            var actualResult = jobsService.GetFirstApplication();
            var expectedResult = applicationModel;

            // Assert
            Assert.True(userId == actualResult.UserId, errorMessage);
            Assert.True(expectedResult.Age == actualResult.Age, errorMessage);
            Assert.True(expectedResult.Name == actualResult.Name, errorMessage);
            Assert.True(expectedResult.Country == actualResult.Country, errorMessage);
            Assert.True(expectedResult.Position == actualResult.Position, errorMessage);
            Assert.True(expectedResult.Rank == actualResult.Rank, errorMessage);
            Assert.True(expectedResult.Champions == actualResult.Champions, errorMessage);
            Assert.True(expectedResult.Description == actualResult.Description, errorMessage);
        }

        [Fact]
        public async Task ApproveApplicationAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "JobsService ApproveApplicationAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var applicationsRepository = new EfRepository<Application>(context);

            var userManager = this.GetUserManagerMock();

            var jobsService = new JobsService(applicationsRepository, userManager.Object);

            var role = GlobalConstants.BoosterRoleName;

            var userId = Guid.NewGuid().ToString();

            var applicationModel = new ApplicationInputModel
            {
                Age = 18,
                Name = "Nikola",
                Country = "Bulgaria",
                Position = role,
                Rank = "Diamond 2",
                Champions = "Katarina, Yasuo",
                Description = "Very good midlaner",
            };

            // Act
            await jobsService.CreateAsync(applicationModel, userId);
            var applicationsCount = applicationsRepository.All().Count();
            await jobsService.ApproveApplicationAsync(userId, role);

            var actualResult = applicationsRepository.All().Count();
            var expectedResult = applicationsCount - 1;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }

        [Fact]
        public async Task RejectApplicationAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "JobsService RejectApplicationAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var applicationsRepository = new EfRepository<Application>(context);

            var userManager = this.GetUserManagerMock();

            var jobsService = new JobsService(applicationsRepository, userManager.Object);

            var userId = Guid.NewGuid().ToString();

            var applicationModel = new ApplicationInputModel
            {
                Age = 18,
                Name = "Nikola",
                Country = "Bulgaria",
                Position = GlobalConstants.BoosterRoleName,
                Rank = "Diamond 2",
                Champions = "Katarina, Yasuo",
                Description = "Very good midlaner",
            };

            // Act
            await jobsService.CreateAsync(applicationModel, userId);
            var applicationsCount = applicationsRepository.All().Count();
            await jobsService.RejectApplicationAsync(userId);

            var actualResult = applicationsRepository.All().Count();
            var expectedResult = applicationsCount - 1;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }

        [Fact]
        public async Task IsUserApplied_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "JobsService IsUserApplied() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var applicationsRepository = new EfRepository<Application>(context);

            var userManager = this.GetUserManagerMock();

            var jobsService = new JobsService(applicationsRepository, userManager.Object);

            var userId = Guid.NewGuid().ToString();

            var applicationModel = new ApplicationInputModel
            {
                Age = 18,
                Name = "Nikola",
                Country = "Bulgaria",
                Position = GlobalConstants.BoosterRoleName,
                Rank = "Diamond 2",
                Champions = "Katarina, Yasuo",
                Description = "Very good midlaner",
            };

            // Act
            await jobsService.CreateAsync(applicationModel, userId);

            var result = jobsService.IsUserApplied(userId);

            // Assert
            Assert.True(result, errorMessage);
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
