using System.Data;
using SchoolDLL.Entities;
using SchoolDLL.IRepositories;
using Microsoft.Data.SqlClient;



namespace SchoolDLL.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly IDBHelper _dBHelper;

        public SectionRepository(IDBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }

        public async Task<bool> isExist(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@Id", Id),
             };

            var result = await _dBHelper.ExecuteScalarProcedure("Section_Exist", parameters);
            return Convert.ToInt32(result) > 1;
        }

        //SectionComb

        private SectionComb MapRowToCombSection(DataRow row)
        {
            return new SectionComb
            {
                Id = Convert.ToInt32(row["SectionId"]),
                Name = row["SectionName"].ToString()!,
                
            };
        }



        private SectionTable MapRowToSection(DataRow row)
        {
            return new SectionTable
            {
                Id = Convert.ToInt32(row["SectionId"]),
                Name = row["SectionName"].ToString()!,
                ClassName = row["ClassName"].ToString()!,
                LevelName = row["LevelName"].ToString()!,
                SemesterName = row["SemesterName"].ToString()!,
                YearName = row["YearName"].ToString()!
            };
        }

  
        public async Task<int> Add(Section section)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SectionName", section.Name),
                new SqlParameter("@ClassId", section.ClassId),
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Section_Insert", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> Update(Section section)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SectionId", section.Id),
                new SqlParameter("@SectionName", section.Name),
                new SqlParameter("@ClassId", section.ClassId),
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Section_Update", parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> Delete(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SectionId", id),
            };
            var result = await _dBHelper.ExecuteScalarProcedure("Section_Delete", parameters);
            return Convert.ToInt32(result);
        }

      
        public async Task<SectionTable> GetSection(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SectionId", id),
                new SqlParameter("@ClassId", DBNull.Value)
            };
            var result = await _dBHelper.ExecuteSelectProcedure("Section_Select", parameters);

            return result.Rows.Count > 0 ? MapRowToSection(result.Rows[0]) : null!;
        }

        public async Task<IList<SectionTable>> GetAllSections()
        {
            var result = await _dBHelper.ExecuteSelectProcedure("Section_Select");
            IList<SectionTable> sections = new List<SectionTable>();
            foreach (DataRow row in result.Rows)
            {
                sections.Add(MapRowToSection(row));
            }
            return sections;
        }


        public async Task<IList<SectionComb>> GetAllCombSections(int classId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", classId),
            };
            var result = await _dBHelper.ExecuteSelectProcedure("Section_CombSelect", parameters);
            IList<SectionComb> sections = new List<SectionComb>();
            foreach (DataRow row in result.Rows)
            {
                sections.Add(MapRowToCombSection(row));
            }
            return sections;
        }


        public async Task<IList<SectionTable>> GetSectionsByClass(int classId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClassId", classId),
                new SqlParameter("@SectionId", DBNull.Value)
            };

            var result = await _dBHelper.ExecuteSelectProcedure("Section_Select", parameters);

            IList<SectionTable> sections = new List<SectionTable>();
            foreach (DataRow row in result.Rows)
            {
                sections.Add(MapRowToSection(row));
            }
            return sections;
        }
    }
}