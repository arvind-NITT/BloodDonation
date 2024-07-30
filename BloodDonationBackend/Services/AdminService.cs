using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Repositories;
using System.Xml.Linq;

namespace BloodDonationBackend.Services
{
    public class AdminService : IAdminInterface
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, BloodInventory> _BloodInventoryRepo;
        //private readonly IRepository<int, Donor> _DonorRepo;

        public AdminService(BloodDonationContext context, IRepository<int, DonationCenter> DonationCenterRepo, IRepository<int, BloodInventory> BloodInventoryRepo)
        {
            _context = context;
            _DonationCenterRepo = DonationCenterRepo;
            _BloodInventoryRepo = BloodInventoryRepo;
  

        }
        public Task<ReturnInventoryDTO> AddBloodInventory(UpdateInventoryDTO updateInventoryDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<DonationCenterDTO> AddDonationCenter(DonationCenterDTO donationCenterDTO)
        {
            var data = new DonationCenter
            {
                Name = donationCenterDTO.Name,
                state = donationCenterDTO.state,
                District = donationCenterDTO.District,
                Pincode = donationCenterDTO.Pincode,
                Address = donationCenterDTO.Address,
                ContactInfo = donationCenterDTO.ContactInfo,
                OperatingHours = donationCenterDTO.OperatingHours,
            };

            var addedDonationCenter =   await   _DonationCenterRepo.Add(data);
            var resultDTO = new DonationCenterDTO
            {
               
                Name = addedDonationCenter.Name,
                state = addedDonationCenter.state,
                District = addedDonationCenter.District,
                Pincode = addedDonationCenter.Pincode,
                Address = addedDonationCenter.Address,
                ContactInfo = addedDonationCenter.ContactInfo,
                OperatingHours = addedDonationCenter.OperatingHours
            };

            return resultDTO;
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDetailsDTO>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailsDTO> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
