using hrms.Application.Services.Dictionaries;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Shared;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryiFacade _dictionaryiFacade;

        public DictionaryController(IDictionaryiFacade dictionaryiFacade)
        {
            _dictionaryiFacade = dictionaryiFacade;
        }

        #region Gender CRUDs

        /// <summary>
        /// Get Single Gender
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetGender")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> GetGender([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetGenderService.Execute(id, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get Genders list (with filter)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetGenders")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> GetGenders([FromQuery] GenderFilter filter, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetGendersService.Execute(filter, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add new gender or updated existing one
        /// </summary>
        /// <param name="genderDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateGender")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> AddOrUpdateGender([FromBody] GenderDTO genderDTO, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.AddOrUpdateGenderService.Execute(genderDTO, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete gender
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteGener")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteGener([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.DeleteGenerService.Execute(request.Id, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion
    }
}
