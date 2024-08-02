using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IAdminInterface
    {
        Task<List<UserDetailsDTO>> GetAllUsersAsync();
        Task<UserDetailsDTO> GetUserByIdAsync(int userId);
        //Task ApproveUserProfileAsync(int userId);
        Task DeleteUserAsync(int userId);
        Task<DonationCenterDTO> AddDonationCenter(DonationCenterDTO donationCenterDTO);
        public Task<LoginReturnDTO> RegisterBloodBank(UserRegisterDTO userDTO);
        public Task<ReturnInventoryDTO> AddBloodInventory(UpdateInventoryDTO updateInventoryDTO);
    }
}
