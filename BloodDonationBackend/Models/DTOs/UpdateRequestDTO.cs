namespace BloodDonationBackend.Models.DTOs
{
    public class UpdateRequestDTO
    {
        public int RequestId { get; set; }
        public int Quantity { get; set; }
        public bool IsUrgent { get; set; }
        public string Status { get; set; }
    }
}