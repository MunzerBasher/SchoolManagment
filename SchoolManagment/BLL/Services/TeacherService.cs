using SchoolDLL.IRepositories;
using BLL.IServices;
using DLL.Entities;



namespace BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        
        public async Task<IList<Teacher>> GetAllTeachers()
        {
            return await _teacherRepository.GetAllTeachers();
        }

        
        public async Task<Teacher> GetTeacher(int id)
        {
            return await _teacherRepository.GetTeacher(id);
        }

        
        public async Task<int> AddTeacher(Teacher teacher, int SubjectId)
        {
              
            return await _teacherRepository.Add(teacher ,SubjectId);
        }

        
        public async Task<int> UpdateTeacher(Teacher teacher, int SubjectId)
        {
            
            return await _teacherRepository.Update(teacher, SubjectId);
        }

        
        public async Task<int> DeleteTeacher(int id)
        {
            
            return await _teacherRepository.Delete(id);
        }

        
        public async Task<bool> IsTeacherExist(int id)
        {
            return await _teacherRepository.IsExist(id);
        }


        public async Task<IList<Teacher>> SearchTeachers(string TeacherName)
        {
            return await _teacherRepository.SearchTeachers(TeacherName);
        }

        public async Task<int> Save(Teacher teacher, int SubjectId)
        {
            if(await IsTeacherExist(teacher.TeacherId))
            {
                return await UpdateTeacher(teacher, SubjectId);
            }
            return await AddTeacher(teacher,SubjectId);
        }

    }

}