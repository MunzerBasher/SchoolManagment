using Microsoft.Data.SqlClient;
using DAL.IRepositories;
using DAL.Entities;
using System.Data;
using SchoolDLL;




namespace DAL.Repositories
{
    public class ReportsRepository : IReportsRepository
    {

        public ReportsRepository(IDBHelper dBHelper) {
             _dBHelper = dBHelper;
        }


        private StudentsReport MapRowToStudentsReport(DataRow row)
        {

            return new StudentsReport
            {

                ClassName = row["ClassName"].ToString()!,
                StudentsCount = Convert.ToInt32(row["ClassId"]),
                YearName = row["YearName"].ToString()!,
                LevelName = row["LevelName"].ToString()!,
                SemesterName = row["SemesterName"].ToString()!,

            };
        }

        private readonly IDBHelper _dBHelper;



        public async Task<IList<StudentsReport>> GetStudentsReport(int YearId, int SemesterId, int LevelId)
        {
            SqlParameter[]  parameters = new SqlParameter[]
            {
                new SqlParameter("@YearId", YearId),
                new SqlParameter("@LevelId", LevelId),
                new SqlParameter("@SemesterId", SemesterId),
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Students_Reports", parameters);

            IList<StudentsReport> classes = new List<StudentsReport>();

            foreach (DataRow row in result.Rows)
            {
                classes.Add(MapRowToStudentsReport(row));
            }

            return classes;
        }
    }



}