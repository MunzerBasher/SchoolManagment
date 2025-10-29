using DLL.Entities;




namespace SchoolDLL.IRepositories
{

    public interface ITeacherRepository
    {
        Task<IList<Teacher>> GetAllTeachers();
        Task<IList<Teacher>> SearchTeachers(string TeacherName);
        Task<Teacher> GetTeacher(int id);
        Task<int> Add(Teacher teacher, int SubjectId);
        Task<int> Update(Teacher teacher, int SubjectId);
        Task<int> Delete(int id);
        Task<bool> IsExist(int id);
    }


}
