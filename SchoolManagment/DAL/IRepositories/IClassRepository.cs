using SchoolDLL.Entities;





namespace SchoolDLL.IRepositories
{


    public interface IClassRepository
    {
        Task<int> Delete(int id);
        Task<bool> isExist(int id);
        Task<Class> GetClass(int id);
        Task<int> Add(Class classObj);
        Task<int> Update(Class classObj);
        Task<IList<Class>> GetAllClasses();
        Task<IList<ClassComb>> GetClassesInActiveYear();
        Task<IList<Class>> GetClassesByLevel(int levelId);
        Task<IList<ClassComb>> GetClassesCombByLevel(int levelId);
    }


}