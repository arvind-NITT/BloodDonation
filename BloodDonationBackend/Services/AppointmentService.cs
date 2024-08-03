using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, Appointment> _AppointmentRepo;
        private readonly IRepository<int, Donor> _DonorRepo;
        private readonly IRepository<int, User> _UserRepo;

        public AppointmentService(BloodDonationContext context, IRepository<int,
            DonationCenter> DonationCenterRepo, IRepository<int, Appointment> appointmentRepo,
            IRepository<int, Donor> donorRepo, IRepository<int, User> userRepo)
        {
            _context = context;
            _DonationCenterRepo = DonationCenterRepo;
            _AppointmentRepo = appointmentRepo;
            _DonorRepo = donorRepo;
            _UserRepo = userRepo;
        }
        public async Task<ScheduleAppointmentReturnDTO> CancelAppointment(int id)
        {
            var app = await _AppointmentRepo.Get(id);
            if (app != null)
            {
                app.Status = "Cancelled";
                await _AppointmentRepo.Update(app);
            }
            return new ScheduleAppointmentReturnDTO
            {
                Status = app.Status,
                Location = app.Location,
                AppointmentDate = app.AppointmentDate,
                AppointmentId = app.AppointmentId,

            };
        }

        public async Task<ScheduleAppointmentReturnDTO> ReScheduleAppointment(ScheduleAppointmentDTO rescheduleAppointmentDTO)
        {
            var app = await _AppointmentRepo.Get(rescheduleAppointmentDTO.CenterId);
            if (app != null)
            {
                app.AppointmentDate = rescheduleAppointmentDTO.Date;
                await _AppointmentRepo.Update(app);
            }
            return new ScheduleAppointmentReturnDTO
            {
                Status = app.Status,
                Location = app.Location,
                AppointmentDate = app.AppointmentDate,
                AppointmentId = app.AppointmentId,

            };
        }

        public Task<ScheduleAppointmentReturnDTO> ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ScheduleAppointmentReturnDTO> UpdateAppointment(int id)
        {
            var app = await _AppointmentRepo.Get(id);
            var don = await _DonorRepo.Get(app.DonorId);
            don.LastDonationDate = DateTime.Now;
            await _DonorRepo.Update(don);
            if (app == null) { 
                throw new NotImplementedException();
            }
          
                app.Status = "Completed";
             app =   await _AppointmentRepo.Update(app);
            
            return new ScheduleAppointmentReturnDTO
            {
                Status = app.Status,
                Location = app.Location,
                AppointmentDate = app.AppointmentDate,
                AppointmentId = app.AppointmentId,

            };
        }
    }
}
