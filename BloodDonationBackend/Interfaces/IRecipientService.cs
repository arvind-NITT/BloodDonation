using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;

namespace BloodDonationBackend.Interfaces
{
    public interface IRecipientService
    {
        public Task<IEnumerable<DonorSearchReturnDTO>> SearchForBlood(DonorSearchDTO donorSearchDTO);
        public Task<IEnumerable<DonationCenterInventoryDTO>> SearchForDonationCenters(DonationCenterSearchDTO searchDTO);
        public Task<BloodRequestReturnDTO> RequestBlood(BloodRequestDTO donationCenterSearchDTO);
        public Task<IEnumerable<BloodRequestReturnDTO>> ViewRequest(int Id);
        public Task<Recipient> UpdateMedicalInfo(int userid,string medicalInfo);
        public Task<BloodRequestReturnDTO> UpdateRequest(UpdateRequestDTO updateRequestDTO);
        public  Task<BloodRequestReturnDTO> CencelRequest(CancleRequestDTO cancleRequestDTO);
        public Task<BloodRequestReturnDTO> ApproveRequest(CancleRequestDTO cancleRequestDTO);
    }
}
