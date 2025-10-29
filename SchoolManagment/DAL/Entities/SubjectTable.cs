namespace DLL.Entities
{
    public class SubjectTable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        public string ClassName { get; set; } = string.Empty;
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
    }
    public class SubjectsCombox
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty ;
    }

}

