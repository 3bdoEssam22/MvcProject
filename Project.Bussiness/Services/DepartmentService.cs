using Project.Bussiness.DataTransferObjects;
using Project.Bussiness.Factories;
using Project.DataAccess.Models;
using Project.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Services
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
        public int AddDepartment(CreatedDepartmentDto departmentDto)
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
