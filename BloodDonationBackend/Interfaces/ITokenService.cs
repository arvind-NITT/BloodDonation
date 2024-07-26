using BloodDonationBackend.Modal;

namespace BloodDonationBackend.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
