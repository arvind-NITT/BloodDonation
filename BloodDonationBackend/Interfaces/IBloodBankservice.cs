using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IBloodBankservice
    {
        public Task<IEnumerable<ViewAppointmentReturnDTO>> ViewAppointments(int centerid);
        public Task<IEnumerable<AvailablityDTO>> BloodAvailabilityInInventories(int userid);
        public Task<ScheduleAppointmentReturnDTO> ReScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO);
        public Task<ScheduleAppointmentReturnDTO> CancelAppointment(CancleAppointmentDTO cancleAppointmentDTO);
        public Task<ScheduleAppointmentReturnDTO> UpdateAppointment(CancleAppointmentDTO cancleAppointmentDTO);

        public Task<ReturnInventoryDTO> UpdateInventory(UpdateInventory updateInventoryDTO);
        public Task<ReturnInventoryDTO> AddInventory(AddInventoryDTO updateInventoryDTO);


    }
}
