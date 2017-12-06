using CarRental.Helpers;
using CarRental.Models;
using CarRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class DutySlipController : Controller
    {
        CarRentalEntities db = new CarRentalEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DutySlipList()
        {
            var list = new List<DutySlipViewModel>();

            var model = await db.DutySlips.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetDutySlip(item));
                }
            }

            var _model = new DutySlipFilter
            {
                DutySlipList = list
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<ActionResult> DutySlipManage(string key)
        {
            var vm = new DutySlipViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.DutySlips.FindAsync(_Id);

                    vm = GetDutySlip(model);
                }
                else
                {
                    vm.CarType = "OW";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_DutySlip(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DutySlipManage(DutySlipViewModel vm)
        {
            using (DbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = new DutySlip();

                        if (vm.DutySlipId == null)
                        {
                            model.active = true;
                            model.entryBy = 1;
                            model.entryDate = DateTime.Now;

                            db.DutySlips.Add(model);
                        }
                        else
                        {
                            model = await db.DutySlips.FindAsync(vm.DutySlipId);

                            if (model == null)
                                return HttpNotFound();

                            model.updatedBy = 2;
                            model.updatedDate = DateTime.Now;

                            db.Entry(model).State = EntityState.Modified;
                        }

                        model.branchId = vm.BranchId;
                        model.slipNumber = (int)vm.SlipNumber;
                        model.slipDate = (DateTime)vm.SlipDate;
                        model.partyId = vm.PartyId;
                        model.bookingId = (long)vm.BookingId;
                        model.carType = vm.CarType;
                        model.carId = vm.CarId;
                        model.driverId = vm.DriverId;
                        model.op_carNumber = vm.OP_CarNumber;
                        model.op_carRegNumber = vm.OP_CarRegisterNumber;
                        model.op_driverName = vm.OP_DriverName;
                        model.op_driverMobile = vm.OP_DriverMobile;
                        model.supplierId = vm.SupplierId;
                        model.driverBalance = vm.DriverBalance;
                        model.advanceDriver = vm.AdvanceDriver;
                        model.openingTime = vm.OpeningTime;
                        model.openingKM = vm.OpeningKM;

                        await db.SaveChangesAsync();

                        dbTran.Commit();

                        return RedirectToAction("DutySlipList");
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();

                    throw;
                }
                finally
                {
                    BindDropdown_DutySlip(vm);
                }
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> DutySlipDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.DutySlips.FindAsync(_Id);

                return View(GetDutySlip(model));
            }

            return RedirectToAction("DutySlipList");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("DutySlipDelete")]
        public async Task<ActionResult> DutySlipDeleteConfirm(string key, DutySlipViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.DutySlips.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("DutySlipList");
            }

            return View(vm);
        }

        [HttpPost]
        public JsonResult GetBookingNumber(int? partyId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "errorMessage", "" },
                { "bookingNos", "" }
            };

            if (partyId != null)
            {
                var bookings = db.Bookings.Where(m => m.partyId == partyId);

                if (bookings != null)
                {
                    data["bookingNos"] = new SelectList(db.Bookings.Where(m => m.partyId == partyId), "bookingId", "BookingNumber");
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBookingInfo(int? bookingId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "errorMessage", "" },
                { "bookingType", "" },
                { "bookingBy", "" },
                { "reportingTo", "" },
                { "reportingLocation", "" },
                { "reportingTime", "" },
                { "modelCar", "" },
                { "dutyType", "" },
                { "dateFrom", "" },
                { "dateTo", "" },
                { "mobile1", "" },
                { "mobile2", "" },
                { "mobile3", "" }
            };

            var booking = db.Bookings.Find(bookingId);

            if (booking != null)
            {
                if (TimeSpan.TryParse(Convert.ToString(booking.reportingTime), out TimeSpan timeSpan))
                {
                    data["reportingTime"] = string.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes);
                }

                data["bookingType"] = booking.bookingType == "N" ? "Normal" : booking.bookingType == "M" ? "Monthly" : booking.bookingType == "E" ? "Event" : string.Empty;
                data["bookingBy"] = booking.bookingBy ?? "";
                data["dateFrom"] = booking.requirementStartDate == null ? string.Empty : ((DateTime)booking.requirementStartDate).ToString("dd/MM/yyyy");
                data["dateTo"] = booking.requirementEndDate == null ? string.Empty : ((DateTime)booking.requirementEndDate).ToString("dd/MM/yyyy");
                data["reportingTo"] = booking.reportingTo ?? "";
                data["reportingLocation"] = booking.reportingLocation ?? "";
                data["modelCar"] = booking.CarModel?.modelDescription ?? "";
                data["dutyType"] = booking.DutyType?.dutyDescription ?? "";
                data["mobile1"] = booking.mobileNumber1 ?? "";
                data["mobile2"] = booking.mobileNumber2 ?? "";
                data["mobile3"] = booking.mobileNumber3 ?? "";
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDriverInfo(int? driverId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "errorMessage", "" },
                { "driverMobile", "" },
                { "balance", "" }
            };

            if (driverId != null)
            {
                var driver = db.Employees.FirstOrDefault(m => m.employeeId == driverId);

                if (driver != null)
                {
                    data["driverMobile"] = driver.mobileNumber;
                    data["balance"] = driver.basicAmount;
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCarInfo(int? carId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "errorMessage", "" },
                { "carRegNumber", "" },
                { "ownerId", "" }
            };

            if (carId != null)
            {
                var car = db.Cars.FirstOrDefault(m => m.CarId == carId);

                if (car != null)
                {
                    data["carRegNumber"] = car.registrationNumber;
                    data["ownerId"] = car.ownerId;
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private DutySlipViewModel GetDutySlip(DutySlip model)
        {
            return new DutySlipViewModel
            {
                DutySlipId = model.dutySlipId,
                BookingId = model.bookingId,
                BookingType = model.Booking?.bookingType,
                BookingNumber = model.Booking?.bookingNumber,
                CarType = model.carType,
                CarTypeName = model.carType == "OP" ? "Operator" : "Owner",
                CarId = model.carId,
                CarNo = model.Car?.carNumber,
                OP_CarNumber = model.op_carNumber,
                OP_CarRegisterNumber = model.op_carRegNumber,
                OP_DriverName = model.op_driverName,
                OP_DriverMobile = model.op_driverMobile,
                DriverId = model.driverId,
                DriverName = model.Employee?.employeeName,
                DriverMobileNo = model.Employee?.mobileNumber,
                SlipNumber = model.slipNumber,
                SlipDate = model.slipDate,
                PartyId = model.partyId,
                PartyName = model.Party?.Name,
                BookingBy = model.Booking?.bookingBy,
                DutyDateFrom = model.Booking?.requirementStartDate,
                DutyDateTo = model.Booking?.requirementEndDate,
                ReportingTo = model.Booking?.reportingTo,
                ReportingLocation = model.Booking?.reportingLocation,
                ReportingTime = model.Booking?.reportingTime,
                CarModel = model.Booking?.CarModel?.modelDescription,
                DutyType = model.Booking?.DutyType?.dutyDescription,
                Mobile1 = model.Booking?.mobileNumber1,
                Mobile2 = model.Booking?.mobileNumber2,
                Mobile3 = model.Booking?.mobileNumber3
            };
        }

        private void BindDropdown_DutySlip(DutySlipViewModel vm)
        {
            vm.BranchList = ApplicationParameter.GetBranches(db);
            vm.PartyList = ApplicationParameter.GetParties(db);
            vm.CarNumberList = ApplicationParameter.GetCars(db);
            vm.DriverNameList = ApplicationParameter.GetDrivers(db);
            vm.SupplierList = ApplicationParameter.GetOwners(db);
            vm.BookingNoList = new SelectList(db.Bookings.Where(m => m.active == true), "bookingId", "bookingNumber");
        }
    }
}