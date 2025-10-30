using BLL.IServices;
using DAL.Entities;
using DAL.IRepositories;

public class SalaryServices : ISalaryServices
{
    private readonly ISalaryRepository _repository;

    public SalaryServices(ISalaryRepository salaryRepository)
    {
        _repository = salaryRepository;
    }

    public async Task<List<Salary>> GetAllSalariesAsync()
    {
        return await _repository.GetAllSalariesAsync();
    }

    public async Task<int> AddSalaryAsync(SalaryModel model)
    {
        return await _repository.AddSalaryAsync(model);
    }


    public async Task<int> UpdateSalaryAsync(SalaryModel model)
    {
        return await _repository.UpdateSalaryAsync(model);
    }


    public async Task<int> DeleteSalaryAsync(int id)
    {
        return await _repository.DeleteSalaryAsync(id);
    }

    public Task<int> IsExistAsync(int id)
    {
        return _repository.IsExistAsync(id);
    }


    public async Task<bool> SaveAsync(SalaryModel model)
    {
        if(await _repository.IsExistAsync(model.Id) > 0)
        {
            return await _repository.UpdateSalaryAsync(model) > 0;
        }
        else
        {
            return await _repository.AddSalaryAsync(model) > 0;
        }
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _repository.GetAllEmployeesAsync();
    }
}
