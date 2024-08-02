namespace BloodDonationBackend.Models.DTOs
{
    public class DonationCenterDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string state { get; set; }
        public string District { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string OperatingHours { get; set; }
    }
}