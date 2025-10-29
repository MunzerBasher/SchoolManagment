

namespace SchoolDLL.Entities
{
    namespace DLL.Entities
    {
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime BirthDate { get; set; }
            public string ParentPhone { get; set; } = string.Empty;
            public int GenderId { get; set; }
            public int ClassId { get; set; }
            public int SectionId { get; set; }
        }


        public class StudentTable
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime BirthDate { get; set; }
            public string ParentPhone { get; set; } = string.Empty;
            public string GenderName { get; set; } = string.Empty;
            public string ClassName { get; set; } = string.Empty;
            public string SectionName { get; set; } = string.Empty;
        }
    }





}