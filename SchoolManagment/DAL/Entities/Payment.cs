﻿public class Payment
{
    public int PaymentId { get; set; }
    public int FeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Notes { get; set; }
}
