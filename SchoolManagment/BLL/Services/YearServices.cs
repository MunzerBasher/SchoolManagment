using SchoolDLL;
using SchoolDLL.Entities;
using System.Threading.Tasks;
using SchoolDLL.IRepositories;
using System.Collections.Generic;


namespace SchoolBLL.Services
{
    public class YearServices : IYearServices
    {
        public YearServices(IYearRepository repository)
        {
            _repository = repository;
        }
        private readonly IYearRepository _repository;


        public async Task<int> Add(Year year)
        {
            var result = await _repository.Add(year);   
            return result;
        }



        public async Task<int> Delete(int Id)
        {
            var result = await _repository.Delete(Id);
            return result;
        }

        public async Task<Year> GetYear(int Id)
        {
            var result = await _repository.GetYear(Id);
            return result;
        }

        public async Task<int> Update(Year year)
        {
            var result = await _repository.Update(year);
            return result;
        }

        public async Task<IList<Year>> GetAllYear()
        {
            var result = await _repository.GetAllYear();
            return result;
        }


        public async Task<int> Save(Year year)
        {
         
            if(year.Id == -1 || ! await isExist(year.Id))
            {
                return await Add(year);
            }
            return await Update(year);
        }


        public async Task<bool> isExist(int Id)
        {
            return await _repository.isExist(Id);
        }


    }

}