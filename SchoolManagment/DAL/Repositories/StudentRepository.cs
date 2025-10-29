using System.Data;
using SchoolDLL.IRepositories;
using Microsoft.Data.SqlClient;
using SchoolDLL.Entities.DLL.Entities;
using DAL.Entities;


namespace SchoolDLL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDBHelper _dBHelper;

        public StudentRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        private StudentTable MapRowToStudent(DataRow row)
        {
            return new StudentTable
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
                BirthDate = Convert.ToDateTime(row["BirthDate"]),
                ParentPhone = row["ParentPhone"].ToString()!,
                GenderName = row["GenderName"].ToString()!,
                ClassName = row["ClassName"].ToString()!,
                SectionName = row["SectionName"].ToString()!
            };
        }

        public async Task<IList<StudentTable>> GetAllStudents(int? classId = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", (object?)classId ?? DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("GetAllStudents", parameters);
            IList<StudentTable> students = new List<StudentTable>();

            foreach (DataRow row in result.Rows)
            {
                students.Add(MapRowToStudent(row));
            }

            return students;
        }

        private Gender MapRowToGender(DataRow row)
        {
            return new Gender
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
            };
        }


        public async Task<IList<Gender>> GetGender()
        {    
            var result = await _dBHelper.ExecuteSelectProcedure("GetGender");
            IList<Gender> students = new List<Gender>();

            foreach (DataRow row in result.Rows)
            {
                students.Add(MapRowToGender(row));
            }

            return students;
        }

        public async Task<StudentTable?> GetStudent(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("GetStudentById", parameters);
            if (result.Rows.Count > 0)
                return MapRowToStudent(result.Rows[0]);

            return null;
        }

        public async Task<(int Status, string Message, int? StudentId)> Add(Student student)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@BirthDate", student.BirthDate),
                new SqlParameter("@ParentPhone", student.ParentPhone),
                new SqlParameter("@GenderId", student.GenderId),
                new SqlParameter("@ClassId", student.ClassId),
                new SqlParameter("@SectionId", student.SectionId)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("AddStudent", parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return (
                    1,
                    "",
                    row["StudentId"] != DBNull.Value ? Convert.ToInt32(row["StudentId"]) : (int?)null
                );
            }

            return (0, "Unknown error", null);
        }

        public async Task<(int Status, string Message, int? StudentId)> Update(Student student)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", student.Id),
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@BirthDate", student.BirthDate),
                new SqlParameter("@ParentPhone", student.ParentPhone),
                new SqlParameter("@GenderId", student.GenderId),
                new SqlParameter("@ClassId", student.ClassId),
                new SqlParameter("@SectionId", student.SectionId)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("UpdateStudent", parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return (
                    Convert.ToInt32(row["Status"]),
                    row["Message"].ToString()!,
                    row["StudentId"] != DBNull.Value ? Convert.ToInt32(row["StudentId"]) : (int?)null
                );
            }

            return (0, "Unknown error", null);
        }

        public async Task<(int Status, string Message)> Delete(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("DeleteStudent", parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return (
                    Convert.ToInt32(row["Status"]),
                    row["Message"].ToString()!
                );
            }

            return (0, "Unknown error");
        }

        public async Task<bool> IsExist(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", Id)
            };

            var result = await _dBHelper.ExecuteScalarProcedure("IsStudentExist", parameters);
            return Convert.ToInt32(result) > 0;
        }
    }

}