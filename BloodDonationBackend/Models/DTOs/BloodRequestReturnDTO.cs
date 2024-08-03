namespace BloodDonationBackend.Models.DTOs
{
    public class BloodRequestReturnDTO
    {
        public int RequestId { get; set; }
        public int RecipientId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string BloodType { get; set; }

        public string State { get; set; }
        public string District { get; set; }
        public int Quantity { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsUrgent { get; set; }
        public string Status { get; set; }
    }
}