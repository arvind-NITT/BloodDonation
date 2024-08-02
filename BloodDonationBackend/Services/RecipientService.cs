using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Services
{
    public class RecipientService : IRecipientService
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, Appointment> _AppointmentRepo;
        private readonly IRepository<int, Donor> _DonorRepo;
        private readonly IRepository<int, BloodRequest> _BloodRequestRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IRepository<int, Recipient> _RecipientRepo;

        public RecipientService(BloodDonationContext context, IRepository<int, DonationCenter> DonationCenterRepo, IRepository<int, Appointment> appointmentRepo, IRepository<int, Donor> donorRepo, IRepository<int, User> userRepo, IRepository<int, BloodRequest> BloodRequestRepo, IRepository<int, Recipient> RecipientRepo)
        {
            _context = context;
            _DonationCenterRepo = DonationCenterRepo;
            _AppointmentRepo = appointmentRepo;
            _DonorRepo = donorRepo;
            _UserRepo = userRepo;
            _BloodRequestRepo = BloodRequestRepo;
            _RecipientRepo = RecipientRepo;
        }

        public async Task<BloodRequestReturnDTO> RequestBlood(BloodRequestDTO bloodRequestDTO)
        {
            // Validate the input (optional but recommended)
            if (bloodRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(bloodRequestDTO));
            }
            var id  = await  _context.Recipients.FirstOrDefaultAsync(d=> d.UserId == bloodRequestDTO.RecipientId);
            if (id == null) {
                throw new ArgumentNullException(nameof(bloodRequestDTO));
            }
            
            // Create a new BloodRequest entity from the DTO
            var bloodRequest = new BloodRequest
            {
                RecipientId = id.RecipientId,
                BloodType = bloodRequestDTO.BloodType,
                Quantity = bloodRequestDTO.Quantity,
                State = bloodRequestDTO.State,
                District = bloodRequestDTO.District,
                RequestDate = bloodRequestDTO.RequestDate,
                IsUrgent = bloodRequestDTO.IsUrgent,
                Status = "Pending" // You can set a default status or make this dynamic
            };

            // Add the new blood request to the context
            await _BloodRequestRepo.Add(bloodRequest);

            // Map the saved entity to the return DTO
            var result = new BloodRequestReturnDTO
            {
                RequestId = bloodRequest.RequestId,
                RecipientId = bloodRequest.RecipientId,
                BloodType = bloodRequest.BloodType,
                Quantity = bloodRequest.Quantity,
                State = bloodRequest.State,
                District = bloodRequest.District,
                RequestDate = bloodRequest.RequestDate,
                IsUrgent = bloodRequest.IsUrgent,
                Status = bloodRequest.Status
            };

            return result;
        }

        public async Task<IEnumerable<DonorSearchReturnDTO>> SearchForBlood(DonorSearchDTO donorSearchDTO)
        {
            var query = from donor in _context.Donors
                        join user in _context.Users on donor.UserId equals user.UserId
                        select new
                        {
                            donor.DonorId,
                            user.Name,
                            user.Email,
                            donor.LastDonationDate,
                            donor.TotalDonations,
                            donor.Available,
                            user.BloodType,
                            user.Gender,
                            user.Address,
                            user.PhoneNumber,
                            user.state,
                            user.District
                        };

            if (!string.IsNullOrEmpty(donorSearchDTO.state))
            {
                query = query.Where(dc => dc.state.ToLower() == donorSearchDTO.state.ToLower());
            }

            if (!string.IsNullOrEmpty(donorSearchDTO.District))
            {
                query = query.Where(dc => dc.District.ToLower() == donorSearchDTO.District.ToLower());
            }
            if (!string.IsNullOrEmpty(donorSearchDTO.BloodType))
            {
                query = query.Where(dc => dc.BloodType.ToLower() == donorSearchDTO.BloodType.ToLower());
            }
            var result = await query.Select(dc => new DonorSearchReturnDTO
            {
                DonorId = dc.DonorId,
                DonorName = dc.Name,
                ContactNumber = dc.PhoneNumber, // Ensure PhoneNumber is available in the query
                Available = dc.Available
            }).ToListAsync();

            return result;

        }

        public async Task<IEnumerable<DonationCenterInventoryDTO>> SearchForDonationCenters(DonationCenterSearchDTO searchDTO)
        {
            var query = from dc in _context.DonationCenters
                        join bi in _context.BloodInventorys on dc.CenterId equals bi.CenterId
                        where dc.state == searchDTO.state
                              && dc.District == searchDTO.District
                        group bi by new
                        {
                            dc.CenterId,
                            dc.Name,
                            dc.Address,
                            dc.ContactInfo,
                            dc.OperatingHours
                        } into g
                        select new DonationCenterInventoryDTO
                        {
                            CenterId = g.Key.CenterId,
                            CenterName = g.Key.Name,
                            Address = g.Key.Address,
                            ContactInfo = g.Key.ContactInfo,
                            OperatingHours = g.Key.OperatingHours,
                            BloodInventories = g.Select(bi => new BloodInventoryDTO
                            {
                                BloodType = bi.BloodType,
                                Quantity = bi.Quantity
                            }).ToList()
                        };

            return await query.ToListAsync();
        }

        public async Task<Recipient> UpdateMedicalInfo(int userid,string medicalInfo)
        {
            var data = await  _context.Recipients.FirstOrDefaultAsync(r=> r.UserId == userid);
            if (data == null )
            {
                throw new Exception("User not found");

            }
            data.MedicalCondition = medicalInfo;
           var result  = await _RecipientRepo.Update(data);
            return result;
        }

        public async Task<BloodRequestReturnDTO> UpdateRequest(UpdateRequestDTO updateRequestDTO)
        {
           var req= await _BloodRequestRepo.Get(updateRequestDTO.RequestId);
            if (req == null) {
                throw new Exception("Request  not found");
            }
            req.State = updateRequestDTO.State;
            req.Status = updateRequestDTO.Status;
            req.District = updateRequestDTO.District;
            req.Quantity = updateRequestDTO.Quantity;
            req.IsUrgent = updateRequestDTO.IsUrgent;

            var result = await _BloodRequestRepo.Update(req);

            var retu = new BloodRequestReturnDTO
            {
                IsUrgent = req.IsUrgent,
                Status = req.Status,
                District = req.District,
                Quantity = req.Quantity,
                RecipientId = req.RecipientId,
                RequestDate = req.RequestDate,
                RequestId = req.RequestId,
                BloodType = req.BloodType,
            };
            return retu;
        }

        public async Task<IEnumerable<BloodRequestReturnDTO>> ViewRequest(int Id)
        {
            // Find the recipient by UserId
            var recipient = await _context.Recipients.FirstOrDefaultAsync(r => r.UserId == Id);

            if (recipient == null)
            {
                // Handle the case where the recipient is not found (optional)
                throw new ArgumentNullException(); // or throw an exception, or return an empty list, etc.
            }

            // Fetch the blood requests associated with the recipient
            var requests = await _context.BloodRequests
                                         .Where(br => br.RecipientId == recipient.RecipientId)
                                         .ToListAsync();

            // Map the requests to BloodRequestReturnDTO objects
            var result = requests.Select(request => new BloodRequestReturnDTO
            {
                RequestId = request.RequestId,
                BloodType = request.BloodType,
                Quantity = request.Quantity,
                State = request.State,
                District = request.District,
                RequestDate = request.RequestDate,
                IsUrgent = request.IsUrgent,
                Status = request.Status
            }).ToList();

            return result;
        }

    }
}
