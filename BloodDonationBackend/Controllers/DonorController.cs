﻿using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Services;
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
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _DonorService;
        public DonorController(IDonorService DonorService)
        {
            _DonorService= DonorService;
        }

        [HttpPost("SearchDonationCenter")]
        [ProducesResponseType(typeof(List<DonationCenterSearchReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DonationCenterSearchReturnDTO>>> SearchDonationCenter( DonationCenterSearchDTO donationCenterSearchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel(StatusCodes.Status400BadRequest, "Invalid search criteria."));
            }


            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var result = await _DonorService.SearchDonationCenter(donationCenterSearchDTO);

                if (result == null )
                {
                    return NotFound(new ErrorModel(StatusCodes.Status404NotFound, "No donation centers found with the given criteria."));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
               // _logger.LogError($"Error occurred while searching donation centers: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(StatusCodes.Status500InternalServerError, "An unexpected error occurred."));
            }
        }

        [Authorize]
        [HttpPost("ScheduleAppointment")]
        [ProducesResponseType(typeof(ScheduleAppointmentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduleAppointmentReturnDTO>> ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel(StatusCodes.Status400BadRequest, "Invalid data."));
            }

            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                int userId = int.Parse(userIdClaim.Value);
                scheduleAppointmentDTO.UserId = userId;
                var result = await _DonorService.ScheduleAppointment(scheduleAppointmentDTO);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ErrorModel(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(StatusCodes.Status500InternalServerError, "An unexpected error occurred."));
            }
        }

        [Authorize]
        [HttpPut("UpdateInfo")]
        [ProducesResponseType(typeof(DonorUpdateReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInfo([FromBody] DonorUpdateDTO donorUpdateDTO)
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
                var updatedDonor = await _DonorService.UpdateInfo(userId,donorUpdateDTO);
                return Ok(updatedDonor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, "An error occurred while updating the donor."));
            }
        }

    }
}
