using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IAppointmentService
    {
        public Task<ScheduleAppointmentReturnDTO> ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO);
        public Task<ScheduleAppointmentReturnDTO> ReScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO);
        public Task<ScheduleAppointmentReturnDTO> CancelAppointment(int id);

        public Task<ScheduleAppointmentReturnDTO> UpdateAppointment(int id);

    }
}
