using SchoolDLL.Entities;





namespace SchoolDLL.IRepositories
{
    public interface ILevelRepository
    {
        Task<int> Delete(int id);
        Task<int> Add(Level level);
        Task<bool> isExist(int Id);
        Task<int> Update(Level level);
        Task<LevleTable> GetLevel(int id);
        Task<IList<LevleComb>> GetActiveLevelsComb();
        Task<IList<LevleTable>> GetLevelsByYear(int yearId);
        Task<IList<LevleTable>> GetAllLevels(int SemesterId);
        Task<IList<LevleTable>> GetLevelsBySemester(int semesterId);
        Task<IList<LevleComb>> GetLevelsCombBySemester(int semesterId);
    }
}