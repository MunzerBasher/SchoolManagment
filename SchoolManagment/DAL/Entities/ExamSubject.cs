

namespace DAL.Entities
{
    public class ExamSubject
    {
        public int ExamSubjectId { get; set; }
        public int ExamId { get; set; }
        public string Exam { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public decimal MaxMark { get; set; }
        public decimal MinMark { get; set; }
        public DateTime ExamDate { get; set; }
    }

}
