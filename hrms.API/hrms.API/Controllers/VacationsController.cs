using hrms.Application.Services.Vacation;
using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationsFacade _vacationsFacade;

        public VacationsController(IVacationsFacade vacationsFacade)
        {
            _vacationsFacade = vacationsFacade;
        }

        #region Payed Leaves

        /// <summary>
        /// Check for specific user, how much payed leaves left for now (quarter/year type rage)
        /// Calculate how much days left from previous quarter/year
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCurrentActivePayedLeaves")]
        public async Task<ActionResult<ServiceResult<GetCurrentActiveLeavesServiceResponse>>> GetCurrentActivePayedLeaves([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetCurrentActivePayedLeavesService.Execute(userId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Register new payed leave request or edit existing one
        /// </summary>
        /// <param name="payedLeaveDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdatePayedLeave")]
        public async Task<ActionResult<ServiceResult<PayedLeaveDTO>>> AddOrUpdatePayedLeave([FromBody] PayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.AddOrUpdatePayedLeaveService.Execute(payedLeaveDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get all payed leaves
        /// Need to check that, by default, user can get only his/her payedleaves list,
        /// but for managers, they can see only their department users payed leaves
        /// for much higher positions, can see all users payed leaves
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetAllPayedLeaves")]
        public async Task<ActionResult<ServiceResult<List<PayedLeaveDTOWithUserDTO>>>> GetAllPayedLeaves([FromQuery] GetAllPayedLeavesServiceFilter filter, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetAllPayedLeavesService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get single payed leave with request author and approve author info
        /// </summary>
        /// <param name="payedLeaveId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetPayedLeave")]
        public async Task<ActionResult<ServiceResult<PayedLeaveDTOWithUserDTO>>> GetPayedLeave([FromQuery] int payedLeaveId, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetPayedLeaveService.Execute(payedLeaveId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Approve or not user's payed leave request
        /// TODO: onlymanagers or higher positions can approve  this request
        /// for managers, onli their departmnt users
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("ApproveOrNotPayedLeaves")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<PayedLeaveDTOWithUserDTO>>> ApproveOrNotPayedLeaves([FromBody] ApproveOrNotLeavesRequest request, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.ApproveOrNotPayedLeavesService.Execute(request, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion

        #region Unpayed Leaves

        /// <summary>
        /// Check for specific user, how much payed leaves left for now (quarter/year type rage)
        /// Calculate how much days left from previous quarter/year
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCurrentActiveUnpayedLeaves")]
        public async Task<ActionResult<ServiceResult<GetCurrentActiveLeavesServiceResponse>>> GetCurrentActiveUnpayedLeaves([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetCurrentActiveUnpayedLeavesService.Execute(userId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Register new unpayed leave request or edit existing one
        /// </summary>
        /// <param name="unpayedLeaveDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateUnpayedLeave")]
        public async Task<ActionResult<ServiceResult<UnpayedLeaveDTO>>> AddOrUpdateUnpayedLeave([FromBody] UnpayedLeaveDTO unpayedLeaveDTO, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.AddOrUpdateUnpayedLeaveService.Execute(unpayedLeaveDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get all unpayed leaves
        /// Need to check that, by default, user can get only his/her unpayedleaves list,
        /// but for managers, they can see only their department users unpayed leaves
        /// for much higher positions, can see all users unpayed leaves
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetAllUnpayedLeaves")]
        public async Task<ActionResult<ServiceResult<List<UnpayedLeaveDTOWithUserDTO>>>> GetAllUnpayedLeaves([FromQuery] GetAllUnpayedLeavesServiceFilter filter, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetAllUnpayedLeavesService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get single payed leave with request author and approve author info 
        /// </summary>
        /// <param name="unpayedLeaveId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUnpayedLeave")]
        public async Task<ActionResult<ServiceResult<UnpayedLeaveDTOWithUserDTO>>> GetUnpayedLeave([FromQuery] int unpayedLeaveId, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetUnpayedLeaveService.Execute(unpayedLeaveId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Approve or not user's unpayed leave request
        /// TODO: only managers or higher positions can approve  this request
        /// for managers, only their departmnt users
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("ApproveOrNotUnpayedLeaves")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<UnpayedLeaveDTOWithUserDTO>>> ApproveOrNotUnpayedLeaves([FromBody] ApproveOrNotLeavesRequest request, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.ApproveOrNotUnpayedLeavesService.Execute(request, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion

        #region Sick Leaves

        /// <summary>
        /// Check for specific user, how much sick leaves left for now (quarter/year type rage)
        /// Calculate how much days left from previous quarter/year
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCurrentActiveSickLeaves")]
        public async Task<ActionResult<ServiceResult<GetCurrentActiveLeavesServiceResponse>>> GetCurrentActiveSickLeaves([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetCurrentActiveSickLeavesService.Execute(userId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Register new or update existing sick leave
        /// </summary>
        /// <param name="sickLeaveDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateSickLeave")]
        public async Task<ActionResult<ServiceResult<SickLeaveDTO>>> AddOrUpdateSickLeave([FromBody] SickLeaveDTO sickLeaveDTO, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.AddOrUpdateSickLeaveService.Execute(sickLeaveDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("GetAllSickLeaves")]
        public async Task<ActionResult<ServiceResult<List<SickLeaveDTOWithUserDTO>>>> GetAllSickLeaves([FromQuery] GetAllSickLeavesServiceFilter filter, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetAllSickLeavesService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get single sick leave with request author and approve author info 
        /// </summary>
        /// <param name="sickLeaveId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetSickLeave")]
        public async Task<ActionResult<ServiceResult<SickLeaveDTOWithUserDTO>>> GetSickLeave([FromQuery] int sickLeaveId, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.GetSickLeaveService.Execute(sickLeaveId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("ApproveOrNotSickLeaves")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<SickLeaveDTOWithUserDTO>>> ApproveOrNotSickLeaves([FromBody] ApproveOrNotLeavesRequest request, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.ApproveOrNotSickLeavesService.Execute(request, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion
        //დასამატებელია საათობრივი გასვლები
    }
}
