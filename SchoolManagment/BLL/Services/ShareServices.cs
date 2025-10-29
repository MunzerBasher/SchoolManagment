using SchoolDLL;
using BLL.IServices;
using SchoolDLL.Entities;
using SchoolBLL.IServices;
using DLL.Entities;




namespace BLL.Services
{
    public class ShareServices : IShareServices
    {

        public ShareServices(ISubjectServices subjectServices,ISectionServices sectionServices,IClassServices classServices,ILevelServices levelServices, IYearServices yearServices, ISemesterServices semesterServices)
        {
            this._yearServices = yearServices;
            this._levelServices = levelServices;
            this._classServices = classServices;
            this._subjectServices = subjectServices;    
            this._sectionServices = sectionServices;
            this._semesterServices = semesterServices;
        }

        private readonly ISubjectServices _subjectServices;
        private readonly ISectionServices _sectionServices;
        private readonly ISemesterServices _semesterServices;
        private readonly IYearServices _yearServices;
        private readonly ILevelServices _levelServices;
        private readonly IClassServices _classServices;

        public async Task<IList<Year>> GetYearCombAsync()
        {
            return await _yearServices.GetAllYear();
        }

        public Task<IList<LevleComb>> GetLevleCombAsync(int semesterId)
        {
            return _levelServices.GetLevelsCombBySemester(semesterId);
        }

        public Task<IList<MinSemester>> GetMinSemesterCombAsync(int yearId)
        {
            return _semesterServices.GetAllMinSemester(yearId);
        }

        public Task<int> SaveClass(Class clas)
        {
            return _classServices.Save(clas);
        }

        public async Task<IList<ClassComb>> GetClassesCombByLevel(int levelId)
        {
            return await _classServices.GetClassesCombByLevel(levelId);
        }

        public async Task<IList<ClassComb>> GetClassesInActiveYear()
        {
            return await _classServices.GetClassesInActiveYear();
        }

        public async Task<IList<LevleComb>> GetActiveLevelsComb()
        {
            return await _levelServices.GetActiveLevelsComb();
        }

        public async Task<IList<SectionComb>> GetAllCombSections(int classId)
        {
            return await _sectionServices.GetAllCombSections(classId);
        }

        public async Task<IList<SubjectsCombox>> GetAllSubjectsCombox()
        {
            return await _subjectServices.GetAllSubjectsCombox();
        }

    }

}