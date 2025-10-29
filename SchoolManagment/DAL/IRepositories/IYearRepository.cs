

using SchoolDLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolDLL.IRepositories
{


    public interface IYearRepository
    {


        Task<Year> GetYear(int Id);

        Task<IList<Year>> GetAllYear();

        Task<int> Delete(int Id);

        Task<int> Add(Year year);

        Task<bool> isExist(int Id);

        Task<int> Update(Year year);


    }





}
