namespace BloodDonationBackend.Models.DTOs
{
    public class DonorSearchReturnDTO
    {
        public int DonorId { get; set; }
        public string DonorName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime LastDonationDate { get; set; }
        public Boolean Available { get; set; }

    }
}