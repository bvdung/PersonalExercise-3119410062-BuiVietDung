using Pay1194.Entity;
using System.ComponentModel.DataAnnotations;

namespace Pay1194.Models
{
    public class PayIndexViewModel
    {
        public int Id { get; set; }

        public Pay1194.Entity.Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string NiNo { get; set; }

        //    public DateTime MonthPay { get; set; }

        public DateTime MonthPay { get; set; }

        public DateTime DatePay { get; set; }

        public Pay1194.Entity.TaxYear TaxYear { get; set; }

        public string YearOfTax { get; set; }
        public string TaxCode { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OvertimeEarnings { get; set; }

        public decimal Tax { get; set; }

        public decimal NiC { get; set; }

        public decimal UnionFee { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
