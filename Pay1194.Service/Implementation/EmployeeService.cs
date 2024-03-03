using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pay1194.Entity;
using Pay1194.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay1194.Service.Implementation
{
    public class EmployeeService : IEmployee

    {
        private readonly ApplicationDbContext _context;

        private decimal studentLoanRepaymentAmount;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Where(employee => employee.Id == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public IEnumerable<SelectListItem> GetEmployeeForPayRoll()
        {
            var allEmployee = _context.Employees.Select(employee => new SelectListItem
            {
                Text = employee.FullName,
                Value = employee.Id.ToString()
            });
            return allEmployee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetById(id);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = GetById(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }


        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            var emp = GetById(id);
            if (emp.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanRepaymentAmount = 11m;
            }
            else if (emp.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
            {
                studentLoanRepaymentAmount = 26m;
            }
            else if (emp.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                studentLoanRepaymentAmount = 45m;
            }
            else if (emp.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
            {
                studentLoanRepaymentAmount = 73m;
            }
            else
            {
                studentLoanRepaymentAmount = 0m;
            }
            return studentLoanRepaymentAmount;
        }

        public decimal UnionFee(int id)
        {
            var Emp = GetById(id);
            var FeeUnion = Emp.UnionMember == UnionMember.Yes ? 10m : 0m;
            return FeeUnion;
        }

        public decimal add () {
            int a = 6;
        }

        
    }
}
