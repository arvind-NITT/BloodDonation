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
    public class RecipientServiceTests
    {
        private RecipientService _recipientService;
        private Mock<IRepository<int, DonationCenter>> _mockDonationCenterRepo;
        private Mock<IRepository<int, Appointment>> _mockAppointmentRepo;
        private Mock<IRepository<int, Donor>> _mockDonorRepo;
        private Mock<IRepository<int, User>> _mockUserRepo;
        private Mock<IRepository<int, BloodRequest>> _mockBloodRequestRepo;
        private Mock<IRepository<int, Recipient>> _mockRecipientRepo;
        private DbContextOptions<BloodDonationContext> _dbContextOptions;
        private BloodDonationContext _context;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BloodDonationContext>()
                .UseInMemoryDatabase(databaseName: "BloodDonationTestDb")
                .Options;

            _context = new BloodDonationContext(_dbContextOptions);

            _mockDonationCenterRepo = new Mock<IRepository<int, DonationCenter>>();
            _mockAppointmentRepo = new Mock<IRepository<int, Appointment>>();
            _mockDonorRepo = new Mock<IRepository<int, Donor>>();
            _mockUserRepo = new Mock<IRepository<int, User>>();
            _mockBloodRequestRepo = new Mock<IRepository<int, BloodRequest>>();
            _mockRecipientRepo = new Mock<IRepository<int, Recipient>>();

            _recipientService = new RecipientService(
                _context,
                _mockDonationCenterRepo.Object,
                _mockAppointmentRepo.Object,
                _mockDonorRepo.Object,
                _mockUserRepo.Object,
                _mockBloodRequestRepo.Object,
                _mockRecipientRepo.Object
            );
        }

        [Test]
        public async Task RequestBlood_ShouldReturnBloodRequestDTO()
        {
            // Arrange
            var recipient = new Recipient { RecipientId = 1, UserId = 1 };
            var bloodRequestDTO = new BloodRequestDTO
            {
                RecipientId = 1,
                BloodType = "O+",
                Quantity = 1,
                State = "California",
                District = "San Francisco",
                RequestDate = DateTime.Now,
                IsUrgent = false
            };

            _context.Recipients.Add(recipient);
            await _context.SaveChangesAsync();

            // Act
            var result = await _recipientService.RequestBlood(bloodRequestDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bloodRequestDTO.BloodType, result.BloodType);
            Assert.AreEqual(bloodRequestDTO.Quantity, result.Quantity);
            Assert.AreEqual(bloodRequestDTO.State, result.State);
            Assert.AreEqual(bloodRequestDTO.District, result.District);
            Assert.AreEqual(bloodRequestDTO.IsUrgent, result.IsUrgent);
        }

        [Test]
        public async Task SearchForBlood_ShouldReturnDonorSearchReturnDTO()
        {
            // Arrange
            var donor = new Donor { DonorId = 1, UserId = 1, Available = true, LastDonationDate = DateTime.Now };
            var user = new User { UserId = 1, Name = "John Doe", BloodType = "O+", PhoneNumber = "1234567890", state = "California", District = "San Francisco" };

            _context.Donors.Add(donor);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var searchDTO = new DonorSearchDTO { state = "California", District = "San Francisco" };

            // Act
            var result = await _recipientService.SearchForBlood(searchDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(donor.DonorId, result.First().DonorId);
        }

        [Test]
        public async Task SearchForDonationCenters_ShouldReturnDonationCenterInventoryDTO()
        {
            // Arrange
            var donationCenter = new DonationCenter { CenterId = 1, Name = "Center 1", Address = "123 Main St", ContactInfo = "123-456-7890", OperatingHours = "9AM - 5PM", state = "California", District = "San Francisco" };
            var bloodInventory = new BloodInventory { CenterId = 1, BloodType = "O+", Quantity = 10 };

            _context.DonationCenters.Add(donationCenter);
            _context.BloodInventorys.Add(bloodInventory);
            await _context.SaveChangesAsync();

            var searchDTO = new DonationCenterSearchDTO { state = "California", District = "San Francisco" };

            // Act
            var result = await _recipientService.SearchForDonationCenters(searchDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(donationCenter.CenterId, result.First().CenterId);
        }

        [Test]
        public async Task UpdateMedicalInfo_ShouldReturnUpdatedRecipient()
        {
            // Arrange
            var recipient = new Recipient { RecipientId = 1, UserId = 1, MedicalCondition = "Diabetes" };
            _context.Recipients.Add(recipient);
            await _context.SaveChangesAsync();

            var newMedicalInfo = "Hypertension";

            // Act
            var result = await _recipientService.UpdateMedicalInfo(1, newMedicalInfo);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newMedicalInfo, result.MedicalCondition);
        }

        [Test]
        public async Task CencelRequest_ShouldReturnUpdatedBloodRequest()
        {
            // Arrange
            var bloodRequest = new BloodRequest { RequestId = 1, Status = "Pending" };
            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();

            var cancleRequestDTO = new CancleRequestDTO { requestid = 1 };

            // Act
            var result = await _recipientService.CencelRequest(cancleRequestDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Cencelled", result.Status);
        }

        [Test]
        public async Task ApproveRequest_ShouldReturnUpdatedBloodRequest()
        {
            // Arrange
            var bloodRequest = new BloodRequest { RequestId = 1, Status = "Pending" };
            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();

            var cancleRequestDTO = new CancleRequestDTO { requestid = 1 };

            // Act
            var result = await _recipientService.ApproveRequest(cancleRequestDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Approved", result.Status);
        }

        [Test]
        public async Task UpdateRequest_ShouldReturnUpdatedBloodRequest()
        {
            // Arrange
            var bloodRequest = new BloodRequest { RequestId = 1, State = "California", Status = "Pending" };
            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();

            var updateRequestDTO = new UpdateRequestDTO
            {
                RequestId = 1,
                State = "Nevada",
                Status = "Updated",
                District = "Las Vegas",
                Quantity = 2,
                IsUrgent = true
            };

            // Act
            var result = await _recipientService.UpdateRequest(updateRequestDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateRequestDTO.State, result.State);
            Assert.AreEqual(updateRequestDTO.Status, result.Status);
            Assert.AreEqual(updateRequestDTO.District, result.District);
            Assert.AreEqual(updateRequestDTO.Quantity, result.Quantity);
            Assert.AreEqual(updateRequestDTO.IsUrgent, result.IsUrgent);
        }

        [Test]
        public async Task ViewRequest_ShouldReturnListOfBloodRequests()
        {
            // Arrange
            var recipient = new Recipient { RecipientId = 1, UserId = 1 };
            var bloodRequest = new BloodRequest { RequestId = 1, RecipientId = 1, Status = "Pending" };

            _context.Recipients.Add(recipient);
            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();

            // Act
            var result = await _recipientService.ViewRequest(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(bloodRequest.RequestId, result.First().RequestId);
        }
    }
}
