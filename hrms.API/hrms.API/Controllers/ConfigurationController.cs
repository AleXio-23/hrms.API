using hrms.Application.Services.Configuration;
using hrms.Domain.Models.Configuration;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Shared;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationFacade _configurationFacade;

        public ConfigurationController(IConfigurationFacade configurationFacade)
        {
            _configurationFacade = configurationFacade;
        }


        #region NumberTypesConfigurations CRUDs

        /// <summary>
        /// Get NumberTypeConfiguration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetNumberTypesConfiguration")]
        public async Task<ActionResult<ServiceResult<NumberTypesConfigurationDTO>>> GetNumberTypesConfiguration([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _configurationFacade.GetNumberTypesConfigurationService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get NumberTypeConfigurations list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetNumberTypesConfigurations")]
        public async Task<ActionResult<ServiceResult<NumberTypesConfigurationDTO>>> GetNumberTypesConfigurations([FromQuery] NumberTypesConfigurationFilter filter, CancellationToken cancellationToken)
        {
            var result = await _configurationFacade.GetNumberTypesConfigurationsService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add new NumberTypeConfiguration
        /// </summary>
        /// <param name="numberTypesConfigurationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateNumberTypesConfiguration")]
        public async Task<ActionResult<ServiceResult<DepartmentDTO>>> AddOrUpdateNumberTypesConfiguration([FromBody] NumberTypesConfigurationDTO numberTypesConfigurationDTO, CancellationToken cancellationToken)
        {
            var result = await _configurationFacade.AddOrUpdateNumberTypesConfigurationService.Execute(numberTypesConfigurationDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete NumberTypeConfiguration
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNumberTypesConfiguration")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteNumberTypesConfiguration([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _configurationFacade.DeleteNumberTypesConfigurationService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion
    }
}
