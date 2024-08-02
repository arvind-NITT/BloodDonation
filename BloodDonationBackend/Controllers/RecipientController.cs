using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class RecipientController : ControllerBase
    {
        private readonly IRecipientService _RecipientService;
        public RecipientController(IRecipientService RecipientService)
        {
            _RecipientService = RecipientService;
        }
        [Authorize]
        [HttpPost("RequestBlood")]
        [ProducesResponseType(typeof(BloodRequestReturnDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RequestBlood([FromBody] BloodRequestDTO bloodRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                int userId = int.Parse(userIdClaim.Value);
                bloodRequestDTO.RecipientId= userId;
                var result = await _RecipientService.RequestBlood(bloodRequestDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("SearchForBlood")]
        [ProducesResponseType(typeof(IEnumerable<DonorSearchReturnDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchForBlood([FromBody] DonorSearchDTO donorSearchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _RecipientService.SearchForBlood(donorSearchDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
         [Authorize]
        [HttpPost("SearchForDonationCenters")]
        [ProducesResponseType(typeof(IEnumerable<DonationCenterInventoryDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchForDonationCenter([FromBody] DonationCenterSearchDTO searchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _RecipientService.SearchForDonationCenters(searchDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [Authorize]
        [HttpGet("ViewRequest")]
        [ProducesResponseType(typeof(IEnumerable<BloodRequestReturnDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ViewRequest()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            var result = await _RecipientService.ViewRequest(userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
   

        [Authorize]
        [HttpPut("UpdateRequest")]
        [ProducesResponseType(typeof(BloodRequestReturnDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRequest([FromBody] UpdateRequestDTO updateRequestDTO)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                int userId = int.Parse(userIdClaim.Value);
                var result = await _RecipientService.UpdateRequest(updateRequestDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("UpdateMedicalInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMedicalInfo([FromBody] medicalinfoupdateDTO medicalInfo)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                int userId = int.Parse(userIdClaim.Value);
                Console.WriteLine(medicalInfo.MedicalCondition);
                Console.WriteLine(medicalInfo);

                var result = await _RecipientService.UpdateMedicalInfo(userId, medicalInfo.MedicalCondition);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
