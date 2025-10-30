namespace DAL.Entities
{
    
    public class SalaryModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary => BasicSalary + Bonus - Deductions;
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Notes { get; set; } = string.Empty;

    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }


    public class Salary
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }



}