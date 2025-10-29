namespace DLL.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string SubjectName { get; set; } = string.Empty ;
        public bool Status { get; set; } = true;
    }
}
