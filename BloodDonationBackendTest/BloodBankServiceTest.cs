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
    public class BloodBankServiceTest
    {
        private Mock<IRepository<int, DonationCenter>> _mockDonationCenterRepo;
        private Mock<IRepository<int, Appointment>> _mockAppointmentRepo;
        private Mock<IRepository<int, Donor>> _mockDonorRepo;
        private Mock<IRepository<int, User>> _mockUserRepo;
        private Mock<IRepository<int, BloodInventory>> _mockBloodInventoryRepo;
        private Mock<IAppointmentService> _mockAppointmentService;
        private Mock<IBloodInventoryService> _mockBloodInventoryService;
        private BloodDonationContext _context;
        private BloodBankService _bloodBankService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationContext>()
                .UseInMemoryDatabase(databaseName: "BloodBankTestDb")
                .Options;

            _context = new BloodDonationContext(options);
            _mockDonationCenterRepo = new Mock<IRepository<int, DonationCenter>>();
            _mockAppointmentRepo = new Mock<IRepository<int, Appointment>>();
            _mockDonorRepo = new Mock<IRepository<int, Donor>>();
            _mockUserRepo = new Mock<IRepository<int, User>>();
            _mockBloodInventoryRepo = new Mock<IRepository<int, BloodInventory>>();
            _mockAppointmentService = new Mock<IAppointmentService>();
            _mockBloodInventoryService = new Mock<IBloodInventoryService>();

            _bloodBankService = new BloodBankService(
                _context,
                _mockDonationCenterRepo.Object,
                _mockAppointmentRepo.Object,
                _mockDonorRepo.Object,
                _mockUserRepo.Object,
                _mockAppointmentService.Object,
                _mockBloodInventoryRepo.Object,
                _mockBloodInventoryService.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CancelAppointment_ShouldReturnCancelledStatus()
        {
            // Arrange
            var cancelDto = new CancleAppointmentDTO { Appointmentid = 1 };
            var resultDto = new ScheduleAppointmentReturnDTO { Status = "Cancelled" };

            _mockAppointmentService.Setup(service => service.CancelAppointment(It.IsAny<int>()))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _bloodBankService.CancelAppointment(cancelDto);

            // Assert
            Assert.AreEqual("Cancelled", result.Status);
            _mockAppointmentService.Verify(service => service.CancelAppointment(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task ReScheduleAppointment_ShouldUpdateAppointmentDate()
        {
            // Arrange
            var scheduleDto = new ScheduleAppointmentDTO
            {
                UserId = 1,
                Date = DateTime.Now.AddDays(1),
                CenterId = 1
            };
            var resultDto = new ScheduleAppointmentReturnDTO
            {
                AppointmentDate = scheduleDto.Date
            };

            _mockAppointmentService.Setup(service => service.ReScheduleAppointment(It.IsAny<ScheduleAppointmentDTO>()))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _bloodBankService.ReScheduleAppointment(scheduleDto);

            // Assert
            Assert.AreEqual(scheduleDto.Date, result.AppointmentDate);
            _mockAppointmentService.Verify(service => service.ReScheduleAppointment(It.IsAny<ScheduleAppointmentDTO>()), Times.Once);
        }

        [Test]
        public async Task UpdateAppointment_ShouldUpdateInventoryAndReturnResult()
        {
            // Arrange
            var cancelDto = new CancleAppointmentDTO { Appointmentid = 1 };
            var appointment = new Appointment
            {
                AppointmentId = 1,
                DonorId = 1,
                CenterId = 1
            };
            var donor = new Donor { DonorId = 1, UserId = 1, LastDonationDate = DateTime.Now.AddMonths(-6) };
            var user = new User { UserId = 1, BloodType = "A+" };
            var bloodInventory = new BloodInventory { CenterId = 1, BloodType = "A+", Quantity = 5 };

            _mockAppointmentRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(appointment);
            _mockDonorRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(donor);
            _mockUserRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(user);
            _mockBloodInventoryRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(bloodInventory);
            _mockBloodInventoryRepo.Setup(repo => repo.Update(It.IsAny<BloodInventory>())).ReturnsAsync(bloodInventory);

            var resultDto = new ScheduleAppointmentReturnDTO { Status = "Completed" };
            _mockAppointmentService.Setup(service => service.UpdateAppointment(It.IsAny<int>())).ReturnsAsync(resultDto);

            // Act
            var result = await _bloodBankService.UpdateAppointment(cancelDto);

            // Assert
            Assert.AreEqual("Completed", result.Status);
            _mockAppointmentRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _mockDonorRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _mockBloodInventoryRepo.Verify(repo => repo.Update(It.IsAny<BloodInventory>()), Times.Once);
        }

        [Test]
        public async Task ViewAppointments_ShouldReturnAppointments()
        {
            // Arrange
            var center = new DonationCenter { CenterId = 1, UserId = 1 };
            var user = new User { UserId = 1, Name = "John", PhoneNumber = "1234567890", BloodType = "A+" };
            var appointment = new Appointment
            {
                AppointmentId = 1,
                AppointmentDate = DateTime.Now,
                Status = "Scheduled",
                Location = "Center 1",
                CenterId = 1,
                DonorId = 1
            };

            _context.DonationCenters.Add(center);
            _context.Users.Add(user);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var expectedAppointments = new List<ViewAppointmentReturnDTO>
            {
                new ViewAppointmentReturnDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    AppointmentDate = appointment.AppointmentDate,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    BloodType = user.BloodType,
                    Location = appointment.Location,
                    Status = appointment.Status
                }
            };

            // Act
            var result = await _bloodBankService.ViewAppointments(1);

            // Assert
            Assert.AreEqual(expectedAppointments.Count(), result.Count());
            Assert.AreEqual(expectedAppointments.First().Name, result.First().Name);
        }

        [Test]
        public async Task Getappointmentsbyid_ShouldReturnAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment { AppointmentId = 1, CenterId = 1 },
                new Appointment { AppointmentId = 2, CenterId = 1 }
            };

            _context.Appointments.AddRange(appointments);
            await _context.SaveChangesAsync();

            // Act
            var result = await _bloodBankService.Getappointmentsbyid(1);

            // Assert
            Assert.AreEqual(appointments.Count, result.Count());
            Assert.IsTrue(result.All(a => a.CenterId == 1));
        }

        [Test]
        public async Task BloodAvailabilityInInventories_ShouldReturnAvailability()
        {
            // Arrange
            var center = new DonationCenter { CenterId = 1, UserId = 1 };
            var inventory = new BloodInventory { CenterId = 1, BloodType = "A+", Quantity = 10 };

            _context.DonationCenters.Add(center);
            _context.BloodInventorys.Add(inventory);
            await _context.SaveChangesAsync();

            var checkAvailabilityDto = new CheckAvailabilityDTO { CenterId = center.CenterId };
            var expectedAvailability = new List<AvailablityDTO>
            {
                new AvailablityDTO { BloodType = "A+", Quantity = 10 }
            };

            _mockBloodInventoryService.Setup(service => service.CheckAvailability(It.IsAny<CheckAvailabilityDTO>()))
                .ReturnsAsync(expectedAvailability);

            // Act
            var result = await _bloodBankService.BloodAvailabilityInInventories(1);

            // Assert
            Assert.AreEqual(expectedAvailability.Count, result.Count());
            Assert.AreEqual(expectedAvailability.First().BloodType, result.First().BloodType);
        }

        [Test]
        public async Task UpdateInventory_ShouldReturnUpdatedInventory()
        {
            // Arrange
            var updateDto = new UpdateInventory { InventoryId = 1, Quantity = 20,};
            var inventory = new BloodInventory { InventoryId = 1, BloodType = "A+", Quantity = 10 };

            _mockBloodInventoryRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(inventory);
            _mockBloodInventoryRepo.Setup(repo => repo.Update(It.IsAny<BloodInventory>())).ReturnsAsync(inventory);

            var updatedInventory = new BloodInventory { InventoryId = 1, BloodType = "A+", Quantity = 20 };
            _mockBloodInventoryRepo.Setup(repo => repo.Update(It.IsAny<BloodInventory>())).ReturnsAsync(updatedInventory);

            // Act
            var result = await _bloodBankService.UpdateInventory(updateDto);

            // Assert
            Assert.AreEqual(updateDto.Quantity, result.Quantity);
            _mockBloodInventoryRepo.Verify(repo => repo.Update(It.IsAny<BloodInventory>()), Times.Once);
        }

        [Test]
        public async Task AddInventory_ShouldReturnAddedInventory()
        {
            // Arrange
            var addDto = new AddInventoryDTO { CenterId = 1, BloodType = "B+", Quantity = 15 };
            var newInventory = new BloodInventory
            {
                CenterId = 1,
                BloodType = "B+",
                Quantity = 15
            };

            _mockBloodInventoryRepo.Setup(repo => repo.Add(It.IsAny<BloodInventory>())).ReturnsAsync(newInventory);

            // Act
            var result = await _bloodBankService.AddInventory(addDto);

            // Assert
            Assert.AreEqual(addDto.BloodType, result.BloodType);
            Assert.AreEqual(addDto.Quantity, result.Quantity);
            _mockBloodInventoryRepo.Verify(repo => repo.Add(It.IsAny<BloodInventory>()), Times.Once);
        }
    }
}
