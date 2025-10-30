using DAL.Entities;

namespace BLL.IServices
{
    public interface IFeesServices
    {


        Task<IList<Fee>> GetAll();
        Task<int> Add(Fee fee);
        Task<int> Update(Fee fee);
        Task<int> Delete(int id);
        Task<int> AddPayment(int feeId, decimal amount, string notes);
        Task<IList<Payment>> GetPaymentsByFee(int feeId);
        Task<IList<FeeType>> GetAllFeeTypes();
        Task<IList<Fee>> Search(string query);

    }
}
