using DAL.Entities;
using System.Data;
using DAL.IRepositories;
using global::SchoolDLL;
using Microsoft.Data.SqlClient;
using System.Collections;



namespace DLL.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly IDBHelper _dbHelper;

        public ExamRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        
        private Exam MapRowToExam(DataRow row)
        {
            return new Exam
            {
                ExamId = Convert.ToInt32(row["ExamId"]),
                Name = row["Name"].ToString()!,
                AcademicYearName = row.Table.Columns.Contains("AcademicYearName") ? row["AcademicYearName"].ToString()! : "",
                SemesterName = row.Table.Columns.Contains("SemesterName") ? row["SemesterName"].ToString()! : "",
                ExamDate = Convert.ToDateTime(row["ExamDate"]),
            };
        }


        public async Task<IList<Exam>> GetAllAsync(int YearId, int SemesterId, int LevelId, int ClassId)
        {
            var result = await _dbHelper.ExecuteSelectProcedure("GetAllExams");
            IList<Exam> exams = new List<Exam>();

            foreach (DataRow row in result.Rows)
            {
                exams.Add(MapRowToExam(row));
            }

            return exams;
        }

       
        
        public async Task<int> AddAsync(Exam exam)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", exam.Name),
                new SqlParameter("@AcademicYearId", exam.AcademicYearId),
                new SqlParameter("@SemesterId", exam.SemesterId),
                new SqlParameter("@ExamDate", exam.ExamDate),
                new SqlParameter("@Notes", exam.Notes ?? (object)DBNull.Value)
            };

            var result = await _dbHelper.ExecuteScalarProcedure("AddExam", parameters);
            return Convert.ToInt32(result);
        }


        public async Task<int> UpdateAsync(Exam exam)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", exam.ExamId),
                new SqlParameter("@Name", exam.Name),
                new SqlParameter("@AcademicYearId", exam.AcademicYearId),
                new SqlParameter("@SemesterId", exam.SemesterId),
                new SqlParameter("@ExamDate", exam.ExamDate),
                new SqlParameter("@Notes", exam.Notes ?? (object)DBNull.Value)
            };

            var result = await _dbHelper.ExecuteScalarProcedure("UpdateExam", parameters);
            return Convert.ToInt32(result);
        }

        
        public async Task<int> DeleteAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ExamId", id)
            };

            var result = await _dbHelper.ExecuteScalarProcedure("DeleteExam", parameters);
            return Convert.ToInt32(result);
        }

       


        private ExamSubject MapRowToExamSubject(DataRow row)
        {
            return new ExamSubject
            {
                ExamDate = Convert.ToDateTime(row["Subject"].ToString()!),
                ExamSubjectId = Convert.ToInt32(row["ExamSubjectId"]),
                MaxMark = Convert.ToDecimal(row["MaxMark"]),
                MinMark = Convert.ToDecimal(row["MinMark"]),
                SubjectName = row["Subject"].ToString()!,
                Exam = row["Exam"].ToString()!,
            };
        }

        
        public async Task<IList<ExamSubject>> GetExamSubjectsAsync(int examId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ExamId", examId)
            };

            var result = await _dbHelper.ExecuteSelectProcedure("GetAllExamSubjects", parameters);

            IList<ExamSubject> subjects = new List<ExamSubject>();

            foreach (DataRow row in result.Rows)
            {
                subjects.Add(MapRowToExamSubject(row));
            }

            return subjects;
        }

        
        public async Task<int> AddExamSubjectAsync(ExamSubject subject)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ExamId", subject.ExamId),
                new SqlParameter("@SubjectId", subject.SubjectId),
                new SqlParameter("@MaxMark", subject.MaxMark),
                new SqlParameter("@MinMark", subject.MinMark)
            };

            var result = await _dbHelper.ExecuteScalarProcedure("AddExamSubjects", parameters);
            return Convert.ToInt32(result);
        }

        
        public async Task<int> DeleteExamSubjectAsync(int examSubjectId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", examSubjectId)
            };

            var result = await _dbHelper.ExecuteScalarProcedure("DeleteExamSubjects", parameters);
            return Convert.ToInt32(result);
        }


        private StudentMarksTable MapRowToStudentMarksTable(DataRow row)
        {
            return new StudentMarksTable
            {
                ExamName = row["ExamName"].ToString()!,
                Id = Convert.ToInt32(row["Id"]),
                MaxMark = Convert.ToDecimal(row["MaxMark"]),
                MinMark = Convert.ToDecimal(row["MinMark"]),
                ObtainedMark  = Convert.ToDecimal(row["ObtainedMark"]),
                StudentName = row["StudentName"].ToString()!,
                Subject = row["SubjectName"].ToString()!,
                Year = row["Year"].ToString()!,
            };
        }

        // ////

        public async Task<IList<StudentMarksTable>> GetStudentMarks()
        {
            var result = await _dbHelper.ExecuteSelectProcedure("GetStudentMarks");
            IList<StudentMarksTable> StudentMarksTable = new List<StudentMarksTable>();

            foreach (DataRow row in result.Rows)
            {
                StudentMarksTable.Add(MapRowToStudentMarksTable(row));
            }

            return StudentMarksTable;
        }

        public async Task<int> AddStudentMarks(StudentMarks studentMarks)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", studentMarks.StudentId),
                new SqlParameter("@ExamSubjectId", studentMarks.SubjectId),
                new SqlParameter("@CreatedAt", studentMarks.CreatedAt),
                new SqlParameter("@ObtainedMark",studentMarks.ObtainedMark),
            };

            var result = await _dbHelper.ExecuteScalarProcedure("AddExam", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> UpdateStudentMarksAsync(int id , decimal ObtainedMark)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Id", id),
                new SqlParameter("@ObtainedMark", ObtainedMark)
                
            };

            var result = await _dbHelper.ExecuteScalarProcedure("UpdateStudentMarks", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> DeleteStudentMarksAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Id", id),
            };

            var result = await _dbHelper.ExecuteScalarProcedure("DeleteStudentMarks", parameters);
            return Convert.ToInt32(result);
        }



    
    }


}