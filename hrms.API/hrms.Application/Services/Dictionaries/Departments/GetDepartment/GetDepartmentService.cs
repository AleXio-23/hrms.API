using AutoMapper;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.GetDepartment
{
    public class GetDepartmentService : IGetDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public GetDepartmentService(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DepartmentDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                throw new ArgumentException("Wrong id value");
            }

            var getDepartment = await _departmentRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record with Id: {id} not found.");

            var departmentDTO = _mapper.Map<DepartmentDTO>(getDepartment);

            return new ServiceResult<DepartmentDTO>()
            {
                Success = true,
                ErrorOccured = false,
                Data = departmentDTO
            };
        }
    }
}
