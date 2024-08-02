namespace BloodDonationBackend.Models.DTOs
{
    public class AddInventoryDTO
    {
        public int CenterId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
    }
}