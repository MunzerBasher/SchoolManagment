namespace SchoolDLL.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ClassId { get; set; }

        
        public string ClassName { get; set; } = string.Empty;
        public int LevelId { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public string SemesterName { get; set; } = string.Empty;
        public string YearName { get; set; }    = string.Empty;
    }



    public class SectionTable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
       
        public string ClassName { get; set; } = string.Empty ;
        public string LevelName { get; set; } = string.Empty;
        public string SemesterName { get; set; } = string.Empty;
        public string YearName { get; set; } = string.Empty;
    }

    public class SectionComb
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }


}