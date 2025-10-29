namespace DAL.Entities
{
    public class StudentMarks
    {

        public int Id { get; set; }

        

        public int StudentId { get; set; }

        

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public decimal ObtainedMark { get; set; }

       
        public int SubjectId { get; set; }
    }

    public class StudentMarksTable
    {
     
        public int Id { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public decimal ObtainedMark { get; set; }


        public string Year { get; set; } = string.Empty;

        public decimal MaxMark { get; set; }

        public decimal MinMark { get; set; }

        public string ExamName { get; set; } = string.Empty;

    }

}