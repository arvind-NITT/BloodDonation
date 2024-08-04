using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Services
{
    public class BloodBankService : IBloodBankservice
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, Appointment> _AppointmentRepo;
        private readonly IRepository<int, Donor> _DonorRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IRepository<int, BloodInventory> _BloodInventory;
        private readonly IAppointmentService _AppointmentService;
        private readonly IBloodInventoryService _BloodInventoryService;

        public BloodBankService(BloodDonationContext context, IRepository<int,
            DonationCenter> DonationCenterRepo, IRepository<int, Appointment> appointmentRepo,
            IRepository<int, Donor> donorRepo, IRepository<int, User> userRepo, IAppointmentService appointmentService,
            IRepository<int, BloodInventory> BloodInventory, IBloodInventoryService BloodInventoryService)
        {
            _context = context;
            _DonationCenterRepo = DonationCenterRepo;
            _AppointmentRepo = appointmentRepo;
            _DonorRepo = donorRepo;
            _UserRepo = userRepo;
            _AppointmentService = appointmentService;
            _BloodInventory = BloodInventory;
            _BloodInventoryService = BloodInventoryService;
        }
        public async Task<ScheduleAppointmentReturnDTO> CancelAppointment(CancleAppointmentDTO cancleAppointmentDTO)
        {
            return await _AppointmentService.CancelAppointment(cancleAppointmentDTO.Appointmentid);
        }

        public async Task<ScheduleAppointmentReturnDTO> ReScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            return await _AppointmentService.ReScheduleAppointment(scheduleAppointmentDTO);
        }

        public async Task<ScheduleAppointmentReturnDTO> UpdateAppointment(CancleAppointmentDTO cancleAppointmentDTO)
        {
            var app = await _AppointmentRepo.Get(cancleAppointmentDTO.Appointmentid);
            if (app == null)
            {
                throw new Exception("Appointment not found");
            }
            var don = await _DonorRepo.Get(app.DonorId);
            if (don == null)
            {
                throw new Exception("Donor not found");
            }
            var user = await _UserRepo.Get(don.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var Bi = await _context.BloodInventorys.Where(b => b.CenterId == app.CenterId && b.BloodType == user.BloodType).ToListAsync();
            if (Bi == null)
            {
                throw new Exception("BloodInventory query failed");
            }
            if (Bi.Any())
            {
                var b1 = Bi.First();
                b1.Quantity += 1;
                await _BloodInventory.Update(b1);
            }
            else
            {
                var data = new BloodInventory
                {
                    CenterId = app.CenterId,
                    BloodType = user.BloodType,
                    Quantity = 1
                };
                await _BloodInventory.Add(data);
            }

            var result = await _AppointmentService.UpdateAppointment(cancleAppointmentDTO.Appointmentid);
            if (result == null)
            {
                throw new Exception("UpdateAppointment failed");
            }
            return result;
        }


        public async Task<IEnumerable<ViewAppointmentReturnDTO>> ViewAppointments(int userid)
        {
            var center = await _context.DonationCenters.FirstOrDefaultAsync(d => d.UserId == userid);
            if (center == null)
            {
                throw new Exception("Donation center not found");
            }

            var appointments = await (from appointment in _context.Appointments
                                      join donor in _context.Donors on appointment.DonorId equals donor.DonorId
                                      join user in _context.Users on donor.UserId equals user.UserId
                                      where appointment.CenterId == center.CenterId
                                      orderby appointment.AppointmentDate
                                      select new ViewAppointmentReturnDTO
                                      {
                                          AppointmentId = appointment.AppointmentId,
                                          AppointmentDate = appointment.AppointmentDate,
                                          Name = user.Name,
                                          PhoneNumber = user.PhoneNumber,
                                          BloodType = user.BloodType,
                                          Location = appointment.Location,
                                          Status = appointment.Status
                                      }).ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> Getappointmentsbyid(int centerid)
        {
            var appointments = await _context.Appointments
                 .Where(a => a.CenterId == centerid)
                 .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<AvailablityDTO>> BloodAvailabilityInInventories(int userid)
        {
            var data  = await _context.DonationCenters.Where(d=> d.UserId == userid).ToListAsync();
            var checkAvailabilityDTO = new CheckAvailabilityDTO
            {
                CenterId = data.ToList()[0].CenterId
            };
         
            return await _BloodInventoryService.CheckAvailability(checkAvailabilityDTO);

        }

        public async Task<ReturnInventoryDTO> UpdateInventory(UpdateInventory updateInventoryDTO)
        {
            var getinv = await _BloodInventory.Get(updateInventoryDTO.InventoryId);
            var Upda = new UpdateInventoryDTO
            {
                InventoryId = updateInventoryDTO.InventoryId,
                Quantity = updateInventoryDTO.Quantity,
                BloodType = getinv.BloodType
            };

           return await _BloodInventoryService.UpdateInventory(Upda);

        }
        public async Task<ReturnInventoryDTO> AddInventory(AddInventoryDTO updateInventoryDTO)
        {
            var userid = updateInventoryDTO.CenterId;

            // Find the donation center by userId (centerId in this case)
            var donationCenter = await _context.DonationCenters
                                               .FirstOrDefaultAsync(dc => dc.UserId == userid);

            if (donationCenter == null)
            {
                // Handle the case where the donation center is not found
                throw new KeyNotFoundException("Donation center not found.");
            }
            updateInventoryDTO.CenterId = donationCenter.CenterId;
            return await _BloodInventoryService.AddInventory(updateInventoryDTO);
        }
    }
}
