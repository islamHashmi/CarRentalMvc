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
    public class DriverAllocationController : Controller
    {
        CarRentalEntities db = new CarRentalEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AllocationList()
        {
            var list = new List<DriverAllocationViewModel>();

            var model = await db.DriverAllocations.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetDriverAllocation(item));
                }
            }

            var _model = new AllocationFilter
            {
                AllocationList = list
            };

            return View(_model);
        }


        [HttpGet]
        public async Task<ActionResult> AllocationManage(string key)
        {
            var vm = new DriverAllocationViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.DriverAllocations.FindAsync(_Id);

                    vm = GetDriverAllocation(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Booking(vm);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AllocationManage(DriverAllocationViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new DriverAllocation();

                    if (vm.AllocationId == null)
                    {
                        model.active = true;
                        model.entryBy = 1;
                        model.entryDate = DateTime.Now;

                        db.DriverAllocations.Add(model);
                    }
                    else
                    {
                        model = await db.DriverAllocations.FindAsync(vm.AllocationId);

                        if (model == null)
                            return HttpNotFound();

                        model.updatedBy = 2;
                        model.updatedDate = DateTime.Now;

                        db.Entry(model).State = EntityState.Modified;
                    }

                    model.bookingId = (long)vm.BookingId;
                    model.carType = vm.CarType;
                    model.carId = vm.CarId;
                    model.carNumber = vm.CarNumber;
                    model.carRegisterNumber = vm.CarRegisterNumber;
                    model.driverId = vm.DriverId;
                    model.driverName = vm.DriverName;
                    model.driverMobileNo = vm.DriverMobileNo;
                    model.slipNumber = vm.SlipNumber;

                    await db.SaveChangesAsync();

                    return RedirectToAction("AllocationList");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BindDropdown_Booking(vm);
            }

            return View(vm);
        }

        [HttpPost]
        public JsonResult GetBookingInfo(int? bookingId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "errorMessage", "" },
                { "bookingDate", "" },
                { "bookingReceivedBy", "" },
                { "company", "" },
                { "bookingBy", "" },
                { "bookingSource", "" },
                { "sourceDetail", "" },
                { "dateFrom", "" },
                { "dateTo", "" },
                { "reportingTo", "" },
                { "reportingLocation", "" },
                { "reportingTime", "" },
                { "modelCar", "" },
                { "dutyType", "" },
                { "specialInstructuion", "" },
                { "requisition", "" },
                { "slipNo", "" }
            };

            var booking = db.Bookings.Find(bookingId);

            if (booking != null)
            {
                data["bookingDate"] = booking.bookingDate.ToString("dd/MM/yyyy HH:mm");
                data["bookingReceivedBy"] = booking.Employee?.employeeName ?? "";
                data["company"] = booking.Party.Name ?? "";
                data["bookingBy"] = booking.bookingBy ?? "";
                data["bookingSource"] = booking.bookingSource;
                data["sourceDetail"] = booking.travelId ?? "";
                data["dateFrom"] = booking.requirementStartDate == null ? string.Empty : ((DateTime)booking.requirementStartDate).ToString("dd/MM/yyyy HH:mm");
                data["dateTo"] = booking.requirementEndDate == null ? string.Empty : ((DateTime)booking.requirementEndDate).ToString("dd/MM/yyyy HH:mm");
                data["reportingTo"] = booking.reportingTo ?? "";
                data["reportingLocation"] = booking.reportingLocation ?? "";
                data["reportingTime"] = booking.reportingTime;
                data["modelCar"] = booking.CarModel?.modelDescription ?? "";
                data["dutyType"] = booking.DutyType?.dutyDescription ?? "";
                data["specialInstructuion"] = booking.specialInstruction ?? "";
                data["requisition"] = booking.requisition == true ? "YES" : "NO";
                data["slipNo"] = "";
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private DriverAllocationViewModel GetDriverAllocation(DriverAllocation model)
        {
            return new DriverAllocationViewModel
            {
                AllocationId = model.allocationId,
                BookingId = model.bookingId,
                CarType = model.carType,
                CarId = model.carId,
                CarNumber = model.carNumber,
                CarRegisterNumber = model.carRegisterNumber,
                DriverId = model.driverId,
                DriverName = model.driverName,
                DriverMobileNo = model.driverMobileNo,
                SlipNumber = model.slipNumber,
                BookingDate = model.Booking == null ? null : (DateTime?)model.Booking.bookingDate,
                BookingReceivedBy = model.Booking == null ? string.Empty : model.Booking.Employee.employeeName,
                Company = model.Booking == null ? string.Empty : model.Booking.Party.Name,
                BookingBy = model.Booking == null ? string.Empty : model.Booking.bookingBy,
                BookingSource = model.Booking == null ? string.Empty : model.Booking.bookingSource,
                SourceDetail = model.Booking == null ? string.Empty : model.Booking.travelId,
                DateFrom = model.Booking == null ? null : model.Booking.requirementStartDate,
                DateTo = model.Booking == null ? null : model.Booking.requirementEndDate,
                ReportingTo = model.Booking == null ? string.Empty : model.Booking.reportingTo,
                ReportingLocation = model.Booking == null ? string.Empty : model.Booking.reportingLocation,
                ReportingTime = model.Booking == null ? null : model.Booking.reportingTime,
                ModelCar = model.Booking == null ? string.Empty : model.Booking.CarModel.modelDescription,
                DutyType = model.Booking == null ? string.Empty : model.Booking.DutyType.dutyDescription,
                SpecialInstruction = model.Booking == null ? string.Empty : model.Booking.specialInstruction,
                Requisition = model.Booking.requisition == true ? "Yes" : "No"
            };
        }

        private void BindDropdown_Booking(DriverAllocationViewModel vm)
        {
            vm.CarNumberList = new SelectList(db.Cars.Where(m => m.active == true), "CarId", "carNumber");
            vm.DriverNameList = new SelectList(db.Employees.Where(m => m.active == true && m.designationId == 2), "employeeId", "employeeName");
            vm.BookingNoList = new SelectList(db.Bookings.Where(m => m.active == true), "bookingId", "bookingNumber");
        }
    }
}