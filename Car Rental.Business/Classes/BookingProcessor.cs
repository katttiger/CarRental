using Car_rental.ExtensionMethods;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes
{
    public class BookingProcessor
    {
        #region
        //Data and resources
        private readonly IData _db;
        public BookingProcessor(IData db) => _db = db;

        public Customer Customer = new();
        public Vehicle Vehicle = new();
        public Booking Booking = new();

        public VehicleStatuses vStatus { get; set; }

        public string error = string.Empty;
        public bool sendError = false;
        public bool hiring = false;
        #endregion
        //Bookings
        //Get full list
        public IEnumerable<IBooking> GetBookings()=>_db.Get<IBooking>(null);
        
        //Get single
        public IBooking GetBooking(int vehicleId)
        {
            var booking = _db.Single<IBooking>(i => i.Id.Equals(vehicleId));
            try
            {
                if (booking is null)
                    throw new ArgumentNullException();
                return booking;
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        //Customer
        //Get full list
        public IEnumerable<ICustomer> GetCustomers()=>_db.Get<ICustomer>(null);
    //Get single
        public ICustomer? GetCustomer(string ssn)
        {
            var customer = _db.Single<ICustomer>(s => s.SSN.Equals(ssn));
            try
            {
                if (customer is null)
                    throw new ArgumentNullException();
                return customer;
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        //Vehicle
        //Get full list
        public VehicleStatuses UpdateVStatus(VehicleStatuses vehicleStatuses = default)
        => vStatus = vehicleStatuses;

        public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
        {
            var list = _db.Get<IVehicle>(null);
            if (list is not null)
            {
                vStatus = status;
                var vehicle = list;
              
                if (!vStatus.Equals(VehicleStatuses.Available) && !vStatus.Equals(VehicleStatuses.Booked))
                    return vehicle;
                else
                {  
                    return vehicle.Where(v => v.Status.Equals(vStatus));
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        //Get single
        public IVehicle? GetVehicle(int vehicleId)
        {
            return _db.Single<IVehicle>(v => v.Id.Equals(vehicleId));
        }
        //Get single
        public IVehicle? GetVehicle(string vin)
        {
            return _db.Single<IVehicle>(t => t.VIN.Equals(vin));
        }

        //Async
        //TODO: Fix errorhandling for when you don't add a customer
        public async Task<IBooking> RentVehicleAsync(int vehicleId, int customerId)
        {
            if (customerId != null && vehicleId != null && vehicleId != 0 && customerId != 0)
            {
                var newB = _db.RentVehicle(vehicleId, customerId);
                Booking = (Booking)newB;
                if (newB.Vehicle.Status == VehicleStatuses.Available)
                {
                    hiring = false;
                    sendError = true;
                    newB.Vehicle.Status = VehicleStatuses.Available;
                    error = "Vehicle is already booked";
                    return newB = null;
                }
                else
                {
                    hiring = true;
                    _db.Add(newB);
                    sendError = false;
                    await Task.Delay(1500);
                    hiring = false;
                    return newB;
                }
            }
            else
            {
                sendError = true;
                hiring = false;
                error = "Must add customer";
                throw new ArgumentNullException(error);
            }
        }

        /* public IEnumerable<IBooking> RentVehicle(int vehicleId, int customerId)
         {
             if (vehicleId != null && customerId != null)
             {
                 var newB = _db.RentVehicle(vehicleId, customerId);
                 _db.Add(newB);
                 return GetBookings();
             }
             else
             {
                 sendError = true;
                 error = "Must add customer";
                 throw new ArgumentNullException(error);
             }
         }*/

        //Return vehicle
        public IBooking ReturnVehicle(int vehicleId, double distance)
        {
            try
            {
                var newB = _db.ReturnVehicle(vehicleId);
                newB.ReturnVehicle(newB.Vehicle, distance);
                newB.DrivenKm = distance;

                newB.isOpen = false;
                return newB;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        //Add vehicle/customer
        //add vehicle
        public void AddVehicle(string make, string vin, double odometer,
            double costKm, double costDay, VehicleStatuses status, Vehicletypes type)
        {
            if (make != null && vin != null && odometer != null
                && costKm != null && costDay != null && status != null && type != null)
            {
                sendError = false;
                if (Vehicle.VehicleType != Vehicletypes.Motorcycle)
                {
                    Vehicle = new Car(_db.NextVehicleId, vin, make, type, odometer, costKm, costDay, status);
                    _db.Add((IVehicle)Vehicle);
                }
                else
                {
                    Vehicle = new Motorcycle(_db.NextVehicleId, vin, make, type, odometer, costKm, costDay, status);
                    _db.Add((IVehicle)Vehicle);
                }
            }
            else
            {
                sendError = true;
                error = "The vehicle could not be added";
            }
        }
        //Add customer
        public void AddCustomer(int ssn, string fName, string lName)
        {
            if (ssn != 0)
            {
                if (_db.NextPersonId != null && fName != null && lName != null && ssn != null)
                {
                    sendError = false;
                    Customer = new(_db.NextPersonId, fName, lName, ssn);
                    _db.Add((ICustomer)Customer);
                    //edit = false;
                }
                else
                {
                    error = "Customer could not be added";
                    sendError = true;
                }
            }
            else
            {
                error = "SSN cannot be 0 or empty";
                sendError = true;
            }
        }

        //Calling default interface methods
        public string[] VehicleStatusNames => _db.VehicleStatusNames();
        public string[] VehicleTypeNames => _db.VehicleTypeNames;
        //public string[] VehicleMakesNames => _db.VehicleMakeNames;
        public Vehicletypes GetVehicleType(string name) => _db.GetVehicleType(name);

    }
}
