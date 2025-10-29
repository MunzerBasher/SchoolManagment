
using DLL.Entities;


namespace BLL.IServices
{
    public interface ITeacherService
    {

        Task<int> DeleteTeacher(int id);
        Task<Teacher> GetTeacher(int id);
        Task<bool> IsTeacherExist(int id);
        Task<IList<Teacher>> GetAllTeachers();
        Task<int> Save(Teacher teacher, int SubjectId);
        Task<int> AddTeacher(Teacher teacher, int SubjectId);
        Task<IList<Teacher>> SearchTeachers(string TeacherName);
        Task<int> UpdateTeacher(Teacher teacher, int SubjectId);

    }

}
