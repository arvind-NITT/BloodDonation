using BloodDonationBackend.Modal;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models.DTOs
{
    public class UserRegisterDTO : User
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }
        public Boolean Available { get; set; }
        public string MedicalCondition { get; set; }

    }
}
