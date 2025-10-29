using SchoolBLL.IServices;
using SchoolDLL.IRepositories;
using SchoolDLL.Entities.DLL.Entities;
using DAL.Entities;


namespace BLL.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IList<StudentTable>> GetAllStudents(int? classId = null)
        {
            return await _studentRepository.GetAllStudents(classId);
        }

        public async Task<StudentTable?> GetStudent(int id)
        {
            return await _studentRepository.GetStudent(id);
        }

        public async Task<(int Status, string Message, int? StudentId)> Add(Student student)
        {
            return await _studentRepository.Add(student);
        }

        public async Task<(int Status, string Message, int? StudentId)> Update(Student student)
        {
            return await _studentRepository.Update(student);
        }

        public async Task<(int Status, string Message)> Delete(int id)
        {
            return await _studentRepository.Delete(id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _studentRepository.IsExist(id);
        }

        public async Task<(int Status, string Message, int? StudentId)> Save(Student student)
        {
            if (await _studentRepository.IsExist(student.Id))
                return await Update(student);

            return await Add(student);
        }

        public async Task<IList<Gender>> GetGender()
        {
            return await _studentRepository.GetGender();
        }

    }
}
