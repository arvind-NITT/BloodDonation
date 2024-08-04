using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Models;
using System.Security.Cryptography;
using System.Text;
using BloodDonationBackend.Contexts;
using Microsoft.EntityFrameworkCore;
using BloodDonationBackend.Exceptions;
using BloodDonationBackend.Repositories;

namespace BloodDonationBackend.Services
{
    public class UserService : IUserService
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, UserDetail> _UserDetailRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IRepository<int, Donor> _DonorRepo;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, Recipient> _RecipientRepo;
        //private readonly IRepository<int, Transaction> _TransactionRepo;
        private readonly ITokenService _tokenService;

        public UserService(BloodDonationContext context, IRepository<int, UserDetail> UserDetailRepo,
            IRepository<int, User> UserRepo, IRepository<int, Donor> DonorRepo,
            IRepository<int, Recipient> RecipientRepo, ITokenService tokenService, IRepository<int, DonationCenter> donationCenterRepo)
        {
            _context = context;
            _UserDetailRepo = UserDetailRepo;
            _UserRepo = UserRepo;
            _tokenService = tokenService;
            _DonorRepo = DonorRepo;
            _RecipientRepo = RecipientRepo;
            _DonationCenterRepo = donationCenterRepo;
        }
        public async Task<LoginReturnDTO> Login(UserLoginDTO loginDTO)
        {
            var data = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginDTO.PhoneNumber);
            if (data == null) { throw new UnauthorizedUserException("Invalid UserDetailname or password"); }
            var UserDetailDB = await _UserDetailRepo.Get(data.UserId);
            if (UserDetailDB == null)
            {
                throw new UnauthorizedUserException("Invalid UserDetailname or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(UserDetailDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, UserDetailDB.Password);
            if (isPasswordSame)
            {
                var User = await _UserRepo.Get(data.UserId);
                // if(UserDetailDB.Status =="Active")
                //{
                LoginReturnDTO loginReturnDTO = MapUserToLoginReturn(User);
                return loginReturnDTO;
                // }

                throw new UserNotActiveException("Your account is not activated");
            }
            throw new UnauthorizedUserException("Invalid UserDetailname or password");
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }


        private LoginReturnDTO MapUserToLoginReturn(User User)
        {
            LoginReturnDTO returnDTO = new LoginReturnDTO();
            returnDTO.UserID = User.UserId;
            returnDTO.Role = User.Role;
            returnDTO.Token = _tokenService.GenerateToken(User);
            return returnDTO;
        }

        private async Task RevertUserDetailInsert(UserDetail UserDetail)
        {
            await _UserDetailRepo.Delete(UserDetail.UserId);
        }
       private async Task RevertUserInsert(User User)
        {

            await _UserRepo.Delete(User.UserId);
        }

        private UserDetail MapUserUserDetailDTOToUserDetail(UserRegisterDTO UserDTO)
        {
            UserDetail UserDetail = new UserDetail();
            UserDetail.UserId = UserDTO.UserId;
            UserDetail.RegistrationDate = DateTime.Now;
            UserDetail.Status = "Disabled";
            HMACSHA512 hMACSHA = new HMACSHA512();
            UserDetail.PasswordHashKey = hMACSHA.Key;
            UserDetail.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(UserDTO.Password));
            return UserDetail;
        }

