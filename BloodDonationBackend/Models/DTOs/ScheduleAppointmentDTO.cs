namespace BloodDonationBackend.Models.DTOs
{
    public class ScheduleAppointmentDTO
    {
        public int UserId { get; set; }
        public int CenterId { get; set; }
        public DateTime Date { get; set; }
    }
}