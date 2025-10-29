using System.Data;
using DLL.Entities;
using Microsoft.Data.SqlClient;
using SchoolDLL.IRepositories;

namespace SchoolDLL.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IDBHelper _dBHelper;

        public TeacherRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        private Teacher MapRowToTeacher(DataRow row)
        {
            return new Teacher
            {
                TeacherId = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
                SubjectName = row["SubjectName"].ToString()!,
                Phone = row["Phone"].ToString(),
                Status = Convert.ToBoolean(row["Status"])
            };
        }

        public async Task<IList<Teacher>> GetAllTeachers()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("GetAllTeachers");

            IList<Teacher> teachers = new List<Teacher>();
            foreach (DataRow row in result.Rows)
            {
                teachers.Add(MapRowToTeacher(row));
            }
            return teachers;
        }


        public async Task<Teacher> GetTeacher(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TeacherId", id)
            };
            var result = await _dBHelper.ExecuteSelectProcedure("GetAllTeachers", parameters);
            if (result.Rows.Count > 0)
                return MapRowToTeacher(result.Rows[0]);
            return null!;
        }

        public async Task<int> Add(Teacher teacher, int SubjectId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", teacher.Name),
                new SqlParameter("@SubjectId",SubjectId),
                new SqlParameter("@Phone", teacher.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Status", teacher.Status)
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Add_Teacher", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> Update(Teacher teacher, int SubjectId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TeacherId", teacher.TeacherId),
                new SqlParameter("@Name", teacher.Name),
                new SqlParameter("@SubjectId",SubjectId),
                new SqlParameter("@Phone", teacher.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Status", teacher.Status)
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Update_Teacher", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> Delete(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TeacherId", id)
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Delete_Teacher", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<bool> IsExist(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TeacherId", id)
            };

            var result = await _dBHelper.ExecuteScalarProcedure("IsTeacherExist", parameters);
            return Convert.ToInt32(result) > 1;
        }

        public async Task<IList<Teacher>> SearchTeachers(string TeacherName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TeacherName", TeacherName)
            };
            var result = await _dBHelper.ExecuteSelectProcedure("SearchTeachers", parameters);

            IList<Teacher> teachers = new List<Teacher>();
            foreach (DataRow row in result.Rows)
            {
                teachers.Add(MapRowToTeacher(row));
            }
            return teachers;
        }
    }
}
