using SchoolDLL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SchoolDLL.IRepositories
{
    public interface IParentRepository
    {
        Task<int> Add(Parent parent);
        Task<int> Update(Parent parent);
        Task<int> Delete(int id);
        Task<Parent> GetParent(int id);
        Task<Parent> GetParentByPhone(string phone);
        Task<IList<Parent>> GetAllParents();
    }
}