﻿using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models
{
    public class DonationCenter
    {
        [Key]
        public int CenterId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string OperatingHours { get; set; }
    }
}