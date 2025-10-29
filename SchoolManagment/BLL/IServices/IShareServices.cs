using DLL.Entities;
using SchoolDLL.Entities;



namespace BLL.IServices
{
    public interface IShareServices
    {

        Task<int> SaveClass(Class clas);
        Task<IList<Year>> GetYearCombAsync();
        Task<IList<LevleComb>> GetActiveLevelsComb();
        Task<IList<ClassComb>> GetClassesInActiveYear();
        Task<IList<SubjectsCombox>> GetAllSubjectsCombox();
        Task<IList<LevleComb>> GetLevleCombAsync(int semesterId);
        Task<IList<SectionComb>> GetAllCombSections(int classId);
        Task<IList<ClassComb>> GetClassesCombByLevel(int levelId);
        Task<IList<MinSemester>> GetMinSemesterCombAsync(int yearId);

    }


}