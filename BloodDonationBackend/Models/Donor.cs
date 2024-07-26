using BloodDonationBackend.Modal;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models
{
    public class Donor 
    {
       
        public int DonorId { get; set; }

        public int UserId { get; set; }
        public DateTime LastDonationDate { get; set; }
        public int TotalDonations { get; set; }
        public Boolean Available { get; set; }
    }
   
}
