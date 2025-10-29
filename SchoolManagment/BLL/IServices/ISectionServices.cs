using SchoolDLL.Entities;

namespace SchoolBLL.IServices
{

    public interface ISectionServices
    {
        Task<int> Delete(int id);
        Task<bool> IsExist(int id);
        Task<int> Add(Section section);
        Task<int> Save(Section section);
        Task<SectionTable> GetSection(int id);
        Task<int> Update(Section section);
        Task<IList<SectionTable>> GetAllSections();
        Task<IList<SectionComb>> GetAllCombSections(int classId);
        Task<IList<SectionTable>> GetSectionsByClass(int classId);
    }


}