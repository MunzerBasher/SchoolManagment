using DAL.Entities;
using SchoolDLL.Entities.DLL.Entities;



namespace SchoolBLL.IServices
{
    public interface IStudentServices
    {
        Task<bool> IsExist(int id);
        Task<IList<Gender>> GetGender();
        Task<StudentTable?> GetStudent(int id);
        Task<(int Status, string Message)> Delete(int id);
        Task<IList<StudentTable>> GetAllStudents(int? classId = null);
        Task<(int Status, string Message, int? StudentId)> Add(Student student);
        Task<(int Status, string Message, int? StudentId)> Save(Student student);
        Task<(int Status, string Message, int? StudentId)> Update(Student student);

    }
}