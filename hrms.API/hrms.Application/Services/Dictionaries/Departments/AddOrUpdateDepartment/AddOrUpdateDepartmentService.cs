using AutoMapper;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.AddOrUpdateDepartment
{
    public class AddOrUpdateDepartmentService : IAddOrUpdateDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Department> _departmentRepository;

        public AddOrUpdateDepartmentService(IMapper mapper, IRepository<Department> departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<ServiceResult<DepartmentDTO>> Execute(DepartmentDTO departmentDTO, CancellationToken cancellationToken)
        {
            if (departmentDTO.Id == null || departmentDTO.Id < 1)
            {
                var newDepartment = _mapper.Map<Department>(departmentDTO);
                var addResult = await _departmentRepository.Add(newDepartment, cancellationToken).ConfigureAwait(false);
                var returnResult = _mapper.Map<DepartmentDTO>(addResult);

                return new ServiceResult<DepartmentDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = returnResult
                };
            }
            else if (departmentDTO.Id > 1)
            {
                var getDepartment = await _departmentRepository.Get(departmentDTO.Id ?? -1, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record on Id:{departmentDTO.Id} not found");
                getDepartment.Name = departmentDTO.Name;
                getDepartment.IsActive = departmentDTO.IsActive;

                var saveResult = await _departmentRepository.Update(getDepartment, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<DepartmentDTO>(saveResult);
                return new ServiceResult<DepartmentDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = resultDto
                };
            }
            else
            {
                throw new Exception("Unexpected error occured");
            }
        }
    }
}
