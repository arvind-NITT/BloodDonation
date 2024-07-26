namespace BloodDonationBackend.Models.DTOs
{
    public class DonorUpdateReturnDTO
    {
        public DateTime LastDonationDate { get; set; }
        public int TotalDonations { get; set; }
        public Boolean Available { get; set; }
    }
}