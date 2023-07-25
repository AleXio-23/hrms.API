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


        /// <summary>
        /// Check for specific user, how much payed leaves left for now (quarter/year type rage)
        /// Calculate how much days left from previous quarter/year
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
            [HttpGet("GetCurrentActivePayedLeaves")]
            public async Task<ActionResult<ServiceResult<GetCurrentActivePayedLeavesServiceResponse>>> GetCurrentActivePayedLeaves([FromQuery] int userId, CancellationToken cancellationToken)
            {
                var result = await _vacationsFacade.GetCurrentActivePayedLeavesService.Execute(userId, cancellationToken).ConfigureAwait(false);
                if (result.ErrorOccured)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
    }
}
