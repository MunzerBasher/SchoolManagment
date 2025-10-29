using DAL.Entities;

namespace BLL.IServices
{




    public interface IReportsServices
    {
        Task<IList<StudentsReport>> GetStudentsReport(int YearId, int SemesterId, int LevelId);


    }






}