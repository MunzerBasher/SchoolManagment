using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolDLL.Entities;



namespace SchoolBLL.IServices
{


    public interface ISemesterServices
    {
        Task<int> Delete(int id);
        Task<int> Add(Semester semester);
        Task<int> Save(Semester semester);
        Task<Semester> GetSemester(int id);
        Task<int> Update(Semester semester);
        Task<IList<Semester>> GetAllSemesters();
        Task<IList<MinSemester>> GetAllMinSemester(int yearId);
        Task<IList<Semester>> GetSemestersByYear(int yearId);
    }



}
