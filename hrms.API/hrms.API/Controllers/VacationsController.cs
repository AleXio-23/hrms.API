using hrms.Application.Services.Vacation;
using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;
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
        public async Task<ActionResult<ServiceResult<PayedLeaveDTO>>> AddOrUpdateUnpayedLeave([FromBody] UnpayedLeaveDTO unpayedLeaveDTO, CancellationToken cancellationToken)
        {
            var result = await _vacationsFacade.AddOrUpdateUnpayedLeaveService.Execute(unpayedLeaveDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        #endregion

    }
}
