using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Models;
using BloodDonationBackend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationBackendTest
{
    [TestFixture]
    public class BloodInventoryServicesTest
    {
        private Mock<IRepository<int, BloodInventory>> _mockBloodInventoryRepo;
        private Mock<IRepository<int, DonationCenter>> _mockDonationCenterRepo;
        private Mock<IRepository<int, Appointment>> _mockAppointmentRepo;
        private Mock<IRepository<int, Donor>> _mockDonorRepo;
        private Mock<IRepository<int, User>> _mockUserRepo;
        private Mock<IAppointmentService> _mockAppointmentService;
        private BloodDonationContext _context;
        private BloodInventoryServices _bloodInventoryServices;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationContext>()
                .UseInMemoryDatabase(databaseName: "BloodInventoryTestDb")
                .Options;

            _context = new BloodDonationContext(options);
            _mockBloodInventoryRepo = new Mock<IRepository<int, BloodInventory>>();
            _mockDonationCenterRepo = new Mock<IRepository<int, DonationCenter>>();
            _mockAppointmentRepo = new Mock<IRepository<int, Appointment>>();
            _mockDonorRepo = new Mock<IRepository<int, Donor>>();
            _mockUserRepo = new Mock<IRepository<int, User>>();
            _mockAppointmentService = new Mock<IAppointmentService>();

            _bloodInventoryServices = new BloodInventoryServices(
                _context,
                _mockDonationCenterRepo.Object,
                _mockAppointmentRepo.Object,
                _mockDonorRepo.Object,
                _mockUserRepo.Object,
                _mockBloodInventoryRepo.Object,
                _mockAppointmentService.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddInventory_ShouldReturnAddedInventory()
        {
            // Arrange
            var addDto = new AddInventoryDTO { CenterId = 1, BloodType = "O+", Quantity = 20 };
            var newInventory = new BloodInventory
            {
                InventoryId = 1,
                CenterId = addDto.CenterId,
                BloodType = addDto.BloodType,
                Quantity = addDto.Quantity
            };

            _mockBloodInventoryRepo.Setup(repo => repo.Add(It.IsAny<BloodInventory>()))
                .ReturnsAsync(newInventory);

            // Act
            var result = await _bloodInventoryServices.AddInventory(addDto);

            // Assert
            Assert.AreEqual(addDto.BloodType, result.BloodType);
            Assert.AreEqual(addDto.Quantity, result.Quantity);
            Assert.AreEqual(newInventory.InventoryId, result.InventoryId);
            _mockBloodInventoryRepo.Verify(repo => repo.Add(It.IsAny<BloodInventory>()), Times.Once);
        }

        [Test]
        public async Task CheckAvailability_ShouldReturnAvailableBloodTypes()
        {
            // Arrange
            var checkDto = new CheckAvailabilityDTO { CenterId = 1 };
            var inventories = new List<BloodInventory>
            {
                new BloodInventory { InventoryId = 1, CenterId = 1, BloodType = "A+", Quantity = 10 },
                new BloodInventory { InventoryId = 2, CenterId = 1, BloodType = "B-", Quantity = 5 }
            };

            _context.BloodInventorys.AddRange(inventories);
            await _context.SaveChangesAsync();

            var expected = inventories.Select(i => new AvailablityDTO
            {
                InventoryId = i.InventoryId,
                BloodType = i.BloodType,
                Quantity = i.Quantity
            });

            // Act
            var result = await _bloodInventoryServices.CheckAvailability(checkDto);

            // Assert
            Assert.AreEqual(expected.Count(), result.Count());
            Assert.AreEqual(expected.First().BloodType, result.First().BloodType);
        }

        [Test]
        public async Task UpdateInventory_ShouldReturnUpdatedInventory()
        {
            // Arrange
            var updateDto = new UpdateInventoryDTO { InventoryId = 1, BloodType = "O-", Quantity = 30 };
            var existingInventory = new BloodInventory
            {
                InventoryId = 1,
                BloodType = "O+",
                Quantity = 10
            };

            _mockBloodInventoryRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(existingInventory);
            _mockBloodInventoryRepo.Setup(repo => repo.Update(It.IsAny<BloodInventory>()))
                .ReturnsAsync(new BloodInventory
                {
                    InventoryId = 1,
                    BloodType = updateDto.BloodType,
                    Quantity = updateDto.Quantity
                });

            // Act
            var result = await _bloodInventoryServices.UpdateInventory(updateDto);

            // Assert
            Assert.AreEqual(updateDto.BloodType, result.BloodType);
            Assert.AreEqual(updateDto.Quantity, result.Quantity);
            Assert.AreEqual(existingInventory.InventoryId, result.InventoryId);
            _mockBloodInventoryRepo.Verify(repo => repo.Update(It.IsAny<BloodInventory>()), Times.Once);
        }
    }
}
