namespace LoLShop.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Data.Models;
    using LoLShop.Data.Repositories;
    using LoLShop.Services.Data.Tests.Common;
    using LoLShop.Web.ViewModels.Boosting;
    using Xunit;

    public class BoostingServiceTests
    {
        [Fact]
        public async Task AddAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "BoostingService AddAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var boostOrdersRepository = new EfRepository<BoostOrder>(context);

            var boostingService = new BoostingService(boostOrdersRepository);

            var boostOrderModel = new PurchaseInputModel
            {
                CurrentRank = "Diamond 1",
                Ranks = 1,
                Username = "want3d1337",
                Password = "123",
            };

            // Act
            await boostingService.AddAsync(boostOrderModel);

            var actualResult = boostOrdersRepository.All().First();
            var expectedResult = boostOrderModel;

            // Assert
            Assert.True(expectedResult.CurrentRank == actualResult.CurrentRank, errorMessage);
            Assert.True(expectedResult.Ranks == actualResult.Ranks, errorMessage);
            Assert.True(expectedResult.Username == actualResult.Username, errorMessage);
            Assert.True(expectedResult.Password == actualResult.Password, errorMessage);
        }

        [Fact]
        public async Task GetAllBoostOrders_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "BoostingService GetAllBoostOrders() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var boostOrdersRepository = new EfRepository<BoostOrder>(context);

            var boostingService = new BoostingService(boostOrdersRepository);

            var boostOrderModel = new PurchaseInputModel
            {
                CurrentRank = "Diamond 1",
                Ranks = 1,
                Username = "want3d1337",
                Password = "123",
            };

            // Act
            await boostingService.AddAsync(boostOrderModel);

            var actualResult = boostingService.GetAllBoostOrders().First();
            var expectedResult = boostOrderModel;

            // Assert
            Assert.True(expectedResult.CurrentRank == actualResult.CurrentRank, errorMessage);
            Assert.True(expectedResult.Ranks == actualResult.Ranks, errorMessage);
            Assert.True(expectedResult.Username == actualResult.Username, errorMessage);
            Assert.True(expectedResult.Password == actualResult.Password, errorMessage);
        }

        [Fact]
        public async Task AcceptOrderAsync_WithCorrectData_ShouldSuccessfullyUpdate()
        {
            var errorMessage = "BoostingService AcceptOrderAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var boostOrdersRepository = new EfRepository<BoostOrder>(context);

            var boostingService = new BoostingService(boostOrdersRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var boostOrderModel = new PurchaseInputModel
            {
                CurrentRank = "Diamond 1",
                Ranks = 1,
                Username = "want3d1337",
                Password = "123",
            };

            // Act
            await boostingService.AddAsync(boostOrderModel);
            await boostingService.AcceptOrderAsync(user, boostOrderModel.Username);

            var actualResult = boostOrdersRepository.All().First();
            var expectedResult = user.Id;

            // Assert
            Assert.True(expectedResult == actualResult.BoosterId, errorMessage);
        }

        [Fact]
        public async Task CompleteOrderAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "BoostingService CompleteOrderAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var boostOrdersRepository = new EfRepository<BoostOrder>(context);

            var boostingService = new BoostingService(boostOrdersRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
            };

            var boostOrderModel = new PurchaseInputModel
            {
                CurrentRank = "Diamond 1",
                Ranks = 1,
                Username = "want3d1337",
                Password = "123",
            };

            // Act
            await boostingService.AddAsync(boostOrderModel);
            await boostingService.AcceptOrderAsync(user, boostOrderModel.Username);

            var boostOrdersCount = boostOrdersRepository.All().Count();
            await boostingService.CompleteOrderAsync(boostOrderModel.Username);

            var actualResult = boostOrdersRepository.All().Count();
            var expectedResult = boostOrdersCount - 1;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }
    }
}
