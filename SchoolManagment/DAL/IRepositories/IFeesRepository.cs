using DAL.Entities;

namespace DAL.IRepositories
{   
    public interface IFeesRepository
    {
        Task<int> Delete(int id);
        Task<IList<Fee>> GetAll();
        Task<bool> isExist(int id);
        Task<int> Add(Fee classObj);
        Task<int> Update(Fee classObj);
        Task<IList<FeeType>> GetAllFeeTypes();
        Task<IList<Payment>> GetPaymentsByFee(int feeId);
        Task<int> AddPayment(int feeId, decimal amount, string notes);


        Task<int> DeleteClassFeesAsync(int feesId);
        Task<int> UpdateClassFeesAsync(int Id, decimal Amount);
        Task<int> AddClassFeesAsync(AddClassFees addClassFees);
        Task<IList<ClassFeesTable>> GetClassFeesAsync(int ClassId);    
    }

}