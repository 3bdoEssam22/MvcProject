using Project.Bussiness.DataTransferObjects.EmployeeDtos;

namespace Project.Bussiness.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking);
        EmployeeDeatilsDto? GetEmployeeById(int id);
        int AddEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);

    }
}
