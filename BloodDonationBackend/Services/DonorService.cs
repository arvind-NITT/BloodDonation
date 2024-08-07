﻿using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Services
{
    public class DonorService : IDonorService
    {
        private readonly BloodDonationContext _context;
        private readonly IRepository<int, DonationCenter> _DonationCenterRepo;
        private readonly IRepository<int, Appointment> _AppointmentRepo;
        private readonly IRepository<int, Donor> _DonorRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IAppointmentService _AppointmentService;

        public DonorService(BloodDonationContext context, IRepository<int,
            DonationCenter> DonationCenterRepo, IRepository<int, Appointment> appointmentRepo,
            IRepository<int, Donor> donorRepo, IRepository<int, User> userRepo, IAppointmentService appointmentService)
        {
            _context = context;
            _DonationCenterRepo = DonationCenterRepo;
            _AppointmentRepo = appointmentRepo;
            _DonorRepo = donorRepo;
            _UserRepo = userRepo;
            _AppointmentService = appointmentService;
        }

        public async Task<ScheduleAppointmentReturnDTO> ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            // Validate if the user and center exist
            var donor = await _context.Donors.FirstOrDefaultAsync(d => d.UserId == scheduleAppointmentDTO.UserId);
            var center = await _context.DonationCenters.FindAsync(scheduleAppointmentDTO.CenterId);

            if (donor == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (center == null)
            {
                throw new ArgumentException("Donation center not found.");
            }

            
             var existingAppointment = await _context.Appointments.FirstOrDefaultAsync(a => a.DonorId == donor.DonorId && a.AppointmentDate == scheduleAppointmentDTO.Date);
             if (existingAppointment != null)
             {
                 throw new InvalidOperationException("An appointment already exists for this user on the selected date.");
             }

            // Create new appointment
            var appointment = new Appointment
            {
                DonorId = donor.DonorId,
                CenterId = center.CenterId,
                AppointmentDate = scheduleAppointmentDTO.Date,
                Location = center.Address + " " + center.District + " " + center.state + " " + center.Pincode,
                Status = "Scheduled"
            };

           await _AppointmentRepo.Add(appointment);

            // Prepare return DTO
            var result = new ScheduleAppointmentReturnDTO
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                Location = appointment.Location,
                Status = appointment.Status
            };

            return result;
        }


        public async Task<IEnumerable<DonationCenterSearchReturnDTO>> SearchDonationCenter(DonationCenterSearchDTO donationCenterSearchDTO)
        {
            var query = _context.DonationCenters.AsQueryable();

            if (!string.IsNullOrEmpty(donationCenterSearchDTO.state))
            {
                query = query.Where(dc => dc.state.ToLower() == donationCenterSearchDTO.state.ToLower());
            }

            if (!string.IsNullOrEmpty(donationCenterSearchDTO.District))
            {
                query = query.Where(dc => dc.District.ToLower() == donationCenterSearchDTO.District.ToLower());
            }

            var result = await query.Select(dc => new DonationCenterSearchReturnDTO
            {
                CenterId = dc.CenterId,
                Name = dc.Name,
                Address = dc.Address,
                ContactInfo = dc.ContactInfo,
                OperatingHours = dc.OperatingHours
            }).ToListAsync();

            return result;
        }


        public async Task<DonorUpdateReturnDTO> GetInfo(int userid)
        {
            // Fetch donor and user details based on the user ID
            var donor = await _context.Donors.Where(d => d.UserId == userid).FirstOrDefaultAsync();
            var user = await _UserRepo.Get(userid);

            // Throw exception if donor or user is not found
            if (donor == null)
            {
                throw new Exception("Donor not found.");
            }
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Map the properties to the DonorUpdateDTO
            var returnDTO = new DonorUpdateReturnDTO
            {
                Name = user.Name,
                DOB = user.DOB,
                BloodType = user.BloodType,
                Gender = user.Gender,
                Father_Name = user.Father_Name,
                state = user.state,
                District = user.District,
                Pincode = user.Pincode,
                Address = user.Address,
                Available = donor.Available,
                LastDonationDate = donor.LastDonationDate,
                TotalDonations = donor.TotalDonations
            };

            return returnDTO;
        }

        public async Task<DonorUpdateReturnDTO> UpdateInfo(int userid, DonorUpdateDTO DonorUpdate)
        {
            var donor = await _context.Donors.Where(d=> d.UserId == userid).FirstOrDefaultAsync();
            var user = await _UserRepo.Get(userid);
            if (donor == null)
            {
                throw new Exception();
            }
            if (user == null) { 
                throw new Exception();
            }
            donor.Available = DonorUpdate.Available;
            await _DonorRepo.Update(donor);

            user.Name = DonorUpdate.Name;
            user.DOB = DonorUpdate.DOB;
            user.BloodType = DonorUpdate.BloodType;
            user.Gender = DonorUpdate.Gender;
            user.Father_Name = DonorUpdate.Father_Name;
            user.state = DonorUpdate.state;
            user.District = DonorUpdate.District;
            user.Pincode = DonorUpdate.Pincode;
            user.Address = DonorUpdate.Address;
            var result  =  await _UserRepo.Update(user);

            var returnDTO = new DonorUpdateReturnDTO
            {
                Name = user.Name,
                DOB = user.DOB,
                BloodType = user.BloodType,
                Gender = user.Gender,
                Father_Name = user.Father_Name,
                state = user.state,
                District = user.District,
                Pincode = user.Pincode,
                Address = user.Address,
                Available = donor.Available,
                LastDonationDate = donor.LastDonationDate,
                TotalDonations = donor.TotalDonations
            };

            return returnDTO;
        }

        public async Task<IEnumerable<ScheduleAppointmentReturnDTO>> ViewAppointments(int DonorId)
        {
            var id  = await _context.Donors.FirstOrDefaultAsync(d=> d.UserId == DonorId);
            if(id == null)
            {
                throw new Exception("Null not found");
            }
            var appointments = await Getappointmentsbyid(id.DonorId);
            var appointmentDTOs = appointments.Select(a => new ScheduleAppointmentReturnDTO
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                Location = a.Location,
                Status = a.Status
            });

            return appointmentDTOs;
        }
        public async Task<IEnumerable<Appointment>> Getappointmentsbyid(int donorid)
        {
            var appointments = await _context.Appointments
                 .Where(a => a.DonorId == donorid)
                 .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<BloodRequestReturnDTO>> RequestInMyDistrict(int userid)
        {
            var user1 = await _UserRepo.Get(userid);
            var requests = await (from request in _context.BloodRequests
                                  join recipient in _context.Recipients on request.RecipientId equals recipient.RecipientId
                                  join user in _context.Users on recipient.UserId equals user.UserId
                                  where request.State == user1.state && request.District == user1.District
                                  select new BloodRequestReturnDTO
                                  {
                                      RequestId = request.RequestId,
                                      RecipientId = recipient.RecipientId,
                                      Name = user.Name,
                                      Contact = user.PhoneNumber,
                                      BloodType = request.BloodType,
                                      Quantity = request.Quantity,
                                      State = request.State,
                                      District = request.District,
                                      RequestDate = request.RequestDate,
                                      IsUrgent = request.IsUrgent,
                                      Status = request.Status
                                  }).ToListAsync();

            return requests;

        }

        public async Task<ScheduleAppointmentReturnDTO> ReScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            return await _AppointmentService.ReScheduleAppointment(scheduleAppointmentDTO);
        }

        public async Task<ScheduleAppointmentReturnDTO> CancelAppointment(CancleAppointmentDTO cancleAppointmentDTO)
        {
            return await _AppointmentService.CancelAppointment(cancleAppointmentDTO.Appointmentid);
        }
    }
}
