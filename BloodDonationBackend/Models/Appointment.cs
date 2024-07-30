using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int DonorId { get; set; }
        public int CenterId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // e.g., Scheduled, Completed, Cancelled
    }
}
