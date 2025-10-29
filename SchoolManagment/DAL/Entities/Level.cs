
namespace SchoolDLL.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SemesterId { get; set; }



        public string SemesterName { get; set; } = string.Empty;
        public int YearId { get; set; }
        public string YearName { get; set; } = string.Empty ;
    }


    public class LevleTable
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SemesterName { get; set; } = string.Empty ;
       
        public string YearName { get; set; } = string.Empty;

    }

    public class LevleComb
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

       

    }

}
