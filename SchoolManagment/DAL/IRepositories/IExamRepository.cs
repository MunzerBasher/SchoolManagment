
using DAL.Entities;

namespace DAL.IRepositories
{
    public interface IExamRepository
    {
        Task<IList<Exam>> GetAllAsync(int YearId, int SemesterId, int LevelId, int ClassId);
        Task<int> AddAsync(Exam exam);
        Task<int> UpdateAsync(Exam exam);
        Task<int> DeleteAsync(int id);

      
        Task<IList<ExamSubject>> GetExamSubjectsAsync(int examId);
        Task<int> AddExamSubjectAsync(ExamSubject es);
        Task<int> DeleteExamSubjectAsync(int examSubjectId);


        Task<int> DeleteStudentMarksAsync(int id);
        Task<IList<StudentMarksTable>> GetStudentMarks();
        Task<int> AddStudentMarks(StudentMarks studentMarks);
        Task<int> UpdateStudentMarksAsync(int id, decimal ObtainedMark);

    }

}
