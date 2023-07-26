using hrms.Application.Services.Dictionaries;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Domain.Models.Dictionary.WeekWorkingDays;
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
            var result = await _dictionaryiFacade.GetGenderService.Execute(id, cancellationToken).ConfigureAwait(false);
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
        public async Task<ActionResult<ServiceResult<List<GenderDTO>>>> GetGenders([FromQuery] GenderFilter filter, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetGendersService.Execute(filter, cancellationToken).ConfigureAwait(false);
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
            var result = await _dictionaryiFacade.AddOrUpdateGenderService.Execute(genderDTO, cancellationToken).ConfigureAwait(false);
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
            var result = await _dictionaryiFacade.DeleteGenerService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
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
            var result = await _dictionaryiFacade.GetDepartmentService.Execute(id, cancellationToken).ConfigureAwait(false);
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
        public async Task<ActionResult<ServiceResult<List<DepartmentDTO>>>> GetDepartments([FromQuery] DepartmentFilter filter, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetDepartmentsService.Execute(filter, cancellationToken).ConfigureAwait(false);
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
            var result = await _dictionaryiFacade.AddOrUpdateDepartmentService.Execute(departmentDTO, cancellationToken).ConfigureAwait(false);
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
            var result = await _dictionaryiFacade.DeleteDepartmentService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion

        #region JobPosition CRUDs

        /// <summary>
        /// Get single job position
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetJobPosition")]
        public async Task<ActionResult<ServiceResult<JobPositionDTO>>> GetJobPosition([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetJobPositionService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get job positions list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetJobPositions")]
        public async Task<ActionResult<ServiceResult<List<JobPositionDTO>>>> GetJobPositions([FromQuery] JobPositionFilter filter, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetJobPositionsService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add or Update job position based on Id is null or not
        /// </summary>
        /// <param name="jobPositionDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateJobPosition")]
        public async Task<ActionResult<ServiceResult<JobPositionDTO>>> AddOrUpdateJobPosition([FromBody] JobPositionDTO jobPositionDTO, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.AddOrUpdateJobPositionService.Execute(jobPositionDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete job position
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteJobPosition")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteJobPosition([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.DeleteJobPositionService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion

        #region CompanyHoliday CRUDs

        /// <summary>
        /// get company holiday
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCompanyHoliday")]
        public async Task<ActionResult<ServiceResult<CompanyHolidayDTO>>> GetCompanyHoliday([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetCompanyHolidayService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// get company holidays list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCompanyHolidays")]
        public async Task<ActionResult<ServiceResult<List<CompanyHolidayDTO>>>> GetCompanyHolidays([FromQuery] CompanyHolidayFilter filter, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetCompanyHolidaysService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add or update company holiday
        /// </summary>
        /// <param name="companyHolidayDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateCompanyHoliday")]
        public async Task<ActionResult<ServiceResult<CompanyHolidayDTO>>> AddOrUpdateCompanyHolidays([FromBody] CompanyHolidayDTO companyHolidayDTO, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.AddOrUpdateCompanyHolidaysService.Execute(companyHolidayDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete registered company holiday
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCompanyHoliday")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteCompanyHoliday([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.DeleteCompanyHolidayService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion

        #region HolidayRangeType CRUDs

        /// <summary>
        /// Get hiliday range type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetHolidayRangeType")]
        public async Task<ActionResult<ServiceResult<HolidayRangeTypeDTO>>> GetHolidayRangeType([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetHolidayRangeTypeService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get hiliday range types list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetHolidayRangeTypes")]
        public async Task<ActionResult<ServiceResult<List<HolidayRangeTypeDTO>>>> GetHolidayRangeTypes(CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetHolidayRangeTypesService.Execute(cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion

        #region HolidayType CRUDs

        /// <summary>
        /// Get holiday type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetHolidayType")]
        public async Task<ActionResult<ServiceResult<HolidayTypeDTO>>> GetHolidayType([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetHolidayTypeService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get holiday types list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetHolidayTypes")]
        public async Task<ActionResult<ServiceResult<List<HolidayTypeDTO>>>> GetHolidayTypes(CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetHolidayTypesService.Execute(cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion

        #region WeekWorkingDays CRUDs

        /// <summary>
        /// Get week working day by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetWeekWorkingDay")]
        public async Task<ActionResult<ServiceResult<WeekWorkingDayDTO>>> GetWeekWorkingDay([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetWeekWorkingDayService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
 
        /// <summary>
        /// Get all week working days
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetWeekWorkingDays")]
        public async Task<ActionResult<ServiceResult<List<GenderDTO>>>> GetWeekWorkingDays([FromQuery]  CancellationToken cancellationToken)
        {
            var result = await _dictionaryiFacade.GetWeekWorkingDaysService.Execute(  cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion
    }
}
