using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models
{
    public class BloodRequest
    {
        [Key]
        public int RequestId { get; set; }
        public int RecipientId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsUrgent { get; set; }
        public string Status { get; set; } // e.g., Pending, Fulfilled, Cancelled
    }
}
