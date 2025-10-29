using SchoolDLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolDLL
{
    public interface IYearServices
    {
        
        Task<Year> GetYear(int Id);

        Task<IList<Year>> GetAllYear();

        Task<int> Delete(int Id);

        Task<int> Add(Year year);

        Task<int> Update(Year year);

        Task<int> Save(Year year);

        Task<bool> isExist(int Id);


    }


    



}
