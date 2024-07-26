using BloodDonationBackend.Modal;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models
{
    public class Recipient
    {
       
        public int RecipientId { get; set; }
        public int UserId { get; set; }
        public string MedicalCondition { get; set; }
    }
}
