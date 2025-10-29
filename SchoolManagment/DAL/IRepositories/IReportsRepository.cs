





using DAL.Entities;

namespace DAL.IRepositories
{
    public interface IReportsRepository
    {
        Task<IList<StudentsReport>> GetStudentsReport(int YearId , int SemesterId , int LevelId);


    }


}
