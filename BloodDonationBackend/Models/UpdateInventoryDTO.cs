namespace BloodDonationBackend.Models
{
    public class UpdateInventoryDTO
    {
        public int CenterId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
    }
}