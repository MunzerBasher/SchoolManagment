using SchoolDLL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SchoolBLL.IServices
{


    public interface IClassServices
    {
        Task<int> Delete(int id);
        Task<Class> GetClass(int id);
        Task<int> Add(Class classObj);
        Task<int> Save(Class classObj);
        Task<int> Update(Class classObj);
        Task<IList<Class>> GetAllClasses();
        Task<IList<ClassComb>> GetClassesInActiveYear();
        Task<IList<Class>> GetClassesByLevel(int levelId);
        Task<IList<ClassComb>> GetClassesCombByLevel(int levelId);
    }


}