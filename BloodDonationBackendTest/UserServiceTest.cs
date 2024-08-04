using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Repositories;
using BloodDonationBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BloodDonationBackendTest
{
    public class UserServiceTest
    {
        private BloodDonationContext _context;
        private IRepository<int, UserDetail> _userDetailRepo;
        private IRepository<int, User> _userRepo;
        private IRepository<int, Donor> _donorRepo;
        private IRepository<int, Recipient> _recipientRepo;
        private IRepository<int, DonationCenter> _donationCenterRepo;
        private ITokenService _tokenService;
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloodDonationContext>()
                                 .UseInMemoryDatabase("testDB");
            _context = new BloodDonationContext(optionsBuilder.Options);

            _userDetailRepo = new UserDetailRepository(_context);
            _userRepo = new UserRepo(_context);
            _donorRepo = new DonorRepo(_context);
            _recipientRepo = new RecipientRepo(_context);
            _donationCenterRepo = new DonationCenterRepo(_context);

            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing"},
                {"Jwt:Issuer", "*"},
                {"Jwt:Audience", "*"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _tokenService = new TokenService(configuration);
            _userService = new UserService(_context, _userDetailRepo, _userRepo, _donorRepo, _recipientRepo, _tokenService, _donationCenterRepo);
        }

        [Test]
        public  void RegisterUserTest()
        {
            var newUser = new UserRegisterDTO
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                Password = "password",
                Role = Role.Donor,
                state = "State",
                District = "District",
                BloodType = "O+",
                DOB = new DateTime(2000, 1, 1),
                Gender = "Male",
                Address = "123 Street",
                Pincode = "123456",
                Available = true,
            };

            var result = _userService.Register(newUser);
            Assert.IsNotNull(result);
        }

        [Test]
        public void LoginUserTest()
        {
            var newUser = new UserRegisterDTO
            {
                Name = "Jane Doe",
                PhoneNumber = "0987654321",
                Password = "password",
                Role = Role.Recipient,
                state = "State",
                District = "District",
                BloodType = "A+",
                DOB = new DateTime(1995, 5, 5),
                Gender = "Female",
                Address = "456 Avenue",
                Pincode = "654321",
                MedicalCondition = "Ok"
            };

            _userService.Register(newUser).Wait();

            var loginDTO = new UserLoginDTO
            {
                PhoneNumber = "0987654321",
                Password = "password"
            };

            var result = _userService.Login(loginDTO);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchForDonorTest()
        {
            var searchDTO = new DonorSearchDTO
            {
                BloodType = "O+",
                state = "State",
                District = "District"
            };

            var result = _userService.SearchForDonor(searchDTO).Result;
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchForDonationCentersTest()
        {
            var searchDTO = new DonationCenterSearchDTO
            {
                state = "State",
                District = "District"
            };

            var result = _userService.SearchForDonationCenters(searchDTO).Result;
            Assert.IsNotNull(result);
        }
    }
}
