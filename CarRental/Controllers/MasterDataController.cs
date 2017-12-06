using CarRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var model = await db.Companies.Where(m => m.active == true).ToListAsync();

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

            var model = await db.CompanyBranches.Include(e => e.Company).Where(m => m.active == true).ToListAsync();

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
            vm.CompanyList = ApplicationParameter.GetCompanies(db);
        }

        #endregion

        #region Department Master

        [HttpGet]
        public async Task<ActionResult> DepartmentList()
        {
            var list = new List<DepartmentViewModel>();

            var model = await db.Departments.Where(m => m.active == true).ToListAsync();

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

            var model = await db.Employees.Include(e => e.Designation).Include(e => e.CompanyBranch).Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetEmployee(item));
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

                return View(GetEmployee(model));
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

                    vm = GetEmployee(model);
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
                    model.accountNumber = vm.AccountNumber;
                    model.bloodGroup = vm.BloodGroup;
                    model.licenseNumber = vm.LicenseNumber;
                    model.otRatePerHour = vm.OtRatePerHour;
                    model.outstation150 = vm.Outstation150;
                    model.outstation100 = vm.Outstation100;
                    model.pfAmount = vm.PfAmount;
                    model.extraDuty = vm.ExtraDuty;
                    model.sundayAmount = vm.SundayAmount;

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

                return View(GetEmployee(model));
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

        private EmployeeViewModel GetEmployee(Employee model)
        {
            return new EmployeeViewModel
            {
                BranchId = model.branchId,
                EmployeeId = model.employeeId,
                EmployeeCode = model.employeeCode,
                BranchName = model.CompanyBranch == null ? "" : model.CompanyBranch.branchName,
                DesignationId = model.designationId,
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
                AccountNumber = model.accountNumber,
                BloodGroup = model.bloodGroup,
                LicenseNumber = model.licenseNumber,
                OtRatePerHour = model.otRatePerHour,
                Outstation100 = model.outstation100,
                Outstation150 = model.outstation150,
                PfAmount = model.pfAmount,
                ExtraDuty = model.extraDuty,
                SundayAmount = model.sundayAmount
            };
        }

        private void BindDropdown_Employee(EmployeeViewModel vm)
        {
            vm.BranchList = ApplicationParameter.GetBranches(db);
            vm.DesignationList = ApplicationParameter.GetDesignations(db);
        }

        #endregion

        #region Party Master

        [HttpGet]
        public async Task<ActionResult> PartyList()
        {
            var list = new List<PartyViewModel>();

            var model = await db.Parties.Include(e => e.CompanyBranch).Include(e => e.PartyStatu).Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetParty(item));
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

                return View(GetParty(model));
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

                    vm = GetParty(model);
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

                return View(GetParty(model));
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

        private PartyViewModel GetParty(Party model)
        {
            return new PartyViewModel
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
        }

        private void BindDropdown_Party(PartyViewModel vm)
        {
            vm.BranchList = ApplicationParameter.GetBranches(db);
            vm.StatusList = ApplicationParameter.GetStatus(db);
            vm.PrimaryGroupList = ApplicationParameter.GetParties(db);
        }

        #endregion

        #region Designation Master

        [HttpGet]
        public async Task<ActionResult> DesignationList()
        {
            var list = new List<DesignationViewModel>();

            var model = await db.Designations.Where(m => m.active == true).ToListAsync();

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

            var model = await db.CarModels.Where(m => m.active == true).ToListAsync();

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
                                       .Where(m => m.active == true)
                                       .ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetGuest(item));
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

                return View(GetGuest(model));
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

                    vm = GetGuest(model);
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
                    model.guestMobile = vm.GuestMobile;
                    model.bookedByMobile = vm.BookedByMobile;
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

                return View(GetGuest(model));
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
            vm.BranchList = ApplicationParameter.GetBranches(db);
            vm.PartyList = ApplicationParameter.GetParties(db);
            vm.DepartmentList = ApplicationParameter.GetDepartments(db);
            vm.ModelList = ApplicationParameter.GetCarModels(db);
        }

        private GuestViewModel GetGuest(Guest model)
        {
            return new GuestViewModel
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
                GuestMobile = model.guestMobile,
                BookedByMobile = model.bookedByMobile,
                ContactNumber1 = model.contactNumber1,
                ContactNumber2 = model.contactNumber2,
                ContactNumber3 = model.contactNumber3
            };
        }

        #endregion

        #region Car

        [HttpGet]
        public async Task<ActionResult> CarList()
        {
            var list = new List<CarViewModel>();

            var model = await db.Cars.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetCar(item));
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> CarDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Cars.FindAsync(_Id);
                
                return View(GetCar(model));
            }

            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<ActionResult> CarManage(string key)
        {
            var vm = new CarViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Cars.FindAsync(_Id);

                    vm = GetCar(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Car(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CarManage(CarViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Car();

                    if (vm.CarId == null)
                    {
                        if (db.Cars.Count(m => m.registrationNumber == vm.RegistrationNumber) > 0)
                        {
                            ModelState.AddModelError("", "Registration Number already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Cars.Add(model);
                    }
                    else
                    {
                        model = await db.Cars.FindAsync(vm.CarId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.branchId = vm.BranchId;
                    model.carType = vm.CarType;
                    model.carModelId = vm.CarModelId;
                    model.fuelTypeCode = vm.FuelTypeCode;
                    model.carNumber = vm.CarNumber;
                    model.driverId = vm.DriverId;
                    model.registrationNumber = vm.RegistrationNumber;
                    model.chasisNumber = vm.ChasisNumber;
                    model.engineNumber = vm.EngineNumber;
                    model.insuranceCompany = vm.InsuranceCompany;
                    model.insurancePolicyNo = vm.InsurancePolicyNo;
                    model.insuranceStartDate = vm.InsuranceStartDate;
                    model.insuranceEndDate = vm.InsuranceEndDate;
                    model.taxStartDate = vm.TaxStartDate;
                    model.taxEndDate = vm.TaxEndDate;
                    model.authorisationStartDate = vm.AuthorisationStartDate;
                    model.authorisationEndDate = vm.AuthorisationEndDate;
                    model.fitnessStartDate = vm.FitnessStartDate;
                    model.fitnessEndDate = vm.FitnessEndDate;
                    model.permitStartDate = vm.PermitStartDate;
                    model.permitEndDate = vm.PermitEndDate;
                    model.permitStartDate = vm.PucStartDate;
                    model.pucEndDate = vm.PucEndDate;
                    model.financeBy = vm.FinanceBy;
                    model.ownerId = vm.OwnerId;
                    model.carInUse = vm.CarInUse;
                    model.carOnHold = vm.CarOnHold;
                    model.servicingSlab = vm.ServicingSlab;
                    model.emiAmount = vm.EmiAmount;
                    model.emiDate = vm.EmiDate;

                    await db.SaveChangesAsync();

                    return RedirectToAction("CarList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Car(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> CarDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Cars.FindAsync(_Id);
                
                return View(GetCar(model));
            }

            return RedirectToAction("CarList");
        }

        [HttpPost]
        [ActionName("CarDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CarDeleteConfirmed(string key, CarViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Cars.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("CarList");
            }

            return View(vm);
        }

        private CarViewModel GetCar(Car model)
        {
            return new CarViewModel
            {
                CarId = model.CarId,
                BranchId = model.branchId,
                BranchName = model.CompanyBranch?.branchName,
                CarType = model.carType,
                CarTypeName = model.carType == "O" ? "Owner" : "Fixed",
                CarModelId = model.carModelId,
                ModelName = model.CarModel?.modelDescription,
                FuelTypeCode = model.fuelTypeCode,
                FuelTypeName = model.FuelType?.fuelTypeDescription,
                CarNumber = model.carNumber,
                DriverId = model.driverId,
                DriverName = model.Employee?.employeeName,
                RegistrationNumber = model.registrationNumber,
                ChasisNumber = model.chasisNumber,
                EngineNumber = model.engineNumber,
                InsuranceCompany = model.insuranceCompany,
                InsurancePolicyNo = model.insurancePolicyNo,
                InsuranceStartDate = model.insuranceStartDate,
                InsuranceEndDate = model.insuranceEndDate,
                TaxStartDate = model.taxStartDate,
                TaxEndDate = model.taxEndDate,
                AuthorisationStartDate = model.authorisationStartDate,
                AuthorisationEndDate = model.authorisationEndDate,
                FitnessStartDate = model.fitnessStartDate,
                FitnessEndDate = model.fitnessEndDate,
                PermitStartDate = model.permitStartDate,
                PermitEndDate = model.permitEndDate,
                PucStartDate = model.permitStartDate,
                PucEndDate = model.pucEndDate,
                FinanceBy = model.financeBy,
                OwnerId = model.ownerId,
                OwnerName = model.Party?.Name,
                CarInUse = model.carInUse,
                CarOnHold = model.carOnHold,
                ServicingSlab = model.servicingSlab,
                EmiAmount = model.emiAmount,
                EmiDate = model.emiDate
            };
        }

        private void BindDropdown_Car(CarViewModel vm)
        {
            vm.FuelTypeList = ApplicationParameter.GetFuelTypes(db);
            vm.BranchList = ApplicationParameter.GetBranches(db);
            vm.OwnerList = ApplicationParameter.GetOwners(db);
            vm.ModelList = ApplicationParameter.GetCarModels(db);
            vm.DriverList = ApplicationParameter.GetDrivers(db);
        }

        #endregion

        #region Scheme Master

        [HttpGet]
        public async Task<ActionResult> SchemeList()
        {
            var list = new List<SchemeViewModel>();

            var model = await db.Schemes.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetScheme(item));
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> SchemeDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Schemes.FindAsync(_Id);

                return View(GetScheme(model));
            }

            return RedirectToAction("SchemeList");
        }

        [HttpGet]
        public async Task<ActionResult> SchemeManage(string key)
        {
            var vm = new SchemeViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Schemes.FindAsync(_Id);

                    vm = GetScheme(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> SchemeManage(SchemeViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Scheme();

                    if (vm.SchemeId == null)
                    {
                        if (db.Schemes.Count(m => m.schemeName == vm.SchemeName) > 0)
                        {
                            ModelState.AddModelError("", "Scheme Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Schemes.Add(model);
                    }
                    else
                    {
                        model = await db.Schemes.FindAsync(vm.SchemeId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.schemeName = vm.SchemeName;
                    model.minimumHours = vm.MinimumHours;
                    model.minimumKilometer = vm.MinimumKilometer;

                    await db.SaveChangesAsync();

                    return RedirectToAction("SchemeList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> SchemeDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Schemes.FindAsync(_Id);

                return View(GetScheme(model));
            }

            return RedirectToAction("SchemeList");
        }

        [HttpPost]
        [ActionName("SchemeDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SchemeDeleteConfirmed(string key, SchemeViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Schemes.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("SchemeList");
            }

            return View(vm);
        }

        private SchemeViewModel GetScheme(Scheme model)
        {
            return new SchemeViewModel
            {
                SchemeId = model.schemeId,
                SchemeName = model.schemeName,
                MinimumHours = model.minimumHours,
                MinimumKilometer = model.minimumKilometer
            };
        }

        #endregion

        #region RateCard Master

        [HttpGet]
        public async Task<ActionResult> RateCardList()
        {
            var list = new List<RateCardViewModel>();

            var model = await db.RateCards.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetRateCard(item));
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> RateCardDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.RateCards.FindAsync(_Id);

                return View(GetRateCard(model));
            }

            return RedirectToAction("RateCardList");
        }

        [HttpGet]
        public async Task<ActionResult> RateCardManage(string key)
        {
            var vm = new RateCardViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.RateCards.FindAsync(_Id);

                    vm = GetRateCard(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_RateCard(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> RateCardManage(RateCardViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new RateCard();

                    if (vm.RateId == null)
                    {
                        if (db.RateCards.Count(m => m.efftectiveDate == vm.EfftectiveDate) > 0)
                        {
                            ModelState.AddModelError("", "Rate is already defined for effective date :" + vm.EfftectiveDate);
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.RateCards.Add(model);
                    }
                    else
                    {
                        model = await db.RateCards.FindAsync(vm.RateId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.efftectiveDate = (DateTime)vm.EfftectiveDate;
                    model.partyId = vm.PartyId;
                    model.carModelId = vm.CarModelId;
                    model.dutyTypeId = vm.DutyTypeId;
                    model.schemeId = vm.SchemeId;
                    model.minHours = vm.MinHours;
                    model.minKm = vm.MinKm;
                    model.rateAmount = vm.RateAmount;
                    model.extraHours = vm.ExtraHours;
                    model.extraKM = vm.ExtraKM;
                    model.nightAllowance = vm.NightAllowance;
                    model.dayAllowance = vm.DayAllowance;

                    await db.SaveChangesAsync();

                    return RedirectToAction("RateCardList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_RateCard(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> RateCardDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.RateCards.FindAsync(_Id);

                return View(GetRateCard(model));
            }

            return RedirectToAction("RateCardList");
        }

        [HttpPost]
        [ActionName("RateCardDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RateCardDeleteConfirmed(string key, RateCardViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.RateCards.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("RateCardList");
            }

            return View(vm);
        }

        private RateCardViewModel GetRateCard(RateCard model)
        {
            return new RateCardViewModel
            {
                EfftectiveDate = model.efftectiveDate,
                RateId = model.rateId,
                PartyId = model.partyId,
                PartyName = model.Party == null ? string.Empty : model.Party.Name,
                CarModelId = model.carModelId,
                CarModelName = model.CarModel == null ? string.Empty : model.CarModel.modelDescription,
                DutyTypeId = model.dutyTypeId,
                DutyTypeName = model.DutyType == null ? string.Empty : model.DutyType.dutyDescription,
                SchemeId = model.schemeId,
                SchemeName = model.Scheme == null ? string.Empty : model.Scheme.schemeName,
                MinHours = model.minHours,
                MinKm = model.minKm,
                RateAmount = model.rateAmount,
                ExtraHours = model.extraHours,
                ExtraKM = model.extraKM,
                NightAllowance = model.nightAllowance,
                DayAllowance = model.dayAllowance
            };
        }

        private void BindDropdown_RateCard(RateCardViewModel vm)
        {
            vm.PartyList = ApplicationParameter.GetParties(db);
            vm.CarModelList = ApplicationParameter.GetCarModels(db);
            vm.DutyTypeList = ApplicationParameter.GetDutyTypes(db);
            vm.SchemeList = ApplicationParameter.GetSchemes(db);
        }

        #endregion

        #region Expense Master

        [HttpGet]
        public async Task<ActionResult> ExpenseList()
        {
            var list = new List<ExpenseViewModel>();

            var model = await db.ExpenseHeads.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetExpense(item));
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> ExpenseDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.ExpenseHeads.FindAsync(_Id);

                return View(GetExpense(model));
            }

            return RedirectToAction("ExpenseList");
        }

        [HttpGet]
        public async Task<ActionResult> ExpenseManage(string key)
        {
            var vm = new ExpenseViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.ExpenseHeads.FindAsync(_Id);

                    vm = GetExpense(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpenseManage(ExpenseViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new ExpenseHead();

                    if (vm.ExpenseId == null)
                    {
                        if (db.ExpenseHeads.Count(m => m.expenseName == vm.ExpenseName) > 0)
                        {
                            ModelState.AddModelError("", "Expense Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.ExpenseHeads.Add(model);
                    }
                    else
                    {
                        model = await db.ExpenseHeads.FindAsync(vm.ExpenseId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.expenseName = vm.ExpenseName;

                    await db.SaveChangesAsync();

                    return RedirectToAction("ExpenseList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> ExpenseDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.ExpenseHeads.FindAsync(_Id);

                return View(GetExpense(model));
            }

            return RedirectToAction("ExpenseList");
        }

        [HttpPost]
        [ActionName("ExpenseDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpenseDeleteConfirmed(string key, ExpenseViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.ExpenseHeads.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("ExpenseList");
            }

            return View(vm);
        }

        private ExpenseViewModel GetExpense(ExpenseHead model)
        {
            return new ExpenseViewModel
            {
                ExpenseId = model.expenseId,
                ExpenseName = model.expenseName
            };
        }

        #endregion

        #region ReleasePoint Master

        [HttpGet]
        public async Task<ActionResult> ReleasePointList()
        {
            var list = new List<ReleasePointViewModel>();

            var model = await db.ReleasePoints.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetReleasePoint(item));
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> ReleasePointDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.ReleasePoints.FindAsync(_Id);

                return View(GetReleasePoint(model));
            }

            return RedirectToAction("ReleasePointList");
        }

        [HttpGet]
        public async Task<ActionResult> ReleasePointManage(string key)
        {
            var vm = new ReleasePointViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.ReleasePoints.FindAsync(_Id);

                    vm = GetReleasePoint(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ReleasePointManage(ReleasePointViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new ReleasePoint();

                    if (vm.ReleasePointId == null)
                    {
                        if (db.ReleasePoints.Count(m => m.releasePointName == vm.ReleasePointName) > 0)
                        {
                            ModelState.AddModelError("", "ReleasePoint Name already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.ReleasePoints.Add(model);
                    }
                    else
                    {
                        model = await db.ReleasePoints.FindAsync(vm.ReleasePointId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.releasePointName = vm.ReleasePointName;

                    await db.SaveChangesAsync();

                    return RedirectToAction("ReleasePointList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> ReleasePointDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.ReleasePoints.FindAsync(_Id);

                return View(GetReleasePoint(model));
            }

            return RedirectToAction("ReleasePointList");
        }

        [HttpPost]
        [ActionName("ReleasePointDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReleasePointDeleteConfirmed(string key, ReleasePointViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.ReleasePoints.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("ReleasePointList");
            }

            return View(vm);
        }

        private ReleasePointViewModel GetReleasePoint(ReleasePoint model)
        {
            return new ReleasePointViewModel
            {
                ReleasePointId = model.releasePointId,
                ReleasePointName = model.releasePointName
            };
        }

        #endregion

    }
}