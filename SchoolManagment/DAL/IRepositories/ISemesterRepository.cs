

using SchoolDLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolDLL.IRepositories
{
    public interface ISemesterRepository
    {
        Task<int> Delete(int id);
        Task<bool> isExist(int Id);
        Task<int> Add(Semester semester);
        Task<Semester> GetSemester(int id);
        Task<int> Update(Semester semester);
        Task<IList<Semester>> GetAllSemesters();
        // قد نحتاج دالة لجلب الفصول حسب العام الدراسي
        Task<IList<Semester>> GetSemestersByYear(int yearId);
        Task<IList<MinSemester>> GetAllMinSemester(int yearId);
    }


}
