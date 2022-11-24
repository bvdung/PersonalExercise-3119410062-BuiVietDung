namespace Pay1194.Models
{
    public class PayDetailViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Pay1194.Entity.Employee? Employee { get; set; }
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? PostCode { get; set; }

        public string? Degisnation { get; set; }

        public string? NationalInsuranceNumber { get; set; }

        public string ImageUrl { get; set; }
        public string NiNo { get; set; }
        public DateTime DatePay { get; set; }
        public DateTime MonthPay { get; set; }
        public int TaxYearId { get; set; }

        public string year { get; set; }
        public TaxYear? TaxYear { get; set; }
        public String TaxCode { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal HoursWorked { get; set; }

        public decimal ContractualHours { get; set; }

        public decimal OvertimeHours { get; set; }

        public decimal OvertimeRate { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OvertimeEarnings { get; set; }

        public decimal Tax { get; set; }

        public decimal NIC { get; set; }

        public decimal UnionFee { get; set; }
        public Nullable<decimal> SLC { get; set; }
        public decimal TotalEarnings { get; set; }
       
        public decimal TotalDeduction { get; set; }
        public decimal NetPayment { get; set; }
       
    }
}
