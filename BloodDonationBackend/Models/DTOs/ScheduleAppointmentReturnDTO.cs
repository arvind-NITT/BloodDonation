namespace BloodDonationBackend.Models.DTOs
{
    public class ScheduleAppointmentReturnDTO
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // e.g., Scheduled, Completed, Cancelled
    }
}