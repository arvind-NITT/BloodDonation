﻿namespace BloodDonationBackend.Models.DTOs
{
    public class ReturnInventoryDTO
    {
        public int InventoryId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
    }
}