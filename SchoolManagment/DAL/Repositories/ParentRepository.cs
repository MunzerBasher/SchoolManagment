using System;
using System.Data;
using System.Data.SqlClient;
using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using Microsoft.Data.SqlClient;

namespace SchoolDLL.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly IDBHelper _dBHelper;

        public ParentRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        private Parent MapRowToParent(DataRow row)
        {
            return new Parent
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
                Phone = row["Phone"].ToString()!,
            };
        }

       

        public async Task<int> Add(Parent parent)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", parent.Name),
                new SqlParameter("@Phone", parent.Phone)
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Parent_Insert", parameters);
            return Convert.ToInt32(result);
        }

        
        
        public async Task<int> Update(Parent parent)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", parent.Id),
                new SqlParameter("@Name", parent.Name),
                new SqlParameter("@Phone", parent.Phone)
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Parent_Update", parameters);
            return Convert.ToInt32(result);
        }

        
        public async Task<int> Delete(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id),
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Parent_Delete", parameters);
            return Convert.ToInt32(result);
        }

      
        public async Task<Parent> GetParent(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Phone", DBNull.Value)
            };
            var result = await _dBHelper.ExecuteSelectProcedure("Parent_Select", parameters);

            return (result.Rows.Count > 0 ? MapRowToParent(result.Rows[0]) : null)!;
        }

        public async Task<Parent> GetParentByPhone(string phone)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", DBNull.Value),
                new SqlParameter("@Phone", phone)
            };
            var result = await _dBHelper.ExecuteSelectProcedure("Parent_Select", parameters);

            return (result.Rows.Count > 0 ? MapRowToParent(result.Rows[0]) : null)!;
        }

        public async Task<IList<Parent>> GetAllParents()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("Parent_Select");
            IList<Parent> parents = new List<Parent>();
            foreach (DataRow row in result.Rows)
            {
                parents.Add(MapRowToParent(row));
            }
            return parents;
        }
    }

}