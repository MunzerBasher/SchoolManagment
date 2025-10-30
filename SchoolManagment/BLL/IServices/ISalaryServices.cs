using DAL.Entities;




namespace BLL.IServices
{
    public interface ISalaryServices
    {
        Task<List<Salary>> GetAllSalariesAsync();
        Task<int> UpdateSalaryAsync(SalaryModel model);
        Task<int> AddSalaryAsync(SalaryModel model);
        Task<int> DeleteSalaryAsync(int id);
        Task<int> IsExistAsync(int id);
        Task<bool> SaveAsync(SalaryModel model);
        Task<List<Employee>> GetAllEmployeesAsync();


    }
}
