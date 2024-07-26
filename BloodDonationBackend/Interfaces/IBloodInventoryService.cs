using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IBloodInventoryService
    {
        public Task<IEnumerable<AvailablityDTO>> CheckAvailability(CheckAvailabilityDTO checkAvailabilityDTO);
        public Task<ReturnInventoryDTO> UpdateInventory(UpdateInventoryDTO updateInventoryDTO);
        public Task<ReturnInventoryDTO> AddInventory(UpdateInventoryDTO updateInventoryDTO);
    }
}
