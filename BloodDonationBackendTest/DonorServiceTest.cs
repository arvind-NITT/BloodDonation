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
    public class DonorServiceTest
    {
        private Mock<IRepository<int, DonationCenter>> _mockDonationCenterRepo;
        private Mock<IRepository<int, Appointment>> _mockAppointmentRepo;
        private Mock<IRepository<int, Donor>> _mockDonorRepo;
        private Mock<IRepository<int, User>> _mockUserRepo;
        private Mock<IAppointmentService> _mockAppointmentService;
        private BloodDonationContext _context;
        private DonorService _donorService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationContext>()
                .UseInMemoryDatabase(databaseName: "DonorServiceTestDb")
                .Options;

            _context = new BloodDonationContext(options);
            _mockDonationCenterRepo = new Mock<IRepository<int, DonationCenter>>();
            _mockAppointmentRepo = new Mock<IRepository<int, Appointment>>();
            _mockDonorRepo = new Mock<IRepository<int, Donor>>();
            _mockUserRepo = new Mock<IRepository<int, User>>();
            _mockAppointmentService = new Mock<IAppointmentService>();

            _donorService = new DonorService(
                _context,
                _mockDonationCenterRepo.Object,
                _mockAppointmentRepo.Object,
                _mockDonorRepo.Object,
                _mockUserRepo.Object,
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
        public async Task ScheduleAppointment_ShouldReturnScheduledAppointment()
        {
            // Arrange
            var scheduleDto = new ScheduleAppointmentDTO { UserId = 1, CenterId = 1, Date = DateTime.Now };
            var donor = new Donor { DonorId = 1, UserId = scheduleDto.UserId };
            var center = new DonationCenter { CenterId = scheduleDto.CenterId, Address = "123 Main St", District = "District A", state = "State A", Pincode = "12345" };

            _context.Donors.Add(donor);
            _context.DonationCenters.Add(center);
            await _context.SaveChangesAsync();

            _mockAppointmentRepo.Setup(repo => repo.Add(It.IsAny<Appointment>()))
                .ReturnsAsync(new Appointment { AppointmentId = 1, DonorId = donor.DonorId, CenterId = center.CenterId, AppointmentDate = scheduleDto.Date, Location = center.Address, Status = "Scheduled" });

            // Act
            var result = await _donorService.ScheduleAppointment(scheduleDto);

            // Assert
            Assert.AreEqual("Scheduled", result.Status);
            Assert.AreEqual(center.Address, result.Location);
            _mockAppointmentRepo.Verify(repo => repo.Add(It.IsAny<Appointment>()), Times.Once);
        }

        [Test]
        public async Task SearchDonationCenter_ShouldReturnMatchingCenters()
        {
            // Arrange
            var searchDto = new DonationCenterSearchDTO { state = "State A", District = "District A" };
            var centers = new List<DonationCenter>
            {
                new DonationCenter { CenterId = 1, Name = "Center 1", Address = "123 Main St", ContactInfo = "1234567890", OperatingHours = "9am - 5pm", state = "State A", District = "District A" },
                new DonationCenter { CenterId = 2, Name = "Center 2", Address = "456 Elm St", ContactInfo = "0987654321", OperatingHours = "10am - 6pm", state = "State A", District = "District B" }
            };

            _context.DonationCenters.AddRange(centers);
            await _context.SaveChangesAsync();

            // Act
            var result = await _donorService.SearchDonationCenter(searchDto);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Center 1", result.First().Name);
        }

        [Test]
        public async Task GetInfo_ShouldReturnDonorAndUserInfo()
        {
            // Arrange
            var userId = 1;
            var donor = new Donor { DonorId = 1, UserId = userId, Available = true, LastDonationDate = DateTime.Now.AddMonths(-1), TotalDonations = 5 };
            var user = new User { UserId = userId, Name = "John Doe", DOB = DateTime.Now.AddYears(-30), BloodType = "O+", Gender = "Male", Father_Name = "Father", state = "State A", District = "District A", Pincode = "12345", Address = "123 Main St" };

            _context.Donors.Add(donor);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _mockUserRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _donorService.GetInfo(userId);

            // Assert
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.BloodType, result.BloodType);
            Assert.AreEqual(donor.Available, result.Available);
            Assert.AreEqual(donor.LastDonationDate, result.LastDonationDate);
        }

        [Test]
        public async Task UpdateInfo_ShouldReturnUpdatedDonorAndUserInfo()
        {
            // Arrange
            var userId = 1;
            var donor = new Donor { DonorId = 1, UserId = userId, Available = true };
            var user = new User { UserId = userId, Name = "John Doe", DOB = DateTime.Now.AddYears(-30), BloodType = "O+", Gender = "Male", Father_Name = "Father", state = "State A", District = "District A", Pincode = "12345", Address = "123 Main St" };

            _context.Donors.Add(donor);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var updateDto = new DonorUpdateDTO
            {
                Name = "Jane Doe",
                DOB = DateTime.Now.AddYears(-25),
                BloodType = "A+",
                Gender = "Female",
                Father_Name = "Father Updated",
                state = "State B",
                District = "District B",
                Pincode = "54321",
                Address = "456 Elm St",
                Available = false
            };

            _mockUserRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(user);
            _mockDonorRepo.Setup(repo => repo.Update(It.IsAny<Donor>())).ReturnsAsync(donor);
            _mockUserRepo.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _donorService.UpdateInfo(userId, updateDto);

            // Assert
            Assert.AreEqual(updateDto.Name, result.Name);
            Assert.AreEqual(updateDto.BloodType, result.BloodType);
            Assert.AreEqual(updateDto.Available, result.Available);
            _mockDonorRepo.Verify(repo => repo.Update(It.IsAny<Donor>()), Times.Once);
            _mockUserRepo.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task ViewAppointments_ShouldReturnAppointments()
        {
            // Arrange
            var donorId = 1;
            var donor = new Donor { DonorId = donorId, UserId = 1 };
            var appointments = new List<Appointment>
            {
                new Appointment { AppointmentId = 1, DonorId = donorId, CenterId = 1, AppointmentDate = DateTime.Now, Location = "Location 1", Status = "Scheduled" },
                new Appointment { AppointmentId = 2, DonorId = donorId, CenterId = 2, AppointmentDate = DateTime.Now.AddDays(1), Location = "Location 2", Status = "Scheduled" }
            };

            _context.Donors.Add(donor);
            _context.Appointments.AddRange(appointments);
            await _context.SaveChangesAsync();

            // Act
            var result = await _donorService.ViewAppointments(donorId);

            // Assert
            Assert.AreEqual(appointments.Count, result.Count());
            Assert.AreEqual(appointments.First().AppointmentId, result.First().AppointmentId);
        }

        [Test]
        public async Task RequestInMyDistrict_ShouldReturnBloodRequests()
        {
            // Arrange
            var userId = 1;
            var user = new User { UserId = userId, state = "State A", District = "District A" };
            var requests = new List<BloodRequest>
            {
                new BloodRequest { RequestId = 1, RecipientId = 1, BloodType = "O+", Quantity = 1, State = "State A", District = "District A", RequestDate = DateTime.Now, IsUrgent = false, Status = "Open" },
                new BloodRequest { RequestId = 2, RecipientId = 2, BloodType = "A+", Quantity = 2, State = "State B", District = "District B", RequestDate = DateTime.Now, IsUrgent = true, Status = "Open" }
            };

            var recipients = new List<Recipient>
            {
                new Recipient { RecipientId = 1, UserId = userId },
                new Recipient { RecipientId = 2, UserId = 2 }
            };

            _context.Users.Add(user);
            _context.BloodRequests.AddRange(requests);
            _context.Recipients.AddRange(recipients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _donorService.RequestInMyDistrict(userId);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(requests.First().RequestId, result.First().RequestId);
        }

        [Test]
        public async Task ReScheduleAppointment_ShouldReturnUpdatedAppointment()
        {
            // Arrange
            var scheduleDto = new ScheduleAppointmentDTO { UserId = 1, CenterId = 1, Date = DateTime.Now };
            var updatedAppointmentDto = new ScheduleAppointmentReturnDTO { AppointmentId = 1, AppointmentDate = scheduleDto.Date, Location = "Updated Location", Status = "Rescheduled" };

            _mockAppointmentService.Setup(service => service.ReScheduleAppointment(scheduleDto))
                .ReturnsAsync(updatedAppointmentDto);

            // Act
            var result = await _donorService.ReScheduleAppointment(scheduleDto);

            // Assert
            Assert.AreEqual(updatedAppointmentDto.Status, result.Status);
            _mockAppointmentService.Verify(service => service.ReScheduleAppointment(scheduleDto), Times.Once);
        }

        [Test]
        public async Task CancelAppointment_ShouldReturnCancellationResult()
        {
            // Arrange
            var cancleDto = new CancleAppointmentDTO { Appointmentid = 1 };
            var cancellationResult = new ScheduleAppointmentReturnDTO { AppointmentId = 1, Status = "Cancelled" };

            _mockAppointmentService.Setup(service => service.CancelAppointment(cancleDto.Appointmentid))
                .ReturnsAsync(cancellationResult);

            // Act
            var result = await _donorService.CancelAppointment(cancleDto);

            // Assert
            Assert.AreEqual(cancellationResult.Status, result.Status);
            _mockAppointmentService.Verify(service => service.CancelAppointment(cancleDto.Appointmentid), Times.Once);
        }
    }
}
