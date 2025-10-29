using System;
using SchoolDLL.Entities;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SchoolDLL.IRepositories;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace SchoolDLL.Repositories
{
    public class YearRepository : IYearRepository
    {
        public YearRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }
        private readonly IDBHelper _dBHelper;


        public async Task<int> Add(Year year)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@YearName", year.Name),
                new SqlParameter("@IsActive", year.isActive),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("AcademicYear_Insert", parameters);
            return Convert.ToInt32(result);
        }



        public async Task<int> Delete(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@YearID", Id),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("AcademicYear_Delete", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<Year> GetYear(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@YearValue", Id),
            };

            var result = await _dBHelper.ExecuteSelectProcedure("", parameters);
            return new Year {Id = Convert.ToInt32(result.Rows[0][""]),Name = result.Rows[0][""].ToString()!, isActive = Convert.ToInt32(result.Rows[0][""]) == 1 };
        }

        public async Task<int> Update(Year year)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@YearID", year.Id),
                new SqlParameter("@YearName", year.Name),
                new SqlParameter("@IsActive", year.isActive),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("AcademicYear_Update", parameters);
            return Convert.ToInt32(result);
        }

        

        public async Task<IList<Year>> GetAllYear()
        {
            
            var result = await _dBHelper.ExecuteSelectProcedure("AcademicYear_SelectAll");

            IList<Year> years = new List<Year>();

            for (int i = 0; i < result.Rows.Count; i++)
            {
                Console.WriteLine(result.Rows[i]["YearID"].ToString());
                years.Add(
                    new Year
                    {
                        Id = Convert.ToInt32(result.Rows[i]["YearID"]),
                        Name = result.Rows[i]["YearName"].ToString()!,
                        isActive = Convert.ToInt32(result.Rows[i]["IsActive"]) == 1,
                    }
                );
            }

            return years;

        }

        public async Task<bool> isExist(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@YearId", Id),
             };

            var result = await _dBHelper.ExecuteScalarProcedure("AcademicYear_Exist", parameters);
            return Convert.ToInt32(result) > 1;
        }


    
    
    }



}