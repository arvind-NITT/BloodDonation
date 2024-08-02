namespace BloodDonationBackend.Models.DTOs
{
    public class AvailablityDTO
    {
        public int InventoryId { get; set; }
        //public int CenterId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
    }
}