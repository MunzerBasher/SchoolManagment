using BLL.IServices;
using DAL.Entities;

public class SalaryServices : ISalaryServices
{
    private readonly SalaryRepository _repository;

    public SalaryServices()
    {
        _repository = new SalaryRepository();
    }

    public async Task<List<SalaryModel>> GetAllSalariesAsync()
    {
        return await _repository.GetAllSalariesAsync();
    }

    public async Task AddSalaryAsync(SalaryModel model)
    {
        await _repository.AddSalaryAsync(model);
    }


    public async Task UpdateSalaryAsync(SalaryModel model)
    {
        await _repository.UpdateSalaryAsync(model);
    }


    public async Task DeleteSalaryAsync(int id)
    {
        await _repository.DeleteSalaryAsync(id);
    }

}
