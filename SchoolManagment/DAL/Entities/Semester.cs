

namespace SchoolDLL.Entities
{
    public class Semester
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int YearId { get; set; }

       
        public string YearName { get; set; } = string.Empty;

    }
    
    public class MinSemester
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

    }




}
