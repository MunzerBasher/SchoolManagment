using DAL.Entities;
using BLL.IServices;
using DAL.IRepositories;



namespace BLL.Services
{


    public class ReportsServices : IReportsServices
    {

        public ReportsServices(IReportsRepository reportsRepository) { 
            
            _reportsRepository = reportsRepository;
        
        }



        private readonly IReportsRepository _reportsRepository;


        public async Task<IList<StudentsReport>> GetStudentsReport(int YearId, int SemesterId, int LevelId)
        {
            return await _reportsRepository.GetStudentsReport(YearId, SemesterId, LevelId); 
        }


    }


}