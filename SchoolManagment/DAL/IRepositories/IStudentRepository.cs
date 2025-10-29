using DAL.Entities;
using SchoolDLL.Entities.DLL.Entities;



namespace SchoolDLL.IRepositories
{
    public interface IStudentRepository
    {
        Task<bool> IsExist(int Id);
        Task<IList<Gender>> GetGender();
        Task<StudentTable?> GetStudent(int id);
        Task<(int Status, string Message)> Delete(int id);
        Task<IList<StudentTable>> GetAllStudents(int? classId = null);
        Task<(int Status, string Message, int? StudentId)> Add(Student student);
        Task<(int Status, string Message, int? StudentId)> Update(Student student);


    }


}