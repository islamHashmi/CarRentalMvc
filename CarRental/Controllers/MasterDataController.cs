using CarRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CarRental.Helpers;
using CarRental.Models;
using System.Data.Entity;

namespace CarRental.Controllers
{
    public class MasterDataController : Controller
    {
        CarRentalEntities db = new CarRentalEntities();

        public ActionResult Index()
        {
            return View();
        }

        #region Company Master

        [HttpGet]
        public async Task<ActionResult> CompanyList()
        {
            var list = new List<CompanyViewModel>();

            var model = await db.Companies.ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new CompanyViewModel
                    {
                        CompanyId = item.companyId,
                        CompanyName = item.companyName,
                        Address = item.address,
                        PanNumber = item.panNumber,
                        Telephone1 = item.telephone1,
                        Telephone2 = item.telephone2,
                        ServiceTaxNumber = item.serviceTaxNumber
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> CompanyDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Companies.FindAsync(_Id);

                var vm = new CompanyViewModel
                {
                    CompanyId = model.companyId,
                    CompanyName = model.companyName,
                    Address = model.address,
                    PanNumber = model.panNumber,
                    Telephone1 = model.telephone1,
                    Telephone2 = model.telephone2,
                    ServiceTaxNumber = model.serviceTaxNumber
                };

                return View(vm);
            }

            return RedirectToAction("CompanyList");
        }

