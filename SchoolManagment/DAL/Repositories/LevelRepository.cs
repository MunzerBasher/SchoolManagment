using System;
using System.Data;
using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using Microsoft.Data.SqlClient;

namespace SchoolDLL.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly IDBHelper _dBHelper;

        public LevelRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }


        private LevleComb MapRowToLevelComb(DataRow row)
        {
            return new LevleComb
            {
                Id = Convert.ToInt32(row["LevelId"]),
                Name = row["LevelName"].ToString()!,
 
            };
        }
        
        private LevleTable MapRowToLevel(DataRow row)
        {
            return new LevleTable
            {
                Id = Convert.ToInt32(row["LevelId"]),
                Name = row["LevelName"].ToString()!,
                SemesterName = row["SemesterName"].ToString()!,
                YearName = row["YearName"].ToString()!
            };
        }

        
        public async Task<int> Add(Level level)
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var result = 0;
            try
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@LevelName", level.Name),
                    new SqlParameter("@SemesterId", level.SemesterId),
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                result = Convert.ToInt32( await _dBHelper.ExecuteScalarProcedure("Level_Insert", parameters));
            }
            return (result);
        }

        
        
        public async Task<int> Update(Level level)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LevelId", level.Id),
                new SqlParameter("@LevelName", level.Name),
                new SqlParameter("@SemesterId", level.SemesterId),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Level_Update", parameters);
            return Convert.ToInt32(result);
        }

        

        
        public async Task<int> Delete(int id)
        {
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LevelId", id),
            };

            var result = await _dBHelper.ExecuteScalarProcedure("Level_Delete", parameters);
            return Convert.ToInt32(result);
        }

        

        
        public async Task<LevleTable> GetLevel(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                
                new SqlParameter("@LevelId", id),
                new SqlParameter("@SemesterId", DBNull.Value),
                new SqlParameter("@YearId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Level_Select", parameters);

            if (result.Rows.Count > 0)
            {
                return MapRowToLevel(result.Rows[0]);
            }
            return null!;
        }


       
        public async Task<IList<LevleTable>> GetAllLevels(int SemesterId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SemesterId", SemesterId),
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Level_Select", parameters);

            IList<LevleTable> levels = new List<LevleTable>();

            foreach (DataRow row in result.Rows)
            {
                levels.Add(MapRowToLevel(row));
            }

            return levels;
        }

       
        public async Task<IList<LevleTable>> GetLevelsBySemester(int semesterId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SemesterId", semesterId),
                new SqlParameter("@LevelId", DBNull.Value),
                new SqlParameter("@YearId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Level_Select", parameters);

            IList<LevleTable> levels = new List<LevleTable>();

            foreach (DataRow row in result.Rows)
            {
                levels.Add(MapRowToLevel(row));
            }

            return levels;
        }

        public async Task<bool> isExist(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@YearId", Id),
             };

            var result = await _dBHelper.ExecuteScalarProcedure("AcademicYear_Exist", parameters);
            return Convert.ToInt32(result) > 1;
        }

        //Level_Comb

        public async Task<IList<LevleComb>> GetLevelsCombBySemester(int semesterId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SemesterId", semesterId),
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Level_Comb", parameters);

            IList<LevleComb> levels = new List<LevleComb>();

            foreach (DataRow row in result.Rows)
            {
                levels.Add(MapRowToLevelComb(row));
            }

            return levels;
        }

        public async Task<IList<LevleComb>> GetActiveLevelsComb()
        { 
            var result = await _dBHelper.ExecuteSelectProcedure("GetLevelCombInActiveYear");

            IList<LevleComb> levels = new List<LevleComb>();

            foreach (DataRow row in result.Rows)
            {
                levels.Add(MapRowToLevelComb(row));
            }

            return levels;
        }



        public async Task<IList<LevleTable>> GetLevelsByYear(int yearId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@YearId", yearId),
                new SqlParameter("@LevelId", DBNull.Value),
                new SqlParameter("@SemesterId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Level_Select", parameters);

            IList<LevleTable> levels = new List<LevleTable>();

            foreach (DataRow row in result.Rows)
            {
                levels.Add(MapRowToLevel(row));
            }

            return levels;
        }
    }

}