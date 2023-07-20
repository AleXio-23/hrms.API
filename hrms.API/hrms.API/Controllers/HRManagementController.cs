using hrms.Application.Services.Accounting;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRManagementController : ControllerBase
    {
        private readonly IAccountingFacade _accountingFacade;

        public HRManagementController(IAccountingFacade accountingFacade)
        {
            _accountingFacade = accountingFacade;
        }

        /// <summary>
        /// Start work accounting
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("StartAccounting")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<bool>>> StartAccounting(CancellationToken cancellationToken)
        {
            var result = await _accountingFacade.StartAccountingService.Execute(cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Finish work accountig
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("FinishWorkAccounting")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<bool>>> FinishWorkAccounting(CancellationToken cancellationToken)
        {
            var result = await _accountingFacade.FinishAccountingService.Execute(cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Finish work accountig
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("TakeBreak")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<bool>>> TakeBreak(CancellationToken cancellationToken)
        {
            var result = await _accountingFacade.TakeBreakService.Execute(cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Return from break 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("GetBackFromBreak")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<bool>>> GetBackFromBreak(CancellationToken cancellationToken)
        {
            var result = await _accountingFacade.GetBackFromBreakService.Execute(cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
