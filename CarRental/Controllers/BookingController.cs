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
    public class BookingController : Controller
    {
        CarRentalEntities db = new CarRentalEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BookingList()
        {
            var list = new List<BookingViewModel>();

            var model = await db.Bookings.Where(m => m.active == true).ToListAsync();

            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(GetBooking(item));
                }
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> BookingDetail(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Bookings.FindAsync(_Id);

                return View(GetBooking(model));
            }

            return RedirectToAction("BookingList");
        }

        [HttpGet]
        public async Task<ActionResult> BookingManage(string key)
        {
            var vm = new BookingViewModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = HelperClass.Decrypt(key);

                    int.TryParse(key, out int _Id);

                    if (_Id == 0)
                        return HttpNotFound();

                    var model = await db.Bookings.FindAsync(_Id);

                    vm = GetBooking(model);
                }
                else
                {
                    vm.BookingNumber = Generate_Booking_Number();
                    vm.Requisition = false;
                    vm.BookingReceivedByName = "Islam";
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
        public async Task<ActionResult> BookingManage(BookingViewModel vm)
        {
            using (DbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = new Booking();

                        if (vm.BookingId == null)
                        {
                            model.active = true;
                            model.entryBy = 1;
                            model.entryDate = DateTime.Now;

                            db.Bookings.Add(model);
                        }
                        else
                        {
                            model = await db.Bookings.FindAsync(vm.BookingId);

                            if (model == null)
                                return HttpNotFound();

                            model.updatedBy = 2;
                            model.updatedDate = DateTime.Now;

                            db.Entry(model).State = EntityState.Modified;
                        }

                        if (vm.GuestAddressId == null && vm.GuestId != null && !string.IsNullOrWhiteSpace(vm.ReportingLocation))
                        {
                            guestAddress ga = new guestAddress
                            {
                                guestId = (int)vm.GuestId,
                                address = vm.ReportingLocation,
                                entryBy = 1,
                                entryDate = DateTime.Now
                            };
                        }

                        model.branchId = vm.BranchId;
                        model.bookingType = vm.BookingType;
                        model.billAddress = vm.BillAddress;
                        model.bookingNumber = vm.BookingNumber;
                        model.bookingDate = (DateTime)vm.BookingDate;
                        model.billingType = vm.BiilingType;
                        model.bookingReceivedBy = vm.BookingReceivedBy;
                        model.partyId = vm.PartyId;
                        model.bookingBy = vm.BookingBy;
                        model.guestId = vm.GuestId;
                        model.guestMobileNo = vm.BookedByMobileNo;
                        model.bookingSource = vm.BookingSource;
                        model.requirementStartDate = vm.RequirementStartDate;
                        model.requirementEndDate = vm.RequirementEndDate;
                        model.travelId = vm.TravelId;
                        model.reportingTo = vm.ReportingTo;
                        model.reportingMobileNo = vm.ReportingMobileNo;
                        model.reportingLocation = vm.ReportingLocation;
                        model.guestAddressId = vm.GuestAddressId;
                        model.reportingTime = vm.ReportingTime;
                        model.carModelId = vm.CarModelId;
                        model.dutyTypeId = vm.DutyTypeId;
                        model.specialInstruction = vm.SpecialInstruction;
                        model.requisition = vm.Requisition;
                        model.mobileNumber1 = vm.MobileNumber1;
                        model.mobileNumber2 = vm.MobileNumber2;
                        model.mobileNumber3 = vm.MobileNumber3;

                        await db.SaveChangesAsync();

                        dbTran.Commit();

                        return RedirectToAction("BookingList");
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();

                    throw;
                }
                finally
                {
                    BindDropdown_Booking(vm);
                }
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> BookingDelete(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Bookings.FindAsync(_Id);

                return View(GetBooking(model));
            }

            return RedirectToAction("BookingList");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("BookingDelete")]
        public async Task<ActionResult> BookingDeleteConfirm(string key, BookingViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HelperClass.Decrypt(key);

                int.TryParse(key, out int _Id);

                if (_Id == 0)
                    return HttpNotFound();

                var model = await db.Bookings.FindAsync(_Id);

                model.active = false;

                await db.SaveChangesAsync();

                return RedirectToAction("BookingList");
            }

            return View(vm);
        }

        private string Generate_Booking_Number()
        {
            var number = db.sp_Generate_BookingNo().ToList();

            return number[0].ToString();
        }

        private BookingViewModel GetBooking(Booking model)
        {
            return new BookingViewModel
            {
                BookingId = model.bookingId,
                BranchId = model.branchId,
                BranchName = model.CompanyBranch == null ? string.Empty : model.CompanyBranch.branchName,
                BookingType = model.bookingType,
                BillAddress = model.billAddress,
                BookingNumber = model.bookingNumber,
                BookingDate = model.bookingDate,
                BillingTypeCode = model.billingType,
                BiilingType = model.billingType == "N" ? "Normal" : model.billingType == "E" ? "Event" : model.billingType == "M" ? "Monthly" : string.Empty,
                BookingReceivedBy = model.bookingReceivedBy,
                PartyId = model.partyId,
                PartyName = model.Party == null ? string.Empty : model.Party.Name,
                BookingBy = model.bookingBy,
                GuestId = model.guestId,
                GuestName = model.Guest == null ? string.Empty : model.Guest.guestName,
                BookedByMobileNo = model.guestMobileNo,
                BookingSource = model.bookingSource,
                RequirementStartDate = model.requirementStartDate,
                RequirementEndDate = model.requirementEndDate,
                TravelId = model.travelId,
                ReportingTo = model.reportingTo,
                ReportingMobileNo = model.reportingMobileNo,
                ReportingLocation = model.reportingLocation,
                GuestAddressId = model.guestAddressId,
                ReportingTime = model.reportingTime,
                CarModelId = model.carModelId,
                CarModelName = model.CarModel == null ? string.Empty : model.CarModel.modelDescription,
                DutyTypeId = model.dutyTypeId,
                DutyTypeName = model.DutyType == null ? string.Empty : model.DutyType.dutyDescription,
                SpecialInstruction = model.specialInstruction,
                Requisition = model.requisition,
                MobileNumber1 = model.mobileNumber1,
                MobileNumber2 = model.mobileNumber2,
                MobileNumber3 = model.mobileNumber3
            };
        }

        private void BindDropdown_Booking(BookingViewModel vm)
        {
            vm.BranchList = new SelectList(db.CompanyBranches.Where(m => m.active == true), "branchId", "BranchName");
            vm.PartyList = new SelectList(db.Parties.Where(m => m.active == true), "partyId", "Name");
            vm.GuestList = new SelectList(db.Guests.Where(m => m.active == true), "guestId", "guestName");
            vm.CarModelList = new SelectList(db.CarModels.Where(m => m.active == true), "carModelId", "modelDescription");
            vm.DutyTypeList = new SelectList(db.DutyTypes.Where(m => m.active == true), "dutyTypeId", "dutyDescription");
        }

        [HttpPost]
        public JsonResult GetGuestInformation(int? guestId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "errorMessage", "" },
                { "guestName", "" },
                { "guestMob", "" },
                { "bookBy", "" },
                { "bookByMob", "" },
                { "carModelId", "" },
                { "Mob1", "" },
                { "Mob2", "" },
                { "Mob3", "" },
                { "guestAddress", "" }
            };
            
            var guest = db.Guests.Find(guestId);
            
            if (guest != null)
            {
                data["guestName"] = guest.guestName ?? "";
                data["guestMob"] = guest.guestMobile ?? "";
                data["bookBy"] = guest.bookedBy ?? "";
                data["bookByMob"] = guest.bookedByMobile ?? "";
                data["carModelId"] = guest.carModelId ?? 0;
                data["Mob1"] = guest.contactNumber1 ?? "";
                data["Mob2"] = guest.contactNumber2 ?? "";
                data["Mob3"] = guest.contactNumber3 ?? "";

                data["guestAddress"] = new SelectList(db.guestAddresses.Where(m => m.guestId == guestId), "guestAddressId", "address");
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGuestList(int? partyId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "ErrorMessage", "" },
                { "guestList", "" }
            };

            if(partyId != null)
            {
                data["guestList"] = new SelectList(db.Guests.Where(m => m.partyId == partyId), "guestId", "guestName");
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}