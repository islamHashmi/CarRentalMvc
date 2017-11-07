using CarRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarRental.Models;
using System.Web.Security;
using CarRental.Helpers;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CarRental.Controllers
{
    public class AccountController : Controller
    {
        CarRentalEntities db = new CarRentalEntities();

        public ActionResult Index()
        {
            return View();
        }

        #region Login

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.IsAuthorized = false;

            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vm)
        {
            var user = db.Logins.FirstOrDefault(m => m.loginId == vm.LoginId && m.loginKey == vm.LoginKey);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(vm.LoginId, false);

                ViewBag.IsAuthorized = true;

                return View("_SignInOtpPartial", vm);
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
                ViewBag.LoggedIn = false;
            }

            return View("_SignInPartial", vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Authorize_User(LoginViewModel vm)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            List<string> _message = new List<string>();
            
            data.Add("IsAuthorize", "false");
            data.Add("OtpRequired", "false");
            data.Add("ErrorMessage", "");
            
            if (string.IsNullOrEmpty(vm.LoginId))
               _message.Add(string.Format("{0}", "Login Id is required field."));

            if (string.IsNullOrEmpty(vm.LoginKey))
                _message.Add(string.Format("{0}", "Password is required field."));

            if (_message.Count <= 0)
            {
                var user = db.Logins.FirstOrDefault(m => m.loginId == vm.LoginId && m.loginKey == vm.LoginKey);

                if (user != null)
                {
                    data["IsAuthorize"] = "true";                    
                    data["OtpRequired"] = user.otpRequired.ToString().ToLower();
                }
                else
                {
                    _message.Add(string.Format("{0}", "Invalid Login Id or Password."));
                }
            }
            
            data["ErrorMessage"] = _message;
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Authorize_Otp(string key)
        {
            key = HelperClass.Decrypt(key);

            string otp = GenerateOTP.OtpNumber(6, GenerateOTP.OtpType.Numeric);


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Register

        [HttpGet]
        public async Task<ActionResult> RegisterList()
        {
            var list = new List<RegisterViewModel>();

            var model = await db.Logins.ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    var vm = new RegisterViewModel
                    {
                        UserId = item.userId,
                        EmployeeId = item.employeeId,
                        EmployeeName = item.Employee == null ? "" : item.Employee.employeeName,
                        LoginId = item.loginId,
                        LastLogin = item.lastLoginDate,
                        OtpRequired = item.otpRequired
                    };

                    list.Add(vm);
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> RegisterDetails(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Logins.FindAsync(_Id);

                var vm = new RegisterViewModel
                {
                    UserId = model.userId,
                    EmployeeId = model.employeeId,
                    EmployeeName = model.Employee == null ? "" : model.Employee.employeeName,
                    LoginId = model.loginId,
                    LastLogin = model.lastLoginDate,
                    OtpRequired = model.otpRequired
                };

                return View(vm);
            }

            return RedirectToAction("CompanyList");
        }

        [HttpGet]
        public async Task<ActionResult> RegisterManage(string key)
        {
            var vm = new RegisterViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Logins.FindAsync(_Id);

                    if (model == null)
                        return HttpNotFound();

                    vm.UserId = model.userId;
                    vm.EmployeeId = model.employeeId;
                    vm.LoginId = model.loginId;
                    vm.LoginKey = model.loginKey;
                    vm.ConfirmLoginKey = model.loginKey;
                    vm.OtpRequired = model.otpRequired;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Register(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterManage(RegisterViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new Login();

                    if (vm.UserId == null)
                    {
                        if (db.Logins.Count(m => m.loginId == vm.LoginId) > 0)
                        {
                            ModelState.AddModelError("", "Login Id already exists in database.");
                            return View(vm);
                        }

                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.Logins.Add(model);
                    }
                    else
                    {
                        model = await db.Logins.FindAsync(vm.UserId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.employeeId = vm.EmployeeId;
                    model.loginId = vm.LoginId;
                    model.loginKey = vm.LoginKey;
                    model.otpRequired = vm.OtpRequired;

                    await db.SaveChangesAsync();

                    return RedirectToAction("RegisterList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Register(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> RegisterDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Logins.FindAsync(_Id);

                var vm = new RegisterViewModel
                {
                    UserId = model.userId,
                    EmployeeId = model.employeeId,
                    EmployeeName = model.Employee == null ? "" : model.Employee.employeeName,
                    LoginId = model.loginId,
                    LastLogin = model.lastLoginDate,
                    OtpRequired = model.otpRequired
                };

                return View(vm);
            }

            return RedirectToAction("CompanyList");
        }

        [HttpPost]
        [ActionName("RegisterDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDeleteConfirmed(string key, CompanyViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Logins.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("RegisterList");
            }

            return View(vm);
        }

        private void BindDropdown_Register(RegisterViewModel vm)
        {
            vm.EmployeeList = new SelectList(db.Employees.Where(m => m.active == true), "employeeId", "employeeName");
        }

        #endregion
    }
}