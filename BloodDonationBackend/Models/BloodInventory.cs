using System.ComponentModel.DataAnnotations;

namespace BloodDonationBackend.Models
{
    public class BloodInventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int CenterId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
    }
}
