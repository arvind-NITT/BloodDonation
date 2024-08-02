﻿using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IDonorService
    {
        public Task<IEnumerable<DonationCenterSearchReturnDTO>> SearchDonationCenter(DonationCenterSearchDTO donationCenterSearchDTO);
        public Task<ScheduleAppointmentReturnDTO> ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO);
        public Task<IEnumerable<ScheduleAppointmentReturnDTO>> ViewAppointments(int DonorId);
        public Task<IEnumerable<BloodRequestReturnDTO>> RequestInMyDistrict(int userid);
        public Task<DonorUpdateReturnDTO> UpdateInfo(int userid, DonorUpdateDTO DonorUpdate);
        public Task<ScheduleAppointmentReturnDTO> ReScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO);
        public Task<ScheduleAppointmentReturnDTO> CancelAppointment(CancleAppointmentDTO cancleAppointmentDTO);
    }
}
