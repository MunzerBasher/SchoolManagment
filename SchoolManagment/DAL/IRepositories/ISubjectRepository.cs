
using DLL.Entities;

namespace DLL.IRepositories
{
    public interface ISubjectRepository
    {
        Task<bool> IsExist(int Id);
        Task<SubjectTable> GetSubject(int id);
        Task<IList<SubjectTable>> GetAllSubjects();
        Task<(int Status, string Message)> Delete(int id);
        Task<IList<SubjectsCombox>> GetAllSubjectsCombox();
        Task<(int Status, string Message, int? SubjectId)> Add(Subject subject);
        Task<(int Status, string Message, int? SubjectId)> Update(Subject subject);

    }

}
