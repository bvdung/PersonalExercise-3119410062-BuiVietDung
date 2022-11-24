using Microsoft.AspNetCore.Mvc.Rendering;
using Pay1194.Entity;
using Pay1194.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay1194.Service.Implementation
{
    public class PayService : IPayService
    {
        private readonly ApplicationDbContext _context;

        private decimal overTimeHours;

        private decimal contractualEarnings;
        public PayService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if(hoursWorked< contractualHours)
            {
                contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }
            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll()
        {
            return _context.PaymentRecords.ToList();
        }

        // public IEnumerable<SelectListItem> GetAllTaxYear()
        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select( taxYears => new SelectListItem
                 {
                    Text = taxYears.YearOfTax,
                    Value = taxYears.Id.ToString()
                });
            return allTaxYear;
        }

        public PaymentRecord GetById(int id)
        {
            return _context.PaymentRecords.Where(PaymentRecords => PaymentRecords.Id == id).FirstOrDefault();
        }

        public TaxYear GetTaxYearById(int id)
        {
            return _context.TaxYears.Where(TaxYears => TaxYears.Id == id).FirstOrDefault();
        }

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction)
        => totalEarnings - totalDeduction;

        public decimal OverTimeEarnings(decimal overtimeRate, decimal overtimeHours)
        => overtimeRate * overtimeHours;

        public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
            {
                overTimeHours = 0.00m;
            }
            else
            {
                overTimeHours = hoursWorked - contractualHours;
            }
            return overTimeHours;
        }

        public decimal OverTimerate(decimal hourlyRate)
        => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal UnionFees)
        => tax + nic + studentLoanRepayment + UnionFees;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarnings;
    }
}
