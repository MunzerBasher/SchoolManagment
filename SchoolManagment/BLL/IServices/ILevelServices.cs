using SchoolDLL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SchoolBLL.IServices
{
    public interface ILevelServices
    {
        Task<int> Add(Level level);
        Task<bool> Save(Level level);
        Task<bool> isExist(int Id);
        Task<int> Update(Level level);
        Task<int> Delete(int id);
        Task<LevleTable> GetLevel(int id);
        Task<IList<LevleComb>> GetActiveLevelsComb();
        Task<IList<LevleComb>> GetLevelsCombBySemester(int semesterId);
        Task<IList<LevleTable>> GetAllLevels(int semesterId);
        Task<IList<LevleTable>> GetLevelsBySemester(int semesterId);
        Task<IList<LevleTable>> GetLevelsByYear(int yearId);
    }


}