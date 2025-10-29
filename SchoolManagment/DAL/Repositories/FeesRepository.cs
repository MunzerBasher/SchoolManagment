using SchoolDLL;
using System.Data;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.Data.SqlClient;



namespace DAL.Repositories
{
    
    public class FeesRepository : IFeesRepository
    {

        public FeesRepository(IDBHelper dBHelper) { 
        
                _dBHelper = dBHelper;
        }



        private readonly IDBHelper _dBHelper;



        private Fee MapRowToFees(DataRow row)
        {

            return new Fee
            {
                FeeId = Convert.ToInt32(row["ClassId"]),
                StudentName = row["StudentName"].ToString()!,
                FeeTypeName = row["FeeTypeName "].ToString()!,
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()!),
                PaidAmount = Decimal.Parse(row["PaidAmount"].ToString()!),
                RemainingAmount = Decimal.Parse(row["RemainingAmount"].ToString()!),
                PaymentStatus = row["PaymentStatus"].ToString()!,
            };
        }

        public async Task<int> Add(Fee fee)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", fee.StudentId),
                new SqlParameter("@FeeTypeId", fee.FeeTypeId),
                new SqlParameter("@PaidAmount", fee.PaidAmount),
                new SqlParameter("@CreatedAt", fee.CreatedAt),
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Class_Insert", parameters);
            return Convert.ToInt32(result);
        }
        public async Task<int> Delete(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", id),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Class_Delete", parameters);
            return Convert.ToInt32(result);
        }

        public Task<int> AddPayment(int feeId, decimal amount, string notes)
        {
            throw new NotImplementedException();
        }


        public Task<Fee> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Fee>> GetAll()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("Class_Select");

            IList<Fee> Fees = new List<Fee>();

            foreach (DataRow row in result.Rows)
            {
                Fees.Add(MapRowToFees(row));
            }

            return Fees;
        }

        public Task<IList<FeeType>> GetAllFeeTypes()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Payment>> GetPaymentsByFee(int feeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isExist(int id)
        {
            throw new NotImplementedException();
        }       

        public Task<int> Update(Fee classObj)
        {
            throw new NotImplementedException();
        }
    }
}
