using hrms.Application.Services.Accounting;
using hrms.Infranstructure.Auth;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    }
}
