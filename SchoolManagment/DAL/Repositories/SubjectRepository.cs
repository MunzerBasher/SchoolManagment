using System;
using System.Data;
using DLL.Entities;
using DLL.IRepositories;
using Microsoft.Data.SqlClient;



namespace SchoolDLL.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IDBHelper _dBHelper;

        public SubjectRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }


        private SubjectTable MapRowToSubject(DataRow row)
        {
            return new SubjectTable
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
                Code = row["Code"].ToString()!,
                Description = row["Description"].ToString()!,
                CreditHours = Convert.ToInt32(row["CreditHours"]),
                ClassName = row["ClassName"].ToString()!
            };
        }

        private SubjectsCombox MapRowToSubjectsCombox(DataRow row)
        {
            return new SubjectsCombox
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
               
            };
        }


        public async Task<IList<SubjectsCombox>> GetAllSubjectsCombox()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("SubjectsCombox");

            IList<SubjectsCombox> subjects = new List<SubjectsCombox>();
            foreach (DataRow row in result.Rows)
            {
                subjects.Add(MapRowToSubjectsCombox(row));
            }

            return subjects;
        }

        public async Task<IList<SubjectTable>> GetAllSubjects()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("GetAllSubjects");

            IList<SubjectTable> subjects = new List<SubjectTable>();
            foreach (DataRow row in result.Rows)
            {
                subjects.Add(MapRowToSubject(row));
            }

            return subjects;
        }



        public async Task<SubjectTable> GetSubject(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("GetSubjectById", parameters);

            if (result.Rows.Count > 0)
                return MapRowToSubject(result.Rows[0]);

            return null!;
        }


        public async Task<(int Status, string Message, int? SubjectId)> Add(Subject subject)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", subject.Name),
                new SqlParameter("@Code", subject.Code),
                new SqlParameter("@Description", subject.Description),
                new SqlParameter("@CreditHours", subject.CreditHours),
                new SqlParameter("@ClassId", subject.ClassId)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("AddSubject", parameters);
            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return (
                    Convert.ToInt32(row["Status"]),
                    row["Message"].ToString()!,
                    row["SubjectId"] != DBNull.Value ? Convert.ToInt32(row["SubjectId"]) : (int?)null
                );
            }

            return (0, "Unknown error", null);
        }


        public async Task<(int Status, string Message, int? SubjectId)> Update(Subject subject)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", subject.Id),
                new SqlParameter("@Name", subject.Name),
                new SqlParameter("@Code", subject.Code),
                new SqlParameter("@ClassId", subject.ClassId),
                new SqlParameter("@Description", subject.Description),
                new SqlParameter("@CreditHours", subject.CreditHours),

            };

            var result = await _dBHelper.ExecuteSelectProcedure("UpdateSubject", parameters);
            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return (
                    Convert.ToInt32(row["Status"]),
                    row["Message"].ToString()!,
                    row["SubjectId"] != DBNull.Value ? Convert.ToInt32(row["SubjectId"]) : (int?)null
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

            var result = await _dBHelper.ExecuteSelectProcedure("DeleteSubject", parameters);
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
                new SqlParameter("@Id", Id)
            };

            var result = await _dBHelper.ExecuteScalarProcedure("IsSubjectExist", parameters);
            return Convert.ToInt32(result) > 0;
        }
    }
}
