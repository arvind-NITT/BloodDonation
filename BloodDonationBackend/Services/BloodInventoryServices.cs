using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Services
{
    public class BloodInventoryServices : IBloodInventoryService
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, Appointment> _AppointmentRepo;
        private readonly IRepository<int, Donor> _DonorRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IRepository<int, BloodInventory> _BloodInventoryRepo;
        private readonly IAppointmentService _AppointmentService;

        public BloodInventoryServices(BloodDonationContext context, IRepository<int,
            DonationCenter> DonationCenterRepo, IRepository<int, Appointment> appointmentRepo,
            IRepository<int, Donor> donorRepo, IRepository<int, User> userRepo, IRepository<int, BloodInventory> BloodInventoryRepo, IAppointmentService appointmentService)
        {
            _context = context;
            _DonationCenterRepo = DonationCenterRepo;
            _AppointmentRepo = appointmentRepo;
            _DonorRepo = donorRepo;
            _UserRepo = userRepo;
            _AppointmentService = appointmentService;
            _BloodInventoryRepo = BloodInventoryRepo;
        }
        public async Task<ReturnInventoryDTO> AddInventory(AddInventoryDTO updateInventoryDTO)
        {

            var newblood = new BloodInventory
            {
                CenterId = updateInventoryDTO.CenterId,
                BloodType = updateInventoryDTO.BloodType,
                Quantity = updateInventoryDTO.Quantity,
            };
            var result = await _BloodInventoryRepo.Add(newblood);
            return new ReturnInventoryDTO
            {
                InventoryId = result.InventoryId,
                BloodType = newblood.BloodType,
                Quantity = newblood.Quantity,

            };

        }

        public async Task<IEnumerable<AvailablityDTO>> CheckAvailability(CheckAvailabilityDTO checkAvailabilityDTO)
        {
            if (checkAvailabilityDTO== null ) {
                throw new ArgumentNullException("Dto Cannot be null");
            }
            var data = await _context.BloodInventorys.Where(a=> a.CenterId == checkAvailabilityDTO.CenterId).ToListAsync();

            var result = data.Select(a => new AvailablityDTO
            {
               InventoryId = a.InventoryId,
               BloodType = a.BloodType,
               Quantity = a.Quantity,
            });

            return result;
        }

        public async Task<ReturnInventoryDTO> UpdateInventory(UpdateInventoryDTO updateInventoryDTO)
        {

            var data  = await _BloodInventoryRepo.Get(updateInventoryDTO.InventoryId);
            if(data==null)
                throw new ArgumentNullException("Dto Cannot be null");

            data.BloodType = updateInventoryDTO.BloodType;
            data.Quantity = updateInventoryDTO.Quantity;
           var result = await _BloodInventoryRepo.Update(data);

            return new ReturnInventoryDTO
            {
                InventoryId = result.InventoryId,
                BloodType = result.BloodType,
                Quantity = result.Quantity,
            };

        }
    }
}
