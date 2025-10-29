namespace DAL.Entities
{
    public class Fee
    {
        public int FeeId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty; 
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }  = string.Empty ;
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }

}