        [HttpGet]
        public async Task<ActionResult> CompanyManage(string key)
        {
            var vm = new CompanyViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Companies.FindAsync(_Id);

                    vm.CompanyId = model.companyId;
                    vm.CompanyName = model.companyName;
                    vm.Address = model.address;
                    vm.PanNumber = model.panNumber;
                    vm.Telephone1 = model.telephone1;
                    vm.Telephone2 = model.telephone2;
                    vm.ServiceTaxNumber = model.serviceTaxNumber;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CompanyManage(CompanyViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Company();

                    if (vm.CompanyId == null)
                    {
                        if (db.Companies.Count(m => m.companyName == vm.CompanyName) > 0)
                        {
                            ModelState.AddModelError("", "Company Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Companies.Add(model);
                    }
                    else
                    {
                        model = await db.Companies.FindAsync(vm.CompanyId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.companyName = vm.CompanyName;
                    model.address = vm.Address;
                    model.telephone1 = vm.Telephone1;
                    model.telephone2 = vm.Telephone2;
                    model.panNumber = vm.PanNumber;
                    model.serviceTaxNumber = vm.ServiceTaxNumber;

                    await db.SaveChangesAsync();

                    return RedirectToAction("CompanyList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> CompanyDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Companies.FindAsync(_Id);

                var vm = new CompanyViewModel
                {
                    CompanyId = model.companyId,
                    CompanyName = model.companyName,
                    Address = model.address,
                    PanNumber = model.panNumber,
                    Telephone1 = model.telephone1,
                    Telephone2 = model.telephone2,
                    ServiceTaxNumber = model.serviceTaxNumber
                };

                return View(vm);
            }

            return RedirectToAction("CompanyList");
        }

        [HttpPost]
        [ActionName("CompanyDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CompanyDeleteConfirmed(string key, CompanyViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Companies.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("CompanyList");
            }

            return View(vm);
        }

        #endregion

        #region Branch Master

        [HttpGet]
        public async Task<ActionResult> BranchList()
        {
            var list = new List<BranchViewModel>();

            var model = await db.CompanyBranches.Include(e => e.Company).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new BranchViewModel
                    {
                        BranchId = item.branchId,
                        BranchName = item.branchName,
                        CompanyName = item.Company == null ? "" : item.Company.companyName
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> BranchDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.CompanyBranches.FindAsync(_Id);

                var vm = new BranchViewModel
                {
                    BranchId = model.branchId,
                    BranchName = model.branchName,
                    CompanyName = model.Company == null ? "" : model.Company.companyName
                };

                return View(vm);
            }

            return RedirectToAction("BranchList");
        }

        [HttpGet]
        public async Task<ActionResult> BranchManage(string key)
        {
            var vm = new BranchViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.CompanyBranches.FindAsync(_Id);

                    vm.BranchId = model.branchId;
                    vm.BranchName = model.branchName;
                    vm.CompanyId = model.companyId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Branch(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> BranchManage(BranchViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new CompanyBranch();

                    if (vm.BranchId == null)
                    {
                        if (db.CompanyBranches.Count(m => m.branchName == vm.BranchName && m.companyId == vm.CompanyId) > 0)
                        {
                            ModelState.AddModelError("", "Branch Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.CompanyBranches.Add(model);
                    }
                    else
                    {
                        model = await db.CompanyBranches.FindAsync(vm.BranchId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.branchName = vm.BranchName;
                    model.companyId = vm.CompanyId;

                    await db.SaveChangesAsync();

                    return RedirectToAction("BranchList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Branch(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> BranchDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.CompanyBranches.FindAsync(_Id);

                var vm = new BranchViewModel
                {
                    BranchId = model.branchId,
                    BranchName = model.branchName,
                    CompanyName = model.Company == null ? "" : model.Company.companyName
                };

                return View(vm);
            }

            return RedirectToAction("BranchList");
        }

        [HttpPost]
        [ActionName("BranchDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BranchDeleteConfirmed(string key, BranchViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.CompanyBranches.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("BranchList");
            }

            return View(vm);
        }

        private void BindDropdown_Branch(BranchViewModel vm)
        {
            vm.CompanyList = new SelectList(db.Companies.Where(m => m.active == true), "companyId", "companyName");
        }

        #endregion

        #region Department Master

        [HttpGet]
        public async Task<ActionResult> DepartmentList()
        {
            var list = new List<DepartmentViewModel>();

            var model = await db.Departments.ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new DepartmentViewModel
                    {
                        DepartmentId = item.departmentId,
                        DepartmentName = item.departmentName,
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> DepartmentDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Departments.FindAsync(_Id);

                var vm = new DepartmentViewModel
                {
                    DepartmentId = model.departmentId,
                    DepartmentName = model.departmentName,
                };

                return View(vm);
            }

            return RedirectToAction("DepartmentList");
        }

        [HttpGet]
        public async Task<ActionResult> DepartmentManage(string key)
        {
            var vm = new DepartmentViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Departments.FindAsync(_Id);

                    vm.DepartmentId = model.departmentId;
                    vm.DepartmentName = model.departmentName;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DepartmentManage(DepartmentViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Department();

                    if (vm.DepartmentId == null)
                    {
                        if (db.Departments.Count(m => m.departmentName == vm.DepartmentName) > 0)
                        {
                            ModelState.AddModelError("", "Department Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Departments.Add(model);
                    }
                    else
                    {
                        model = await db.Departments.FindAsync(vm.DepartmentId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.departmentName = vm.DepartmentName;

                    await db.SaveChangesAsync();

                    return RedirectToAction("DepartmentList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> DepartmentDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Departments.FindAsync(_Id);

                var vm = new DepartmentViewModel
                {
                    DepartmentId = model.departmentId,
                    DepartmentName = model.departmentName,
                };

                return View(vm);
            }

            return RedirectToAction("DepartmentList");
        }

        [HttpPost]
        [ActionName("DepartmentDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DepartmentDeleteConfirmed(string key, DepartmentViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Departments.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("DepartmentList");
            }

            return View(vm);
        }

        #endregion

        #region Employee Master

        [HttpGet]
        public async Task<ActionResult> EmployeeList()
        {
            var list = new List<EmployeeViewModel>();

            var model = await db.Employees.Include(e => e.Designation).Include(e => e.CompanyBranch).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new EmployeeViewModel
                    {
                        EmployeeId = item.employeeId,
                        EmployeeCode = item.employeeCode,
                        BranchName = item.CompanyBranch == null ? "" : item.CompanyBranch.branchName,
                        DesignationName = item.Designation == null ? "" : item.Designation.designationName,
                        EmployeeName = item.employeeName,
                        ResidentiallAddress = item.residentiallAddress,
                        NativeAddress = item.nativeAddress,
                        MobileNumber = item.mobileNumber,
                        ResidentialTelephone = item.residentialTelephone,
                        JoiningDate = item.joiningDate,
                        LeavingDate = item.leavingDate,
                        BasicAmount = item.basicAmount,
                        HraAmount = item.hraAmount,
                        CcAmount = item.ccAmount,
                        Bank = item.bank ?? false,
                        AccountNumber = item.accountNumber
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Employees.FindAsync(_Id);

                var vm = new EmployeeViewModel
                {
                    EmployeeId = model.employeeId,
                    EmployeeCode = model.employeeCode,
                    BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                    DesignationName = model.Designation == null ? "" : model.Designation.designationName,
                    EmployeeName = model.employeeName,
                    ResidentiallAddress = model.residentiallAddress,
                    NativeAddress = model.nativeAddress,
                    MobileNumber = model.mobileNumber,
                    ResidentialTelephone = model.residentialTelephone,
                    JoiningDate = model.joiningDate,
                    LeavingDate = model.leavingDate,
                    BasicAmount = model.basicAmount,
                    HraAmount = model.hraAmount,
                    CcAmount = model.ccAmount,
                    Bank = model.bank ?? false,
                    AccountNumber = model.accountNumber
                };

                return View(vm);
            }

            return RedirectToAction("EmployeeList");
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeManage(string key)
        {
            var vm = new EmployeeViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Employees.FindAsync(_Id);

                    vm.BranchId = model.branchId;
                    vm.DesignationId = model.designationId;
                    vm.EmployeeId = model.employeeId;
                    vm.EmployeeCode = model.employeeCode;
                    vm.EmployeeName = model.employeeName;
                    vm.ResidentiallAddress = model.residentiallAddress;
                    vm.NativeAddress = model.nativeAddress;
                    vm.MobileNumber = model.mobileNumber;
                    vm.ResidentialTelephone = model.residentialTelephone;
                    vm.JoiningDate = model.joiningDate;
                    vm.LeavingDate = model.leavingDate;
                    vm.BasicAmount = model.basicAmount;
                    vm.HraAmount = model.hraAmount;
                    vm.CcAmount = model.ccAmount;
                    vm.Bank = model.bank ?? false;
                    vm.AccountNumber = model.accountNumber;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Employee(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeManage(EmployeeViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Employee();

                    if (vm.EmployeeId == null)
                    {
                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Employees.Add(model);
                    }
                    else
                    {
                        model = await db.Employees.FindAsync(vm.EmployeeId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.branchId = vm.BranchId;
                    model.designationId = vm.DesignationId;
                    model.employeeName = vm.EmployeeName;
                    model.employeeCode = vm.EmployeeCode;
                    model.residentiallAddress = vm.ResidentiallAddress;
                    model.nativeAddress = vm.NativeAddress;
                    model.mobileNumber = vm.MobileNumber;
                    model.residentialTelephone = vm.ResidentialTelephone;
                    model.joiningDate = (DateTime)vm.JoiningDate;
                    model.leavingDate = vm.LeavingDate;
                    model.basicAmount = vm.BasicAmount;
                    model.hraAmount = vm.HraAmount;
                    model.ccAmount = vm.CcAmount;
                    model.bank = vm.Bank;
                    model.accountNumber = vm.AccountNumber;

                    await db.SaveChangesAsync();

                    return RedirectToAction("EmployeeList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Employee(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Employees.FindAsync(_Id);

                var vm = new EmployeeViewModel
                {
                    EmployeeId = model.employeeId,
                    EmployeeCode = model.employeeCode,
                    BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                    DesignationName = model.Designation == null ? "" : model.Designation.designationName,
                    EmployeeName = model.employeeName,
                    ResidentiallAddress = model.residentiallAddress,
                    NativeAddress = model.nativeAddress,
                    MobileNumber = model.mobileNumber,
                    ResidentialTelephone = model.residentialTelephone,
                    JoiningDate = model.joiningDate,
                    LeavingDate = model.leavingDate,
                    BasicAmount = model.basicAmount,
                    HraAmount = model.hraAmount,
                    CcAmount = model.ccAmount,
                    Bank = model.bank ?? false,
                    AccountNumber = model.accountNumber
                };

                return View(vm);
            }

            return RedirectToAction("EmployeeList");
        }

        [HttpPost]
        [ActionName("EmployeeDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeDeleteConfirmed(string key, EmployeeViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Employees.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("EmployeeList");
            }

            return View(vm);
        }

        private void BindDropdown_Employee(EmployeeViewModel vm)
        {
            vm.BranchList = new SelectList(db.CompanyBranches.Where(m => m.active == true), "branchId", "branchName");
            vm.DesignationList = new SelectList(db.Designations.Where(m => m.active == true), "designationId", "designationName");
        }

        #endregion

        #region Party Master

        [HttpGet]
        public async Task<ActionResult> PartyList()
        {
            var list = new List<PartyViewModel>();

            var model = await db.Parties.Include(e => e.CompanyBranch).Include(e => e.PartyStatu).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new PartyViewModel
                    {
                        PartyId = item.partyId,
                        BranchId = item.branchId,
                        BranchName = item.CompanyBranch == null ? "" : item.CompanyBranch.branchName,
                        CompanyId = item.CompanyBranch?.companyId,
                        CompanyName = item.CompanyBranch.Company == null ? "" : item.CompanyBranch.Company.companyName,
                        StatusCode = item.status,
                        StatusDescription = item.PartyStatu.statusDescription,
                        Name = item.Name,
                        BillName = item.billName,
                        IdName = item.idName,
                        Address1 = item.address1,
                        City1 = item.city1,
                        Pincode1 = item.pincode1,
                        Address2 = item.address2,
                        City2 = item.city2,
                        Pincode2 = item.pincode2,
                        Contact1 = item.contact1,
                        Contact2 = item.contact2,
                        FaxNumber = item.faxNumber,
                        EmailId = item.emailId,
                        PrimaryGroupId = item.primaryGroupId,
                        PrimaryGroupName = db.Parties.FirstOrDefault(m => m.partyId == item.primaryGroupId) == null ? ""
                                            : db.Parties.FirstOrDefault(m => m.partyId == item.primaryGroupId).Name,
                        DiscountAllowed = item.discountAllowed,
                        DutySlipFormat = item.dutySlipFormat,
                        VendorCode = item.vendorCode,
                        CostCenter = item.costCenter,
                        TdsPercent = item.tdsPercent,
                        CommissionPercent = item.commissionPercent,
                        GstNumber = item.gstNumber
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> PartyDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Parties.FindAsync(_Id);

                var vm = new PartyViewModel
                {
                    PartyId = model.partyId,
                    BranchId = model.branchId,
                    BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                    CompanyId = model.CompanyBranch?.companyId,
                    CompanyName = model.CompanyBranch.Company == null ? "" : model.CompanyBranch.Company.companyName,
                    StatusCode = model.status,
                    StatusDescription = model.PartyStatu.statusDescription,
                    Name = model.Name,
                    BillName = model.billName,
                    IdName = model.idName,
                    Address1 = model.address1,
                    City1 = model.city1,
                    Pincode1 = model.pincode1,
                    Address2 = model.address2,
                    City2 = model.city2,
                    Pincode2 = model.pincode2,
                    Contact1 = model.contact1,
                    Contact2 = model.contact2,
                    FaxNumber = model.faxNumber,
                    EmailId = model.emailId,
                    PrimaryGroupId = model.primaryGroupId,
                    PrimaryGroupName = db.Parties.FirstOrDefault(m => m.partyId == model.primaryGroupId) == null ? ""
                                            : db.Parties.FirstOrDefault(m => m.partyId == model.primaryGroupId).Name,
                    DiscountAllowed = model.discountAllowed,
                    DutySlipFormat = model.dutySlipFormat,
                    VendorCode = model.vendorCode,
                    CostCenter = model.costCenter,
                    TdsPercent = model.tdsPercent,
                    CommissionPercent = model.commissionPercent,
                    GstNumber = model.gstNumber
                };

                return View(vm);
            }

            return RedirectToAction("PartyList");
        }

        [HttpGet]
        public async Task<ActionResult> PartyManage(string key)
        {
            var vm = new PartyViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Parties.FindAsync(_Id);

                    vm.PartyId = model.partyId;
                    vm.BranchId = model.branchId;
                    vm.StatusCode = model.status;
                    vm.Name = model.Name;
                    vm.BillName = model.billName;
                    vm.IdName = model.idName;
                    vm.Address1 = model.address1;
                    vm.City1 = model.city1;
                    vm.Pincode1 = model.pincode1;
                    vm.Address2 = model.address2;
                    vm.City2 = model.city2;
                    vm.Pincode2 = model.pincode2;
                    vm.Contact1 = model.contact1;
                    vm.Contact2 = model.contact2;
                    vm.FaxNumber = model.faxNumber;
                    vm.EmailId = model.emailId;
                    vm.PrimaryGroupId = model.primaryGroupId;
                    vm.PrimaryGroupName = db.Parties.FirstOrDefault(m => m.partyId == model.primaryGroupId).Name;
                    vm.DiscountAllowed = model.discountAllowed;
                    vm.DutySlipFormat = model.dutySlipFormat;
                    vm.VendorCode = model.vendorCode;
                    vm.CostCenter = model.costCenter;
                    vm.TdsPercent = model.tdsPercent;
                    vm.CommissionPercent = model.commissionPercent;
                    vm.GstNumber = model.gstNumber;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Party(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> PartyManage(PartyViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Party();

                    if (vm.PartyId == null)
                    {
                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Parties.Add(model);
                    }
                    else
                    {
                        model = await db.Parties.FindAsync(vm.PartyId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.branchId = vm.BranchId;
                    model.status = vm.StatusCode;
                    model.Name = vm.Name;
                    model.billName = vm.BillName;
                    model.idName = vm.IdName;
                    model.address1 = vm.Address1;
                    model.city1 = vm.City1;
                    model.pincode1 = vm.Pincode1;
                    model.address2 = vm.Address2;
                    model.city2 = vm.City2;
                    model.pincode2 = vm.Pincode2;
                    model.contact1 = vm.Contact1;
                    model.contact2 = vm.Contact2;
                    model.faxNumber = vm.FaxNumber;
                    model.emailId = vm.EmailId;
                    model.primaryGroupId = vm.PrimaryGroupId;
                    model.discountAllowed = vm.DiscountAllowed;
                    model.dutySlipFormat = vm.DutySlipFormat;
                    model.vendorCode = vm.VendorCode;
                    model.costCenter = vm.CostCenter;
                    model.tdsPercent = vm.TdsPercent;
                    model.commissionPercent = vm.CommissionPercent;
                    model.gstNumber = vm.GstNumber;

                    await db.SaveChangesAsync();

                    return RedirectToAction("PartyList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Party(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> PartyDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Parties.FindAsync(_Id);

                var vm = new PartyViewModel
                {
                    PartyId = model.partyId,
                    BranchId = model.branchId,
                    BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                    CompanyId = model.CompanyBranch?.companyId,
                    CompanyName = model.CompanyBranch.Company == null ? "" : model.CompanyBranch.Company.companyName,
                    StatusCode = model.status,
                    StatusDescription = model.PartyStatu.statusDescription,
                    Name = model.Name,
                    BillName = model.billName,
                    IdName = model.idName,
                    Address1 = model.address1,
                    City1 = model.city1,
                    Pincode1 = model.pincode1,
                    Address2 = model.address2,
                    City2 = model.city2,
                    Pincode2 = model.pincode2,
                    Contact1 = model.contact1,
                    Contact2 = model.contact2,
                    FaxNumber = model.faxNumber,
                    EmailId = model.emailId,
                    PrimaryGroupId = model.primaryGroupId,
                    PrimaryGroupName = db.Parties.FirstOrDefault(m => m.partyId == model.primaryGroupId) == null ? ""
                                            : db.Parties.FirstOrDefault(m => m.partyId == model.primaryGroupId).Name,
                    DiscountAllowed = model.discountAllowed,
                    DutySlipFormat = model.dutySlipFormat,
                    VendorCode = model.vendorCode,
                    CostCenter = model.costCenter,
                    TdsPercent = model.tdsPercent,
                    CommissionPercent = model.commissionPercent,
                    GstNumber = model.gstNumber
                };

                return View(vm);
            }

            return RedirectToAction("PartyList");
        }

        [HttpPost]
        [ActionName("PartyDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PartyDeleteConfirmed(string key, PartyViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Parties.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("PartyList");
            }

            return View(vm);
        }

        private void BindDropdown_Party(PartyViewModel vm)
        {
            vm.BranchList = new SelectList(db.CompanyBranches.Where(m => m.active == true), "branchId", "branchName");
            vm.StatusList = new SelectList(db.PartyStatus, "statusCode", "statusDescription");
            vm.PrimaryGroupList = new SelectList(db.Parties.Where(m => m.active == true), "partyId", "Name");
        }

        #endregion

        #region Designation Master

        [HttpGet]
        public async Task<ActionResult> DesignationList()
        {
            var list = new List<DesignationViewModel>();

            var model = await db.Designations.ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new DesignationViewModel
                    {
                        DesignationId = item.designationId,
                        DesignationName = item.designationName
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> DesignationDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Designations.FindAsync(_Id);

                var vm = new DesignationViewModel
                {
                    DesignationId = model.designationId,
                    DesignationName = model.designationName,
                };

                return View(vm);
            }

            return RedirectToAction("DesignationList");
        }

        [HttpGet]
        public async Task<ActionResult> DesignationManage(string key)
        {
            var vm = new DesignationViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Designations.FindAsync(_Id);

                    vm.DesignationId = model.designationId;
                    vm.DesignationName = model.designationName;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DesignationManage(DesignationViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Designation();

                    if (vm.DesignationId == null)
                    {
                        if (db.Designations.Count(m => m.designationName == vm.DesignationName) > 0)
                        {
                            ModelState.AddModelError("", "Designation Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Designations.Add(model);
                    }
                    else
                    {
                        model = await db.Designations.FindAsync(vm.DesignationId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.designationName = vm.DesignationName;

                    await db.SaveChangesAsync();

                    return RedirectToAction("DesignationList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> DesignationDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Designations.FindAsync(_Id);

                var vm = new DesignationViewModel
                {
                    DesignationId = model.designationId,
                    DesignationName = model.designationName,
                };

                return View(vm);
            }

            return RedirectToAction("DesignationList");
        }

        [HttpPost]
        [ActionName("DesignationDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DesignationDeleteConfirmed(string key, DesignationViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Designations.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("DesignationList");
            }

            return View(vm);
        }

        #endregion

        #region Car Model

        [HttpGet]
        public async Task<ActionResult> CarModelList()
        {
            var list = new List<CarModelViewModel>();

            var model = await db.CarModels.ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new CarModelViewModel
                    {
                        CarModelId = item.carModelId,
                        ModelDescription = item.modelDescription,
                        SeatingCapacity = item.seatingCapacity
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> CarModelDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.CarModels.FindAsync(_Id);

                var vm = new CarModelViewModel
                {
                    CarModelId = model.carModelId,
                    ModelDescription = model.modelDescription,
                    SeatingCapacity = model.seatingCapacity
                };

                return View(vm);
            }

            return RedirectToAction("CarModelList");
        }

        [HttpGet]
        public async Task<ActionResult> CarModelManage(string key)
        {
            var vm = new CarModelViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.CarModels.FindAsync(_Id);

                    vm.CarModelId = model.carModelId;
                    vm.ModelDescription = model.modelDescription;
                    vm.SeatingCapacity = model.seatingCapacity;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CarModelManage(CarModelViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new CarModel();

                    if (vm.CarModelId == null)
                    {
                        if (db.CarModels.Count(m => m.modelDescription == vm.ModelDescription) > 0)
                        {
                            ModelState.AddModelError("", "Model Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.CarModels.Add(model);
                    }
                    else
                    {
                        model = await db.CarModels.FindAsync(vm.CarModelId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.modelDescription = vm.ModelDescription;
                    model.seatingCapacity = vm.SeatingCapacity;

                    await db.SaveChangesAsync();

                    return RedirectToAction("CarModelList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> CarModelDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.CarModels.FindAsync(_Id);

                var vm = new CarModelViewModel
                {
                    CarModelId = model.carModelId,
                    ModelDescription = model.modelDescription,
                    SeatingCapacity = model.seatingCapacity
                };

                return View(vm);
            }

            return RedirectToAction("CarModelList");
        }

        [HttpPost]
        [ActionName("CarModelDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CarModelDeleteConfirmed(string key, CarModelViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.CarModels.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("CarModelList");
            }

            return View(vm);
        }

        #endregion

        #region Guest Master

        [HttpGet]
        public async Task<ActionResult> GuestList()
        {
            var list = new List<GuestViewModel>();

            var model = await db.Guests.Include(e => e.CompanyBranch)
                                       .Include(e => e.CarModel)
                                       .Include(e => e.Party)
                                       .Include(e => e.Department)
                                       .ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new GuestViewModel
                    {
                        GuestId = item.guestId,
                        BranchId = item.branchId,
                        BranchName = item.CompanyBranch == null ? "" : item.CompanyBranch.branchName,
                        CarModelId = item.carModelId,
                        ModelName = item.CarModel == null ? "" : item.CarModel.modelDescription,
                        PartyId = item.partyId,
                        PartyName = item.Party == null ? "" : item.Party.Name,
                        DepartmentId = item.departmentId,
                        DepartmentName = item.Department == null ? "" : item.Department.departmentName,
                        GuestName = item.guestName,
                        BookedBy = item.bookedBy,
                        ContactNumber1 = item.contactNumber1,
                        ContactNumber2 = item.contactNumber2,
                        ContactNumber3 = item.contactNumber3
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> GuestDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Guests.FindAsync(_Id);

                var vm = new GuestViewModel
                {
                    GuestId = model.guestId,
                    BranchId = model.branchId,
                    BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                    CarModelId = model.carModelId,
                    ModelName = model.CarModel == null ? "" : model.CarModel.modelDescription,
                    PartyId = model.partyId,
                    PartyName = model.Party == null ? "" : model.Party.Name,
                    DepartmentId = model.departmentId,
                    DepartmentName = model.Department == null ? "" : model.Department.departmentName,
                    GuestName = model.guestName,
                    BookedBy = model.bookedBy,
                    ContactNumber1 = model.contactNumber1,
                    ContactNumber2 = model.contactNumber2,
                    ContactNumber3 = model.contactNumber3
                };

                return View(vm);
            }

            return RedirectToAction("GuestList");
        }

        [HttpGet]
        public async Task<ActionResult> GuestManage(string key)
        {
            var vm = new GuestViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Guests.FindAsync(_Id);

                    vm.GuestId = model.guestId;
                    vm.BranchId = model.branchId;
                    vm.CarModelId = model.carModelId;
                    vm.PartyId = model.partyId;
                    vm.DepartmentId = model.departmentId;
                    vm.GuestName = model.guestName;
                    vm.BookedBy = model.bookedBy;
                    vm.ContactNumber1 = model.contactNumber1;
                    vm.ContactNumber2 = model.contactNumber2;
                    vm.ContactNumber3 = model.contactNumber3;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Guest(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> GuestManage(GuestViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Guest();

                    if (vm.GuestId == null)
                    {
                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Guests.Add(model);
                    }
                    else
                    {
                        model = await db.Guests.FindAsync(vm.GuestId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.branchId = (int)vm.BranchId;
                    model.carModelId = vm.CarModelId;
                    model.partyId = (int)vm.PartyId;
                    model.departmentId = vm.DepartmentId;
                    model.guestName = vm.GuestName;
                    model.bookedBy = vm.BookedBy;
                    model.contactNumber1 = vm.ContactNumber1;
                    model.contactNumber2 = vm.ContactNumber2;
                    model.contactNumber3 = vm.ContactNumber3;

                    await db.SaveChangesAsync();

                    return RedirectToAction("GuestList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Guest(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> GuestDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Guests.FindAsync(_Id);

                var vm = new GuestViewModel
                {
                    GuestId = model.guestId,
                    BranchId = model.branchId,
                    BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                    CarModelId = model.carModelId,
                    ModelName = model.CarModel == null ? "" : model.CarModel.modelDescription,
                    PartyId = model.partyId,
                    PartyName = model.Party == null ? "" : model.Party.Name,
                    DepartmentId = model.departmentId,
                    DepartmentName = model.Department == null ? "" : model.Department.departmentName,
                    GuestName = model.guestName,
                    BookedBy = model.bookedBy,
                    ContactNumber1 = model.contactNumber1,
                    ContactNumber2 = model.contactNumber2,
                    ContactNumber3 = model.contactNumber3
                };

                return View(vm);
            }

            return RedirectToAction("GuestList");
        }

        [HttpPost]
        [ActionName("GuestDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuestDeleteConfirmed(string key, GuestViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Guests.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("GuestList");
            }

            return View(vm);
        }

        private void BindDropdown_Guest(GuestViewModel vm)
        {
            vm.BranchList = new SelectList(db.CompanyBranches.Where(m => m.active == true), "branchId", "branchName");
            vm.PartyList = new SelectList(db.Parties.Where(m => m.active == true), "partyId", "Name");
            vm.DepartmentList = new SelectList(db.Departments.Where(m => m.active == true), "departmentId", "departmentName");
            vm.ModelList = new SelectList(db.CarModels.Where(m => m.active == true), "carModelId", "modelDescription");
        }

        #endregion

    }
}