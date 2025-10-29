using SchoolDLL.Entities;
using SchoolBLL.IServices;
using SchoolDLL.IRepositories;




namespace SchoolBLL.Services
{
    public class SectionServices : ISectionServices
    {
        private readonly ISectionRepository _repository;

        public SectionServices(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(Section section)
        {
           

            return await _repository.Add(section);
        }

        public async Task<int> Update(Section section)
        {
            return await _repository.Update(section);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<SectionTable> GetSection(int id)
        {
            return await _repository.GetSection(id);
        }

        public async Task<IList<SectionTable>> GetAllSections()
        {
            return await _repository.GetAllSections();
        }

        public async Task<IList<SectionTable>> GetSectionsByClass(int classId)
        {
            return await _repository.GetSectionsByClass(classId);
        }

        public async Task<bool> IsExist(int id)
        {
            return await    _repository.isExist(id);
        }

        public async Task<int> Save(Section section)
        {
            if(!await IsExist(section.Id))
            {
                return await Add(section);
            }
            return await Update(section);
        
        }

        public async Task<IList<SectionComb>> GetAllCombSections(int classId)
        {
            return await _repository.GetAllCombSections(classId);
        }


    }

}