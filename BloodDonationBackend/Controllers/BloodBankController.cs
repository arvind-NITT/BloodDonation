using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models.DTOs;
using BloodDonationBackend.Models;
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
    public class BloodBankController : ControllerBase
    {
        private readonly IBloodBankservice _BloodBankservice;
        public BloodBankController(IBloodBankservice BloodBankservice)
        {
            _BloodBankservice = BloodBankservice;
        }

        [Authorize]
        [HttpGet("ViewAppointment")]
        [ProducesResponseType(typeof(List<ViewAppointmentReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ViewAppointmentReturnDTO>>> ViewAppointment()
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

                var result = await _BloodBankservice.ViewAppointments(userId);
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
        [HttpGet("BloodAvailabilityInInventories")]
        [ProducesResponseType(typeof(List<AvailablityDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AvailablityDTO>>> BloodAvailabilityInInventory()
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

                
                var result = await _BloodBankservice.BloodAvailabilityInInventories(userId);
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
        [HttpPut("ReScheduleAppointment")]
        [ProducesResponseType(typeof(ScheduleAppointmentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReScheduleAppointment([FromBody] ScheduleAppointmentDTO scheduleAppointmentDTO)
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
                var reschedule = await _BloodBankservice.ReScheduleAppointment(scheduleAppointmentDTO);
                return Ok(reschedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, "An error occurred while updating the Appointment."));
            }
        }
        [Authorize]
        [HttpPut("UpdateAppointment")]
        [ProducesResponseType(typeof(ScheduleAppointmentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAppointment([FromBody] CancleAppointmentDTO cancleAppointmentDTO)
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
                var reschedule = await _BloodBankservice.UpdateAppointment(cancleAppointmentDTO);
                return Ok(reschedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, "An error occurred while updating the Appointment."));
            }
        }

        [Authorize]
        [HttpPut("UpdateInventory")]
        [ProducesResponseType(typeof(ScheduleAppointmentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInventorys([FromBody] UpdateInventory cancleAppointmentDTO)
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
                var reschedule = await _BloodBankservice.UpdateInventory(cancleAppointmentDTO);
                return Ok(reschedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, "An error occurred while updating the Appointment."));
            }
        }

         [Authorize]
        [HttpPut("CancelAppointment")]
        [ProducesResponseType(typeof(ScheduleAppointmentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelAppointment([FromBody] CancleAppointmentDTO cancleAppointmentDTO)
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
                var reschedule = await _BloodBankservice.CancelAppointment(cancleAppointmentDTO);
                return Ok(reschedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, "An error occurred while updating the Appointment."));
            }
        }

    }
}
