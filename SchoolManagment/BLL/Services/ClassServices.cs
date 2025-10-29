using SchoolDLL.Entities;
using SchoolBLL.IServices;
using SchoolDLL.IRepositories;


namespace SchoolBLL.Services
{
    public class ClassServices : IClassServices
    {
        
        private readonly IClassRepository _repository;

        public ClassServices(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(Class classObj)
        {
            return await _repository.Add(classObj);
        }

        public async Task<int> Update(Class classObj)
        {
            return await _repository.Update(classObj);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Class> GetClass(int id)
        {
            return await _repository.GetClass(id);
        }

        public async Task<IList<Class>> GetAllClasses()
        {
            return await _repository.GetAllClasses();
        }

        public async Task<IList<Class>> GetClassesByLevel(int levelId)
        {
            return await _repository.GetClassesByLevel(levelId);
        }

        public async Task<int> Save(Class classObj)
        {
            if(!await _repository.isExist(classObj.Id))
            {
                return await Add(classObj);
            }
            return await Update(classObj);
        }

        public async Task<IList<ClassComb>> GetClassesCombByLevel(int levelId)
        {
            return await _repository.GetClassesCombByLevel(levelId);
        }

        public Task<IList<ClassComb>> GetClassesInActiveYear()
        {
            return _repository.GetClassesInActiveYear();    
        }
    }


}