using static BloodDonationBackend.Services.UserService;

namespace BloodDonationBackend.Models.DTOs
{
    public class DonationCenterInventoryDTO
    {
        public int CenterId { get; set; }
        public string CenterName { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string OperatingHours { get; set; }
        public List<BloodInventoryDTO> BloodInventories { get; set; }
    }
}
