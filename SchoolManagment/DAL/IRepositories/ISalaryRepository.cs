
using DAL.Entities;

namespace DAL.IRepositories
{
    public interface ISalaryRepository
    {
        Task<List<SalaryModel>> GetAllSalariesAsync();
        Task UpdateSalaryAsync(SalaryModel model);
        Task AddSalaryAsync(SalaryModel model);
        Task DeleteSalaryAsync(int id);

    }
}
