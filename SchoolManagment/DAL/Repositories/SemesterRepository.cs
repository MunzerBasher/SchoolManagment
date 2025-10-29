using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace SchoolDLL.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly IDBHelper _dBHelper;

        public SemesterRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

       
        public async Task<int> Add(Semester semester)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SemesterName", semester.Name),
                new SqlParameter("@YearId", semester.YearId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Semester_Insert", parameters);
            return Convert.ToInt32(result);
        }

        

        public async Task<int> Update(Semester semester)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SemesterId", semester.Id),
                new SqlParameter("@SemesterName", semester.Name),
                new SqlParameter("@YearId", semester.YearId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Semester_Update", parameters);
            return Convert.ToInt32(result);
        }

       
        public async Task<int> Delete(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SemesterId", id),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Semester_Delete", parameters);
            return Convert.ToInt32(result);
        }

        
        private Semester MapRowToSemester(DataRow row)
        {
            return new Semester
            {
                Id = Convert.ToInt32(row["SemesterId"]),
                Name = row["SemesterName"].ToString()!,
                YearName = row["YearName"].ToString()!
            };
        }

        private MinSemester Map(DataRow row)
        {
            return new MinSemester
            {
                Id = Convert.ToInt32(row["SemesterId"]),
                Name = row["SemesterName"].ToString()!,
            };
        }

        public async Task<IList<Semester>> GetAllSemesters()
        {
           
            var result = await _dBHelper.ExecuteSelectProcedure("Semester_Select");

            IList<Semester> semesters = new List<Semester>();

            foreach (DataRow row in result.Rows)
            {
                semesters.Add(MapRowToSemester(row));
            }

            return semesters;
        }


        public async Task<IList<MinSemester>> GetAllMinSemester(int yearId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {

                new SqlParameter("@yearId", yearId),
            };
            var result = await _dBHelper.ExecuteSelectProcedure("SelectAll_CombSemester",parameters);

            IList<MinSemester> semesters = new List<MinSemester>();

            foreach (DataRow row in result.Rows)
            {
                semesters.Add(Map(row));
            }

            return semesters;
        }


        public async Task<Semester> GetSemester(int id)
           {
            SqlParameter[] parameters = new SqlParameter[]
            {
               
                new SqlParameter("@SemesterId", id),
            };

            
            var result = await _dBHelper.ExecuteSelectProcedure("Semester_SelectById", parameters);

            if (result.Rows.Count > 0)
            {
                return MapRowToSemester(result.Rows[0]);
            }
            return null!;
        }

        
        public async Task<IList<Semester>> GetSemestersByYear(int yearId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                
                new SqlParameter("@YearId", yearId)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Semester_Select", parameters);

            IList<Semester> semesters = new List<Semester>();

            foreach (DataRow row in result.Rows)
            {
                semesters.Add(MapRowToSemester(row));
            }

            return semesters;
        }


        public async Task<bool> isExist(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@Id", Id),
             };

            var result = await _dBHelper.ExecuteScalarProcedure("Semester_Exist", parameters);
            return Convert.ToInt32(result) > 1;
        }
    }
}
