using System;
using System.Collections.Generic;

namespace Pay1194
{
    public partial class Employee
    {
        public Employee()
        {
            PaymentRecords = new HashSet<PaymentRecord>();
        }

        public int Id { get; set; }
        public string EmployeeNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public string Designation { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string NationalInsuranceNo { get; set; } = null!;
        public int PaymentMethod { get; set; }
        public int StudentLoan { get; set; }
        public int UnionMember { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostCode { get; set; } = null!;

        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
