using System;
using System.Collections.Generic;

namespace Pay1194
{
    public partial class PaymentRecord
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string NiNo { get; set; } = null!;
        public DateTime PayDate { get; set; }
        public string PayMonth { get; set; } = null!;
        public int TaxYearId { get; set; }
        public string TaxCode { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal ContractualHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal ContractualEarnings { get; set; }
        public decimal OvertimeEarnings { get; set; }
        public decimal Tax { get; set; }
        public decimal Nic { get; set; }
        public decimal? UnionFee { get; set; }
        public decimal? Slc { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPayment { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual TaxYear TaxYear { get; set; } = null!;
    }
}