        public async Task<LoginReturnDTO> Register(UserRegisterDTO UserDTO)
        {
            var found = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == UserDTO.PhoneNumber);
            if (found != null)
            {
                throw new Exception("User with this PhoneNumber Already Exist, Kindly Login");
            }
            User user = null;
            //Donor donor = null;
            //Recipient recipient = null;
            UserDetail UserDetail = null;
            try
            {
                user = UserDTO;

                UserDetail = MapUserUserDetailDTOToUserDetail(UserDTO);
                user = await _UserRepo.Add(user);

                if (UserDTO.Role == Role.Donor)
                {
                    var donor = new Donor
                    {
                        UserId = user.UserId,
                        LastDonationDate = DateTime.Now, // Set as per your logic
                        TotalDonations = 0, // Initial total donations
                        Available = true // Set initial availability
                    };
                    await _DonorRepo.Add(donor);
                }
                else if (UserDTO.Role == Role.Recipient)
                {
                    var recipient = new Recipient
                    {
                        UserId = user.UserId,
                        MedicalCondition = UserDTO.MedicalCondition // Set medical condition from DTO
                    };
                    await _RecipientRepo.Add(recipient);
                } else if (UserDTO.Role == Role.Bloodbank)
                {
                    var data = new DonationCenter
                    {
                        UserId = user.UserId,
                        Name = UserDTO.Name,
                        state = UserDTO.state,
                        District = UserDTO.District,
                        Pincode = UserDTO.Pincode,
                        Address = UserDTO.Address,
                        ContactInfo = UserDTO.PhoneNumber,
                        OperatingHours =" 9 - 7",
                    };

                    var addedDonationCenter = await _DonationCenterRepo.Add(data);
                    //var resultDTO = new DonationCenterDTO
                    //{
                    //    UserId = addedDonationCenter.UserId,
                    //    Name = addedDonationCenter.Name,
                    //    state = addedDonationCenter.state,
                    //    District = addedDonationCenter.District,
                    //    Pincode = addedDonationCenter.Pincode,
                    //    Address = addedDonationCenter.Address,
                    //    ContactInfo = addedDonationCenter.ContactInfo,
                    //    OperatingHours = addedDonationCenter.OperatingHours
                    //};

                    
                }
                UserDetail.UserId = user.UserId;
                UserDetail = await _UserDetailRepo.Add(UserDetail);
               ((UserRegisterDTO)user).Password = string.Empty;

                var User = await _UserRepo.Get(UserDetail.UserId);
                // if(UserDetailDB.Status =="Active")
                //{
                LoginReturnDTO loginReturnDTO = MapUserToLoginReturn(User);
                return loginReturnDTO;
                //return user;
            }
            catch (Exception) { }
            if (user != null)
                await RevertUserInsert(user);
            if (UserDetail != null && user == null)
                await RevertUserDetailInsert(UserDetail);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        public async Task<IEnumerable<DonorSearchReturnDTO>> SearchForDonor(DonorSearchDTO donorSearchDTO)
        {
            Console.WriteLine(donorSearchDTO);
            var query = from u in _context.Users
                        join d in _context.Donors on u.UserId equals d.UserId
                        where u.BloodType == donorSearchDTO.BloodType
                              && u.state == donorSearchDTO.state
                              && u.District == donorSearchDTO.District
                              && d.Available == true // Include only available donors
                        select new DonorSearchReturnDTO
                        {
                            DonorId = d.DonorId,
                            DonorName = u.Name,
                            ContactNumber = u.PhoneNumber,
                            Available = d.Available
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<DonationCenterInventoryDTO>> SearchForDonationCenters(DonationCenterSearchDTO searchDTO)
        {
            var query = from dc in _context.DonationCenters
                        join bi in _context.BloodInventorys on dc.CenterId equals bi.CenterId
                        where dc.state == searchDTO.state
                              && dc.District == searchDTO.District
                        group bi by new
                        {
                            dc.CenterId,
                            dc.Name,
                            dc.Address,
                            dc.ContactInfo,
                            dc.OperatingHours
                        } into g
                        select new DonationCenterInventoryDTO
                        {
                            CenterId = g.Key.CenterId,
                            CenterName = g.Key.Name,
                            Address = g.Key.Address,
                            ContactInfo = g.Key.ContactInfo,
                            OperatingHours = g.Key.OperatingHours,
                            BloodInventories = g.Select(bi => new BloodInventoryDTO
                            {
                                BloodType = bi.BloodType,
                                Quantity = bi.Quantity
                            }).ToList()
                        };

            return await query.ToListAsync();
        }

    }
}


//user.Name = UserDTO.Name;
//user.Father_Name = UserDTO.Father_Name;
//user.Address = UserDTO.Address;
//user.PhoneNumber = UserDTO.PhoneNumber;
//user.Email = UserDTO.Email;
//user.state = UserDTO.state;
//user.Pincode= UserDTO.Pincode;
//user.District = UserDTO.District;
//user.DOB = UserDTO.DOB;
//user.BloodType= UserDTO.BloodType;
//user.Gender = UserDTO.Gender;