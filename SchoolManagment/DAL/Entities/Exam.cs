

namespace DAL.Entities
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AcademicYearId { get; set; }
        public string AcademicYearName { get; set; } = string.Empty;
        public int SemesterId { get; set; }
        public string SemesterName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

}
