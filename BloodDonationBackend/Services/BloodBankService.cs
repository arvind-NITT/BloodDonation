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
            var don = await _DonorRepo.Get(app.DonorId);
            var user = await _UserRepo.Get(don.UserId);
            var Bi = await _context.BloodInventorys.Where(b=> b.CenterId == app.CenterId && b.BloodType == user.BloodType).ToListAsync();
            if (Bi.Any()) {
                var b1 = Bi.First();
                b1.Quantity = b1.Quantity + 1;
                await _BloodInventory.Update(b1);
            }
            else {
                var data = new BloodInventory
                {
                    CenterId = app.CenterId,
                    BloodType = user.BloodType,
                    Quantity = 1
                };
                await _BloodInventory.Add(data);
            }

            return await _AppointmentService.UpdateAppointment(cancleAppointmentDTO.Appointmentid);
        }

        public async Task<IEnumerable<ScheduleAppointmentReturnDTO>> ViewAppointments(int userid)
        {
            var id = await _context.DonationCenters.FirstOrDefaultAsync(d => d.UserId == userid);
            if (id == null)
            {
                throw new Exception("Null not found");
            }
            var appointments = await Getappointmentsbyid(id.CenterId);
            var appointmentDTOs = appointments.Select(a => new ScheduleAppointmentReturnDTO
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                Location = a.Location,
                Status = a.Status
            });

            return appointmentDTOs;
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
    }
}
