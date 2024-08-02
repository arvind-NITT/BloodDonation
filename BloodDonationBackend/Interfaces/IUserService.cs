using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(UserLoginDTO loginDTO);
        public Task<LoginReturnDTO> Register(UserRegisterDTO userDTO);
        public Task<IEnumerable<DonorSearchReturnDTO>> SearchForDonor(DonorSearchDTO donorSearchDTO);
        public Task<IEnumerable<DonationCenterInventoryDTO>> SearchForDonationCenters(DonationCenterSearchDTO searchDTO);
    }
}
