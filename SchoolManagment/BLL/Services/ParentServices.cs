using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using SchoolBLL.IServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SchoolBLL.Services
{
    public class ParentServices : IParentServices
    {
        private readonly IParentRepository _repository;

        public ParentServices(IParentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(Parent parent)
        {
            
            return await _repository.Add(parent);
        }

        public async Task<int> Update(Parent parent)
        {
            return await _repository.Update(parent);
        }

        public async Task<int> Delete(int id)
        {
            
            return await _repository.Delete(id);
        }

        public async Task<Parent> GetParent(int id)
        {
            return await _repository.GetParent(id);
        }

        public async Task<Parent> GetParentByPhone(string phone)
        {
            return await _repository.GetParentByPhone(phone);
        }

        public async Task<IList<Parent>> GetAllParents()
        {
            return await _repository.GetAllParents();
        }
    
    }

}