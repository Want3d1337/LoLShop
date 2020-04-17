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

    public class AccountsServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "AccountsService CreateAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

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

            var actualResult = accountsRepository.All().First();
            var expectedResult = accountServiceModel;

            // Assert
            Assert.True(user.Id == actualResult.SellerId, errorMessage);
            Assert.True(expectedResult.ChampionsCount == actualResult.ChampionsCount, errorMessage);
            Assert.True(expectedResult.SkinsCount == actualResult.SkinsCount, errorMessage);
            Assert.True(expectedResult.BlueEssence == actualResult.BlueEssence, errorMessage);
            Assert.True(expectedResult.RiotPoints == actualResult.RiotPoints, errorMessage);
            Assert.True(expectedResult.Username == actualResult.Username, errorMessage);
            Assert.True(expectedResult.Password == actualResult.Password, errorMessage);
            Assert.True(expectedResult.Region == actualResult.Region, errorMessage);
        }

        [Fact]
        public async Task GetFirstAccount_WithExistingAccount_ShouldReturnCorrectResult()
        {
            var errorMessage = "AccountsService GetFirstAccount() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

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

            var actualResult = accountsService.GetFirstAccount();
            var expectedResult = accountServiceModel;

            // Assert
            Assert.True(user.Id == actualResult.SellerId, errorMessage);
            Assert.True(expectedResult.ChampionsCount == actualResult.ChampionsCount, errorMessage);
            Assert.True(expectedResult.SkinsCount == actualResult.SkinsCount, errorMessage);
            Assert.True(expectedResult.BlueEssence == actualResult.BlueEssence, errorMessage);
            Assert.True(expectedResult.RiotPoints == actualResult.RiotPoints, errorMessage);
            Assert.True(expectedResult.Username == actualResult.Username, errorMessage);
            Assert.True(expectedResult.Password == actualResult.Password, errorMessage);
            Assert.True(expectedResult.Region == actualResult.Region, errorMessage);
        }

        [Fact]
        public void GetFirstAccount_WithEmptySoldAccountsCollection_ShouldReturnNull()
        {
            var errorMessage = "AccountsService GetFirstAccount() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

            // Act
            var actualResult = accountsService.GetFirstAccount();
            Account nullAccount = null;
            var expectedResult = nullAccount;

            // Assert
            Assert.True(expectedResult == actualResult, errorMessage);
        }

        [Fact]
        public async Task RejectAccountAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "AccountsService RejectAccountAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

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

            var accountsCount = accountsRepository.All().Count();
            await accountsService.RejectAccountAsync(accountServiceModel.Username);

            var actualResult = accountsRepository.All().Count();
            var expectedResult = accountsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage);
        }

        [Fact]
        public async Task RejectAccountAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

            var username = "wanted1337";

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await accountsService.RejectAccountAsync(username);
            });
        }

        [Fact]
        public async Task ApproveAccountAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var errorMessage = "AccountsService ApproveAccountAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

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

            var accountsCount = accountsRepository.All().Count();
            await accountsService.ApproveAccountAsync(accountServiceModel.Username);

            var actualResult = accountsRepository.All().Count();
            var expectedResult = accountsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage);
        }

        [Fact]
        public async Task ApproveAccountAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "AccountsService ApproveAccountAsync() method does not work properly.";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

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

            var actualResult = approvedAccountsRepository.All().First();

            var expectedResult = accountServiceModel;

            // Assert
            Assert.True(actualResult.SellerId == user.Id, errorMessage);
            Assert.True(actualResult.Username == expectedResult.Username, errorMessage);
            Assert.True(actualResult.Password == expectedResult.Password, errorMessage);
            Assert.True(actualResult.Region == expectedResult.Region, errorMessage);
        }

        [Fact]
        public async Task ApproveAccountAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var accountsRepository = new EfRepository<Account>(context);

            var approvedAccountsRepository = new EfRepository<ApprovedAccount>(context);

            var accountsService = new AccountsService(accountsRepository, approvedAccountsRepository);

            var username = "want3d1337";

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await accountsService.ApproveAccountAsync(username);
            });
        }
    }
}
