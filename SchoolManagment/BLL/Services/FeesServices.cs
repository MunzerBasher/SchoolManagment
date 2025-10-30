using DAL.Entities;
using BLL.IServices;
using DAL.IRepositories;



namespace SchoolBLL.Services
{
    public class FeesServices : IFeesServices
    {
        private readonly IFeesRepository _repository;

        public FeesServices(IFeesRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(Fee fee)
        {
            return await _repository.Add(fee);
        }

        public async Task<int> Update(Fee fee)
        {
            return await _repository.Update(fee);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.Delete(id);
        }

      

        public async Task<IList<Fee>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<int> Save(Fee fee)
        {
           
          
            if (await _repository.isExist(fee.FeeId))
                return await Add(fee);
            else
                return await Update(fee);
        }

 
        public async Task<int> AddPayment(int feeId, decimal amount, string notes)
        {
            return await _repository.AddPayment(feeId, amount, notes);
        }

        public async Task<IList<Payment>> GetPaymentsByFee(int feeId)
        {
            return await _repository.GetPaymentsByFee(feeId);
        }

     
        public async Task<IList<FeeType>> GetAllFeeTypes()
        {
            return await _repository.GetAllFeeTypes();
        }

        public Task<IList<Fee>> Search(string query)
        {
            throw new NotImplementedException();
        }
    }
}
