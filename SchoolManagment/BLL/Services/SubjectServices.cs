using BLL.IServices;
using DLL.Entities;
using DLL.IRepositories;

namespace BLL.Services
{


    public class SubjectServices : ISubjectServices
    {

        public SubjectServices(ISubjectRepository subjectRepository) 
        {
            _subjectRepository = subjectRepository;
        }

        
        
        private readonly ISubjectRepository _subjectRepository;

        public async Task<(int Status, string Message, int? SubjectId)> Add(Subject subject)
        {
            return await _subjectRepository.Add(subject);
        }

        public async Task<(int Status, string Message)> Delete(int id)
        {
            return await _subjectRepository.Delete(id);
        }

        public async Task<IList<SubjectTable>> GetAllSubjects()
        {
            return await _subjectRepository.GetAllSubjects();
        }


        public async Task<SubjectTable> GetSubject(int id)
        {
            return await _subjectRepository.GetSubject(id);
        }

        public async Task<bool> IsExist(int Id)
        {
            return await _subjectRepository.IsExist(Id);
        }

        public async Task<(int Status, string Message, int? SubjectId)> Update(Subject subject)
        {
            return await _subjectRepository.Update(subject);
        }

        public async Task<(int Status, string Message, int? SubjectId)> save(Subject subject)
        {
            if(await _subjectRepository.IsExist(subject.Id))
            {
                return await Update(subject);
            } 
            return await Add(subject);
        }

        public async Task<IList<SubjectsCombox>> GetAllSubjectsCombox()
        {
            return await _subjectRepository.GetAllSubjectsCombox();
        }
    }






}