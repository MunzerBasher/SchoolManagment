using DAL.Entities;




namespace BLL.IServices
{
    public interface ISalaryServices
    {
        Task<List<SalaryModel>> GetAllSalariesAsync();
        Task UpdateSalaryAsync(SalaryModel model);
        Task AddSalaryAsync(SalaryModel model);
        Task DeleteSalaryAsync(int id);

    }
}
