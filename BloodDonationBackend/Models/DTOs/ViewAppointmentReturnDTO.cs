namespace BloodDonationBackend.Models.DTOs
{
    public class ViewAppointmentReturnDTO
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BloodType { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // e.g., Scheduled, Completed, Cancelled
    }
}