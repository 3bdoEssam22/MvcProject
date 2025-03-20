
namespace Project.DataAccess.Repositories
{
    public interface IDepartmentRepository
    {
        int Delete(Department department);
        IEnumerable<Department> GetAll(bool WithTracking = false);
        Department? GetById(int id);
        int Add(Department department);
        int Update(Department department);
    }
}