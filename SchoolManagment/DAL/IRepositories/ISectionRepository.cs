using SchoolDLL.Entities;




namespace SchoolDLL.IRepositories
{


    public interface ISectionRepository
    {
        Task<int> Delete(int id);
        Task<bool> isExist(int Id);
        Task<int> Add(Section section);
        Task<SectionTable> GetSection(int id);
        Task<int> Update(Section section);
        Task<IList<SectionTable>> GetAllSections();
        Task<IList<SectionComb>> GetAllCombSections(int classId);
        Task<IList<SectionTable>> GetSectionsByClass(int classId);
    }


}