using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Models;
using BloodDonationBackend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackendTest
{
    [TestFixture]
    public class AppointmentServiceTest
    {
        private Mock<IRepository<int, DonationCenter>> _mockDonationCenterRepo;
        private Mock<IRepository<int, Appointment>> _mockAppointmentRepo;
        private Mock<IRepository<int, Donor>> _mockDonorRepo;
        private Mock<IRepository<int, User>> _mockUserRepo;
        private BloodDonationContext _context;
        private AppointmentService _appointmentService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BloodDonationContext>()
                .UseInMemoryDatabase(databaseName: "BloodDonationTestDb")
                .Options;

            _context = new BloodDonationContext(options);
            _mockDonationCenterRepo = new Mock<IRepository<int, DonationCenter>>();
            _mockAppointmentRepo = new Mock<IRepository<int, Appointment>>();
            _mockDonorRepo = new Mock<IRepository<int, Donor>>();
            _mockUserRepo = new Mock<IRepository<int, User>>();

            _appointmentService = new AppointmentService(
                _context,
                _mockDonationCenterRepo.Object,
                _mockAppointmentRepo.Object,
                _mockDonorRepo.Object,
                _mockUserRepo.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CancelAppointment_ShouldCancelAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                AppointmentId = 1,
                Status = "Scheduled",
                Location = "Center 1",
                AppointmentDate = DateTime.Now
            };

            _mockAppointmentRepo.Setup(repo => repo.Get(It.IsAny<int>()))
                .ReturnsAsync(appointment);
            _mockAppointmentRepo.Setup(repo => repo.Update(It.IsAny<Appointment>()))
                .ReturnsAsync(appointment);

            // Act
            var result = await _appointmentService.CancelAppointment(1);

            // Assert
            Assert.AreEqual("Cancelled", result.Status);
            _mockAppointmentRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _mockAppointmentRepo.Verify(repo => repo.Update(It.IsAny<Appointment>()), Times.Once);
        }

        [Test]
        public async Task ReScheduleAppointment_ShouldUpdateAppointmentDate()
        {
            // Arrange
            var appointment = new Appointment
            {
                AppointmentId = 1,
                Status = "Scheduled",
                Location = "Center 1",
                AppointmentDate = DateTime.Now
            };

            _mockAppointmentRepo.Setup(repo => repo.Get(It.IsAny<int>()))
                .ReturnsAsync(appointment);
            _mockAppointmentRepo.Setup(repo => repo.Update(It.IsAny<Appointment>()))
                .ReturnsAsync(appointment);

            var rescheduleDto = new ScheduleAppointmentDTO
            {
                CenterId = 1,
                Date = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await _appointmentService.ReScheduleAppointment(rescheduleDto);

            // Assert
            Assert.AreEqual(rescheduleDto.Date, result.AppointmentDate);
            _mockAppointmentRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _mockAppointmentRepo.Verify(repo => repo.Update(It.IsAny<Appointment>()), Times.Once);
        }

        [Test]
        public void ScheduleAppointment_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.ThrowsAsync<NotImplementedException>(async () =>
                await _appointmentService.ScheduleAppointment(new ScheduleAppointmentDTO()));
        }

        [Test]
        public async Task UpdateAppointment_ShouldUpdateDonorLastDonationDateAndCompleteAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                AppointmentId = 1,
                DonorId = 1,
                Status = "Scheduled",
                Location = "Center 1",
                AppointmentDate = DateTime.Now
            };

            var donor = new Donor
            {
                DonorId = 1,
                LastDonationDate = DateTime.Now.AddMonths(-6)
            };

            _mockAppointmentRepo.Setup(repo => repo.Get(It.IsAny<int>()))
                .ReturnsAsync(appointment);
            _mockAppointmentRepo.Setup(repo => repo.Update(It.IsAny<Appointment>()))
                .ReturnsAsync(appointment);
            _mockDonorRepo.Setup(repo => repo.Get(It.IsAny<int>()))
                .ReturnsAsync(donor);
            _mockDonorRepo.Setup(repo => repo.Update(It.IsAny<Donor>()))
                .ReturnsAsync(donor);

            // Act
            var result = await _appointmentService.UpdateAppointment(1);

            // Assert
            Assert.AreEqual("Completed", result.Status);
            _mockAppointmentRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _mockAppointmentRepo.Verify(repo => repo.Update(It.IsAny<Appointment>()), Times.Once);
            _mockDonorRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
            _mockDonorRepo.Verify(repo => repo.Update(It.IsAny<Donor>()), Times.Once);
        }
    }
}
