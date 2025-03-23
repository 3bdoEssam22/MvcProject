using Project.Bussiness.DataTransferObjects.DepartmentDtos;
using Project.Bussiness.Factories;
using Project.Bussiness.Services.Interfaces;
using Project.DataAccess.Repositories.Interfaces;

namespace Project.Bussiness.Services.Classes
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = departmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());
        }

        //Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = departmentRepository.GetById(id);
            //AutoMapper can be used for mapping

            //Manual mapping
            //return department is null ? null : new DepartmentDetailsDto
            //{
            //    Id = department.Id,
            //    Code = department.Code,
            //    Description = department.Description,
            //    Name = department.Name,
            //    CreatedBy = department.CreatedBy,
            //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
            //    LastModifiedBy = department.LastModifiedBy,
            //    LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn) ,
            //    IsDeleted = department.IsDeleted
            //};

            //Constructor mapping
            //return department is null ? null : new DepartmentDetailsDto(department);

            //Extension method mapping
            return department == null ? null : department.ToDepartmentDetailsDto();
        }

        //Create new Department
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return departmentRepository.Add(department);
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return departmentRepository.Update(departmentDto.ToEntity());
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = departmentRepository.GetById(id);
            if (department == null)
            {
                return false;
            }
            else
            {
                int result = departmentRepository.Delete(department);
                return result > 0 ? true : false;
            }
        }

    }
}
