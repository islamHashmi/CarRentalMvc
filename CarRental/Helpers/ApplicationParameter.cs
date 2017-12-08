using CarRental.Models;
using System.Linq;
using System.Web.Mvc;

namespace CarRental.Helpers
{
    public class ApplicationParameter
    {
        public static SelectList GetCompanies(CarRentalEntities db)
        {
            return new SelectList(db.Companies
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        companyId = m.companyId,
                                        companyName = m.companyName
                                    }), "companyId", "companyName");
        }

        public static SelectList GetBranches(CarRentalEntities db)
        {
            return new SelectList(db.CompanyBranches
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        branchId = m.branchId,
                                        branchName = m.branchName + " [ " + m.Company.companyName + " ]"
                                    }), "branchId", "branchName");
        }

        public static SelectList GetDesignations(CarRentalEntities db)
        {
            return new SelectList(db.Designations
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        designationId = m.designationId,
                                        designationName = m.designationName
                                    }), "designationId", "designationName");
        }

        public static SelectList GetStatus(CarRentalEntities db)
        {
            return new SelectList(db.PartyStatus
                                    .Select(m => new
                                    {
                                        statusCode = m.statusCode,
                                        statusDescription = m.statusDescription
                                    }), "statusCode", "statusDescription");
        }

        public static SelectList GetParties(CarRentalEntities db)
        {
            return new SelectList(db.Parties
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        partyId = m.partyId,
                                        Name = m.Name
                                    }), "partyId", "Name");
        }

        public static SelectList GetDepartments(CarRentalEntities db)
        {
            return new SelectList(db.Departments
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        departmentId = m.departmentId,
                                        departmentName = m.departmentName
                                    }), "departmentId", "departmentName");
        }

        public static SelectList GetCarModels(CarRentalEntities db)
        {
            return new SelectList(db.CarModels
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        carModelId = m.carModelId,
                                        modelDescription = m.modelDescription
                                    }), "carModelId", "modelDescription");
        }

        public static SelectList GetFuelTypes(CarRentalEntities db)
        {
            return new SelectList(db.FuelTypes
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        fuelTypeCode = m.fuelTypeCode,
                                        fuelTypeDescription = m.fuelTypeDescription
                                    }), "fuelTypeCode", "fuelTypeDescription");
        }

        public static SelectList GetOwners(CarRentalEntities db)
        {
            return new SelectList(db.Parties
                                    .Where(m => m.active == true && m.status == "SU")
                                    .Select(m => new
                                    {
                                        partyId = m.partyId,
                                        Name = m.Name
                                    }), "partyId", "Name");
        }

        public static SelectList GetDrivers(CarRentalEntities db)
        {
            return new SelectList(db.Employees
                                    .Where(m => m.active == true && m.designationId == 2)
                                    .Select(m => new
                                    {
                                        employeeId = m.employeeId,
                                        employeeName = m.employeeName
                                    }), "employeeId", "employeeName");
        }

        public static SelectList GetDutyTypes(CarRentalEntities db)
        {
            return new SelectList(db.DutyTypes
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        dutyTypeId = m.dutyTypeId,
                                        dutyDescription = m.dutyDescription
                                    }), "dutyTypeId", "dutyDescription");
        }

        public static SelectList GetSchemes(CarRentalEntities db)
        {
            return new SelectList(db.Schemes
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        schemeId = m.schemeId,
                                        schemeName = m.schemeName
                                    }), "schemeId", "schemeName");
        }

        public static SelectList GetCars(CarRentalEntities db)
        {
            return new SelectList(db.Cars
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        CarId = m.CarId,
                                        carNumber = m.carNumber + " [ " + m.registrationNumber + " ]"
                                    }), "CarId", "carNumber");
        }

        public static SelectList GetReleasePoints(CarRentalEntities db)
        {
            return new SelectList(db.ReleasePoints
                                    .Where(m => m.active == true)
                                    .Select(m => new
                                    {
                                        releasePointId = m.releasePointId,
                                        releasePointName = m.releasePointName
                                    }), "releasePointId", "releasePointName");
        }
    }
}