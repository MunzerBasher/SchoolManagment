



using DLL.Entities;

namespace BLL.IServices
{
   
    public interface ISubjectServices
    {

        Task<IList<SubjectTable>> GetAllSubjects();
        Task<SubjectTable> GetSubject(int id);
        Task<IList<SubjectsCombox>> GetAllSubjectsCombox();
        Task<(int Status, string Message, int? SubjectId)> Add(Subject subject);
        Task<(int Status, string Message, int? SubjectId)> Update(Subject subject);
        Task<(int Status, string Message)> Delete(int id);
        Task<bool> IsExist(int Id);
        Task<(int Status, string Message, int? SubjectId)> save(Subject subject);
    }



}
