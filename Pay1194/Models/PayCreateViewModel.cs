using Pay1194.Entity;
using System.ComponentModel.DataAnnotations;

namespace Pay1194.Models
{
    public class PayCreateViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Pay1194.Entity.Employee? Employee { get; set; }

        public String? FullName { get; set; }

        public String? NiNo { get; set; }

        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        public DateTime PayMonth { get; set; } 

        public int TaxYearId { get; set; }

        public TaxYear? TaxYear { get; set; }

        public String TaxCode { get; set; } = "1250L";

        public decimal HourlyRate { get; set; }

        public decimal HoursWorked { get; set; }

        public decimal ContractualHours { get; set; } = 144m;

        public decimal OvertimeHours { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OvertimeEarnings { get; set; }

        public decimal Tax { get; set; }

        public decimal NIC { get; set; }

        public decimal? UnionFee { get; set; }

        public Nullable<decimal> SLC { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
