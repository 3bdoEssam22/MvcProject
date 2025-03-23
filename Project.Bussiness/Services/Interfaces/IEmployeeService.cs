using Project.Bussiness.DataTransferObjects.EmployeeDtos;

namespace Project.Bussiness.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDeatilsDto? GetEmployeeById(int id);
        IEnumerable<EmployeeDto> GetAllEmployees();
        int AddEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);

    }
}
