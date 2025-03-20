
namespace Project.DataAccess.Repositories
{
    public interface IDepartmentRepository
    {
        int Delete(Department department);
        IEnumerable<Department> GetAll(bool WithTracking = false);
        Department? GetById(int id);
        int Insert(Department department);
        int Update(Department department);
    }
}