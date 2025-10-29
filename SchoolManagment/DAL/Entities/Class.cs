namespace SchoolDLL.Entities
{
    
    
    
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        
        public int LevelId { get; set; }    
        public string SemesterName { get; set; } = string.Empty;
       
        public string YearName { get; set; } = string.Empty;

        public string LevelName { get; set; } = string.Empty;
    }

    public class ClassComb
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }



}