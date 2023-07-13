using hrms.Application.Services.Dictionaries;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Shared;
using hrms.Shared.Models;
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
        [HttpDelete("DeleteGender")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteGender([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.DeleteGenerService.Execute(request.Id, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion

        #region Departments CRUDs
 
        /// <summary>
        /// Get single department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetDepartment")]
        public async Task<ActionResult<ServiceResult<DepartmentDTO>>> GetDepartment([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetDepartmentService.Execute(id, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get departments list with filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetDepartments")]
        public async Task<ActionResult<ServiceResult<DepartmentDTO>>> GetDepartments([FromQuery] DepartmentFilter filter, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetDepartmentsService.Execute(filter, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add or update department (depend Id is null or not)
        /// </summary>
        /// <param name="departmentDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateDepartment")]
        public async Task<ActionResult<ServiceResult<DepartmentDTO>>> AddOrUpdateDepartment([FromBody] DepartmentDTO departmentDTO, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.AddOrUpdateDepartmentService.Execute(departmentDTO, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete department
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDepartment")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteDepartment([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.DeleteDepartmentService.Execute(request.Id, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion
    }
}
