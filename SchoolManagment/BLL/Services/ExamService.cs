using DAL.Entities;
using BLL.IServices;
using DAL.IRepositories;


namespace BLL.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _repo;
        public ExamService(IExamRepository repo) => _repo = repo;

        public Task<IList<Exam>> GetAllAsync(int YearId, int SemesterId, int LevelId, int ClassId) => _repo.GetAllAsync(YearId, SemesterId, LevelId,ClassId);
        
        public Task<int> AddAsync(Exam exam)
        {
            if (string.IsNullOrWhiteSpace(exam.Name)) throw new ArgumentException("اسم الامتحان مطلوب");
            return _repo.AddAsync(exam);
        }
        public Task<int> UpdateAsync(Exam exam) => _repo.UpdateAsync(exam);
        public Task<int> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<IList<ExamSubject>> GetExamSubjectsAsync(int examId) => _repo.GetExamSubjectsAsync(examId);
        public Task<int> AddExamSubjectAsync(ExamSubject es)
        {
            if (es.SubjectId <= 0) throw new ArgumentException("اختر مادة");
            return _repo.AddExamSubjectAsync(es);
        }
        public Task<int> DeleteExamSubjectAsync(int examSubjectId) => _repo.DeleteExamSubjectAsync(examSubjectId);

        public async Task<int> DeleteStudentMarksAsync(int id)
        {
            return await _repo.DeleteStudentMarksAsync(id);
        }

        public async Task<IList<StudentMarksTable>> GetStudentMarks()
        {
            return await _repo.GetStudentMarks();
        }

        public async Task<int> AddStudentMarks(StudentMarks studentMarks)
        {
            return await _repo.AddStudentMarks(studentMarks);
        }

        public async Task<int> UpdateStudentMarksAsync(int id, decimal ObtainedMark)
        {
            return await _repo.UpdateStudentMarksAsync(id, ObtainedMark);
        }
    }


}