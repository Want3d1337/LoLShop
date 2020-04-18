namespace LoLShop.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoLShop.Data;
    using LoLShop.Data.Models;
    using LoLShop.Data.Repositories;
    using LoLShop.Services.Data.Tests.Common;
    using LoLShop.Web.ViewModels.Accounts;
    using Xunit;

    public class ApprovedAccountsServiceTests
    {
        [Fact]
        public async Task GetAllAccountsRegion_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "ApprovedAccountsService GetAllAccountsRegion() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

            var approvedAccountsService = new ApprovedAccountsService(approvedAccountsRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Nikola",
            };

            var accountServiceModel = new SellAccountInputModel
            {
                ChampionsCount = 20,
                SkinsCount = 20,
                BlueEssence = 500,
                RiotPoints = 600,
                Username = "want3d1337",
                Password = "123",
                Region = Regions.EUNE,
            };

            // Act
            await accountsService.CreateAsync(accountServiceModel, user);
            await accountsService.ApproveAccountAsync(accountServiceModel.Username);

            var actualResult = approvedAccountsService.GetAllAccountsRegion().First().Region;
            var expectedResult = Regions.EUNE;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage);
        }

        [Fact]
        public async Task PurchaseAccountAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "ApprovedAccountsService PurchaseAccountAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

            var approvedAccountsService = new ApprovedAccountsService(approvedAccountsRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Nikola",
            };

            var accountServiceModel = new SellAccountInputModel
            {
                ChampionsCount = 20,
                SkinsCount = 20,
                BlueEssence = 500,
                RiotPoints = 600,
                Username = "want3d1337",
                Password = "123",
                Region = Regions.EUNE,
            };

            // Act
            await accountsService.CreateAsync(accountServiceModel, user);
            await accountsService.ApproveAccountAsync(accountServiceModel.Username);

            var actualResult = await approvedAccountsService.PurchaseAccountAsync(accountServiceModel.Region);
            var expectedResult = accountServiceModel;

            // Assert
            Assert.True(actualResult.SellerId == user.Id, errorMessage);
            Assert.True(actualResult.Username == expectedResult.Username, errorMessage);
            Assert.True(actualResult.Password == expectedResult.Password, errorMessage);
            Assert.True(actualResult.Region == expectedResult.Region, errorMessage);
        }

        [Fact]
        public async Task PurchaseAccountAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "ApprovedAccountsService PurchaseAccountAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

            var approvedAccountsService = new ApprovedAccountsService(approvedAccountsRepository);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Nikola",
            };

            var accountServiceModel = new SellAccountInputModel
            {
                ChampionsCount = 20,
                SkinsCount = 20,
                BlueEssence = 500,
                RiotPoints = 600,
                Username = "want3d1337",
                Password = "123",
                Region = Regions.EUNE,
            };

            // Act
            await accountsService.CreateAsync(accountServiceModel, user);
            await accountsService.ApproveAccountAsync(accountServiceModel.Username);

            var approvedAccountsCount = approvedAccountsRepository.All().Count();
            await approvedAccountsService.PurchaseAccountAsync(accountServiceModel.Region);

            var actualResult = approvedAccountsRepository.All().Count();
            var expectedResult = approvedAccountsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage);
        }

        [Fact]
        public async Task PurchaseAccountAsync_WithEmptyCollection_ShouldReturnNull()
        {
            var errorMessage = "ApprovedAccountsService PurchaseAccountAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var approvedAccountsService = new ApprovedAccountsService(approvedAccountsRepository);

            // Act
            var actualResult = await approvedAccountsService.PurchaseAccountAsync(Regions.EUNE);
            ApprovedAccount nullAccount = null;
            var expectedResult = nullAccount;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage);
        }
    }
}