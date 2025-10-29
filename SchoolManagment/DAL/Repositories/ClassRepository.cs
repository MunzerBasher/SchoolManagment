using System;
using System.Data;
using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using Microsoft.Data.SqlClient;



namespace SchoolDLL.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IDBHelper _dBHelper;

        public ClassRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }


     
        private ClassComb MapRowToClassComb(DataRow row)
        {

            return new ClassComb
            {
                Name = row["ClassName"].ToString()!,
                Id = Convert.ToInt32(row["ClassId"]),

            };
        }

        private Class MapRowToClass(DataRow row)
        {
            
            return new Class
            {
                Name = row["ClassName"].ToString()!,
                Id = Convert.ToInt32(row["ClassId"]),

                YearName = row["YearName"].ToString()!,
                LevelName = row["LevelName"].ToString()!,
                SemesterName = row["SemesterName"].ToString()!,

            };
        }

       
        public async Task<IList<Class>> GetAllClasses()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("Class_Select");

            IList<Class> classes = new List<Class>();

            foreach (DataRow row in result.Rows)
            {
                classes.Add(MapRowToClass(row));
            }

            return classes;
        }


        public async Task<int> Add(Class classObj)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassName", classObj.Name),
                new SqlParameter("@LevelId", classObj.LevelId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Class_Insert", parameters);
            return Convert.ToInt32(result);
        }


        public async Task<int> Update(Class classObj)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", classObj.Id),
                new SqlParameter("@ClassName", classObj.Name),
                new SqlParameter("@LevelId", classObj.LevelId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Class_Update", parameters); 
            return Convert.ToInt32(result);
        }


        public async Task<int> Delete(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", id),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Class_Delete", parameters); 
            return Convert.ToInt32(result);
        }

        
        public async Task<Class> GetClass(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", id),
                new SqlParameter("@LevelId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Class_Select", parameters);

            if (result.Rows.Count > 0)
            {
                return MapRowToClass(result.Rows[0]);
            }
            return null;
        }

        


        public async Task<IList<Class>> GetClassesByLevel(int levelId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LevelId", levelId),
                new SqlParameter("@ClassId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Class_Select", parameters);

            IList<Class> classes = new List<Class>();

            foreach (DataRow row in result.Rows)
            {
                classes.Add(MapRowToClass(row));
            }

            return classes;
        }


        //GetClassesInActiveYear

        public async Task<IList<ClassComb>> GetClassesInActiveYear()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("GetClassesInActiveYear");

            IList<ClassComb> classes = new List<ClassComb>();

            foreach (DataRow row in result.Rows)
            {
                classes.Add(MapRowToClassComb(row));
            }

            return classes;
        }



        public async Task<IList<ClassComb>> GetClassesCombByLevel(int levelId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LevelId", levelId),
                new SqlParameter("@ClassId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Class_SelectComb", parameters);

            IList<ClassComb> classes = new List<ClassComb>();

            foreach (DataRow row in result.Rows)
            {
                classes.Add(MapRowToClassComb(row));
            }

            return classes;
        }


        public async Task<bool> isExist(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@ClassId", Id),
             };

            var result = await _dBHelper.ExecuteScalarProcedure("IsClassExist", parameters);
            return Convert.ToInt32(result) > 1;
        }



    }

}