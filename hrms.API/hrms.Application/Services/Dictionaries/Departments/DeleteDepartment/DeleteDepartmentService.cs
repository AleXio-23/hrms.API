using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.DeleteDepartment
{
    public class DeleteDepartmentService : IDeleteDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;

        public DeleteDepartmentService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1) throw new ArgumentException("Wrong value for id");
            var getDepartment = await _departmentRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Department on id: {id} not found");
            getDepartment.IsActive = false;
            await _departmentRepository.Update(getDepartment, cancellationToken).ConfigureAwait(false);
            return new ServiceResult<bool>
            {
                Success = true,
                Data = true,
                ErrorOccured = false
            };
        }
    }
}
