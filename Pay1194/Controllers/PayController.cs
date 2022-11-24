using Microsoft.AspNetCore.Mvc;
using Pay1194.Entity;
using Pay1194.Models;
using Pay1194.Service;
using Pay1194.Service.Implementation;
using System.Net;

namespace Pay1194.Controllers
{
    public class PayController : Controller
    {
        private readonly INationalInsuranceService _nationalInsuranceService;
        private readonly IPayService _payService;
        private readonly IEmployee _employeeService;
        private readonly ITaxService _taxService;
        private readonly IWebHostEnvironment _hostEnvironment;

        private decimal overtimeHrs;
        private decimal contractualEarnings;
        private decimal overtimeEarnings;
        private decimal totalEarnings;
        private decimal unionFee;
        private decimal studentloan;
        private decimal nationalInsurance;
        private decimal totalDeduction;
        private decimal tax;

        public PayController(IPayService payService, IEmployee employeeService, ITaxService taxService, INationalInsuranceService nationalInsuranceServi, IWebHostEnvironment hostEnvironment)
        {
            _nationalInsuranceService = nationalInsuranceServi;
            _payService = payService;
            _employeeService = employeeService;
            _taxService = taxService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employess = _employeeService.GetEmployeeForPayRoll();
            ViewBag.taxYear = _payService.GetAllTaxYear();
            var model = new PayCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PayCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var payrecord = new Pay1194.Entity.PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    NiNo = _employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    DatePay = model.PayDate,
                    MonthPay = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HourlyWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OverTimeHours = overtimeHrs = _payService.OverTimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contractualEarnings = _payService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings = overtimeEarnings = _payService.OverTimeEarnings(_payService.OverTimerate(model.HourlyRate), overtimeHrs),
                    TotalEarnings = totalEarnings = _payService.TotalEarnings(overtimeEarnings, contractualEarnings),
                    Tax = tax = _taxService.TaxAmount(totalEarnings),
                    UnionFee = unionFee = _employeeService.UnionFee(model.EmployeeId),
                    SLC = studentloan = _employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings),
                    NiC = nationalInsurance = _nationalInsuranceService.NIContribution(totalEarnings),
                    TotalDeduction = totalDeduction = _payService.TotalDeduction(tax, nationalInsurance, studentloan, unionFee),
                    NetPayment = _payService.NetPay(totalEarnings, totalDeduction)

                };
                await _payService.CreateAsync(payrecord);
                return RedirectToAction("Index");
            }
            ViewBag.employees = _employeeService.GetEmployeeForPayRoll();
            ViewBag.taxYears = _payService.GetAllTaxYear();
            return View(model);
        }

        public IActionResult Index()
        {
            var model = _payService.GetAll().Select(pay => new PayIndexViewModel
            {
                Id = pay.Id,
           
                EmployeeId = pay.EmployeeId,
                FullName = _employeeService.GetById(pay.EmployeeId).FullName,
                NiNo = pay.NiNo,
                MonthPay = pay.MonthPay,
                DatePay = pay.DatePay,
                TaxYear = pay.TaxYear,
                YearOfTax = _payService.GetTaxYearById(pay.TaxYearId).YearOfTax,
                TaxCode = pay.TaxCode,
                ContractualEarnings = pay.ContractualEarnings,
                OvertimeEarnings = pay.OvertimeEarnings,
                Tax = pay.Tax,
                NiC = pay.NiC,
                UnionFee = pay.UnionFee,
                TotalEarnings = pay.TotalEarnings,
                TotalDeduction = pay.TotalDeduction,
                NetPayment = pay.NetPayment

            }).ToList();
            return View(model);
        }


        public IActionResult Detail(int id)
        {
            var paymentRecord = _payService.GetById(id);
            if(paymentRecord == null)
            {
                return NotFound();
            }
            var model = new PayDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = _employeeService.GetById(paymentRecord.EmployeeId).FullName,
                ImageUrl = _employeeService.GetById(paymentRecord.EmployeeId).ImageUrl,
                TaxYearId = paymentRecord.TaxYearId,
                year = _payService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HourlyWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHours = paymentRecord.OverTimeHours,
                OvertimeRate = _payService.OverTimerate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax ,
                NIC = paymentRecord.NiC,
                SLC = paymentRecord.SLC,
                NiNo = paymentRecord.NiNo,
                DatePay = paymentRecord.DatePay,
                MonthPay = paymentRecord.MonthPay,
                TotalEarnings = paymentRecord.TotalEarnings,
                UnionFee = paymentRecord.UnionFee,
                TotalDeduction = paymentRecord.TotalDeduction,
                NetPayment = paymentRecord.NetPayment
            };
            return View(model);

        }



        public IActionResult Payslip(int id)
        {
            var paymentRecord = _payService.GetById(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }
            var model = new PayDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = _employeeService.GetById(paymentRecord.EmployeeId).FullName,
                Address = _employeeService.GetById(paymentRecord.EmployeeId).Address,
                City = _employeeService.GetById(paymentRecord.EmployeeId).City,
                PostCode = _employeeService.GetById(paymentRecord.EmployeeId).PostCode,
                Degisnation = _employeeService.GetById(paymentRecord.EmployeeId).Designation,
                NationalInsuranceNumber = _employeeService.GetById(paymentRecord.EmployeeId).NationalInsuranceNo,
                ImageUrl = _employeeService.GetById(paymentRecord.EmployeeId).ImageUrl,
                TaxYearId = paymentRecord.TaxYearId,
                year = _payService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HourlyWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHours = paymentRecord.OverTimeHours,
                OvertimeRate = _payService.OverTimerate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                NIC = paymentRecord.NiC,
                SLC = paymentRecord.SLC,
                NiNo = paymentRecord.NiNo,
                DatePay = paymentRecord.DatePay,
                MonthPay = paymentRecord.MonthPay,
                TotalEarnings = paymentRecord.TotalEarnings,
                UnionFee = paymentRecord.UnionFee,
                TotalDeduction = paymentRecord.TotalDeduction,
                NetPayment = paymentRecord.NetPayment
            };
            return View(model);

        }

    }
}
