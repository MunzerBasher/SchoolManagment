using SchoolBLL.IServices;
using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolBLL.Services
{
    public class SemesterServices : ISemesterServices
    {
        private readonly ISemesterRepository _repository;

        public SemesterServices(ISemesterRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(Semester semester)
        {
           
            return await _repository.Add(semester);
        }

        public async Task<int> Update(Semester semester)
        {
            
            return await _repository.Update(semester);
        }

        public async Task<int> Delete(int id)
        {
            
            return await _repository.Delete(id);
        }

        public async Task<Semester> GetSemester(int id)
        {
            return await _repository.GetSemester(id);
        }

        public async Task<IList<Semester>> GetAllSemesters()
        {
            return await _repository.GetAllSemesters();
        }

        public async Task<IList<Semester>> GetSemestersByYear(int yearId)
        {
            return await _repository.GetSemestersByYear(yearId);
        }

        public async Task<int> Save(Semester semester)
        {
            if(await _repository.isExist(semester.Id))
            {
                return await _repository.Update(semester);
            }
            return await _repository.Add(semester);
        }

        public async Task<IList<MinSemester>> GetAllMinSemester(int yearId)
        {
            return await _repository.GetAllMinSemester(yearId);
        }
    }


}