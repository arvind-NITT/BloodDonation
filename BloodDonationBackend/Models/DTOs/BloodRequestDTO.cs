namespace BloodDonationBackend.Models.DTOs
{
    public class BloodRequestDTO
    {
        public int RecipientId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
        public string State {  get; set; }
        public string District {  get; set; }    
        public DateTime RequestDate { get; set; }
        public bool IsUrgent { get; set; }

    }
}