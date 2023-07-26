using hrms.Application.Services.Vacation;
using hrms.Domain.Models.User;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;
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

        ///// <summary>
        ///// Register new payed leave request or edit existing one
        ///// </summary>
        ///// <param name="payedLeaveDTO"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //[HttpPost("AddOrUpdatePayedLeave")]
        //public async Task<ActionResult<ServiceResult<PayedLeaveDTO>>> AddOrUpdatePayedLeave([FromBody] PayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken)
        //{
        //    var result = await _vacationsFacade.AddOrUpdatePayedLeaveService.Execute(payedLeaveDTO, cancellationToken).ConfigureAwait(false);
        //    if (result.ErrorOccured)
        //    {
        //        return BadRequest(result);
        //    }

        //    return Ok(result);
        //}

      
        #endregion

    }
}
