using SchoolDLL.Entities;
using SchoolBLL.IServices;
using SchoolDLL.IRepositories;




namespace SchoolBLL.Services
{
    public class LevelServices : ILevelServices
    {
        private readonly ILevelRepository _repository;

        public LevelServices(ILevelRepository repository)
        {
            _repository = repository;
        }

        
        public async Task<int> Add(Level level)
        {   
            return await _repository.Add(level);
        }



        public async Task<int> Update(Level level)
        {   
            return await _repository.Update(level);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.Delete(id);
        }


        public async Task<LevleTable> GetLevel(int id)
        {
            return await _repository.GetLevel(id);
        }

        
        public async Task<IList<LevleTable>> GetAllLevels(int semesterId)
        {
            return await _repository.GetAllLevels(semesterId);
        }


        public async Task<IList<LevleTable>> GetLevelsBySemester(int semesterId)
        {
            return await _repository.GetLevelsBySemester(semesterId);
        }


        public async Task<IList<LevleTable>> GetLevelsByYear(int yearId)
        {
            return await _repository.GetLevelsByYear(yearId);
        }

        public async Task<bool> isExist(int Id)
        {
            return await _repository.isExist(Id);
        }

        public async Task<bool> Save(Level level)
        {
            if(await isExist(level.Id))
            {
                return await Update(level) > 0;
            }
            return await Add(level) > 0;
        }

        public async Task<IList<LevleComb>> GetLevelsCombBySemester(int semesterId)
        {
            return await _repository.GetLevelsCombBySemester(semesterId);
        }


        public async Task<IList<LevleComb>> GetActiveLevelsComb()
        {
            return await _repository.GetActiveLevelsComb();
        }


    }

}