using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminInterface _AdminService;
        public AdminController(IAdminInterface AdminService) {
            _AdminService = AdminService;
        }
        [HttpPost]
        [Route("addDonationCenter")]
        public async Task<ActionResult<DonationCenterDTO>> AddDonationCenter([FromBody] DonationCenterDTO donationCenterDTO)
        {
            if (donationCenterDTO == null)
            {
                return BadRequest("Donation center data is required.");
            }

            try
            {
                var result = await _AdminService.AddDonationCenter(donationCenterDTO);
                return CreatedAtAction(nameof(AddDonationCenter), new { id = result }, result);
            }
            catch (Exception ex)
            {
                // Log the exception (using your logging framework)
                return StatusCode(500, "An error occurred while adding the donation center.");
            }
        }
    }
}
