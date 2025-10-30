using SchoolDLL;
using System.Data;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.Data.SqlClient;



namespace DAL.Repositories
{
    
    public class FeesRepository : IFeesRepository
    {

        private readonly IDBHelper _dBHelper;

        public FeesRepository(IDBHelper dBHelper) { 
        
                _dBHelper = dBHelper;
        }

        private Fee MapRowToFees(DataRow row)
        {
            return new Fee
            {
                FeeId = Convert.ToInt32(row["FeeId"]),
                StudentName = row["StudentName"].ToString()!,
                FeeTypeName = row["FeeTypeName "].ToString()!,
                Amount = Decimal.Parse(row["Amount"].ToString()!),
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
            var result = await _dBHelper.ExecuteScalarProcedure("AddFee", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> Update(Fee fee)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FeeId", fee.FeeId),
                new SqlParameter("@StudentId", fee.StudentId),
                new SqlParameter("@FeeTypeId", fee.FeeTypeId),
                new SqlParameter("@PaidAmount", fee.PaidAmount),
                new SqlParameter("@DueDatet", fee.DueDate),
            };
            var result = await _dBHelper.ExecuteScalarProcedure("UpdateFee", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> Delete(int FeeId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FeeId", FeeId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("DeleteFee", parameters);
            return Convert.ToInt32(result);
        }


        public async Task<IList<Fee>> GetAll()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("GetAllFees");

            IList<Fee> Fees = new List<Fee>();

            foreach (DataRow row in result.Rows)
            {
                Fees.Add(MapRowToFees(row));
            }

            return Fees;
        }

       
        public Task<int> AddPayment(int feeId, decimal amount, string notes)
        {
            throw new NotImplementedException();
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


        private ClassFeesTable MapRowToClassFees(DataRow row)
        {
            return new ClassFeesTable
            {
                Amount = decimal.Parse(row["Amount"].ToString()!),
                ClassName = row["ClassName"].ToString()!,
                LevelName = row["LevelName"].ToString()!,
                SemesterName = row["SemesterName"].ToString()!,
                YearName = row["YearName"].ToString()!,
                Id = Convert.ToInt32(row["id"].ToString())
            };
        }

        public async Task<int> DeleteClassFeesAsync(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("DeleteClassFees", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> UpdateClassFeesAsync(int Id, decimal Amount)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id),
                 new SqlParameter("@Amount", Amount),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("UpdateClassFees", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> AddClassFeesAsync(AddClassFees addClassFees)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CreateAt", addClassFees.CreateAt),
                new SqlParameter("@Amount",addClassFees.Amount),
                new SqlParameter("@ClassId", addClassFees.ClassId),
                new SqlParameter("@FeesTypeId", addClassFees.FeesTypeId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("AddClassFees", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<IList<ClassFeesTable>> GetClassFeesAsync(int ClassId)
        {
            var result = await _dBHelper.ExecuteSelectProcedure("GetClassFees");

            IList<ClassFeesTable> Fees = new List<ClassFeesTable>();

            foreach (DataRow row in result.Rows)
            {
                Fees.Add(MapRowToClassFees(row));
            }

            return Fees;
        }
    }
}
