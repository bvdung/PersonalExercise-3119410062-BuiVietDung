using System;
using System.Collections.Generic;

namespace Pay1194
{
    public partial class TaxYear
    {
        public TaxYear()
        {
            PaymentRecords = new HashSet<PaymentRecord>();
        }

        public int Id { get; set; }
        public string YearOfTax { get; set; } = null!;

        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
