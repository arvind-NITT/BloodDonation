using BloodDonationBackend.Modal;

namespace BloodDonationBackend.Models.DTOs
{
    public class DonorUpdateDTO
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string BloodType { get; set; }
        public string Gender { get; set; }
        public string Father_Name { get; set; }
        public string state { get; set; }
        public string District { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public Boolean Available { get; set; }
    }
}