using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Services;
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


        [HttpPost("AddBloodbank")]
        [ProducesResponseType(typeof(LoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginReturnDTO>> Register(UserRegisterDTO userDTO)
        {
            try
            {
                LoginReturnDTO result = await _AdminService.RegisterBloodBank(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

    }
}
