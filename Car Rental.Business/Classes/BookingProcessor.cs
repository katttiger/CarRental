using CarRental.ExtensionMethods;
using CarRental.Common.Classes;
using CarRental.Common.Enum;
using CarRental.Common.Interface;
using CarRental.Data.Interfaces;

namespace CarRental.Business.Classes
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

        public VehicleStatuses VehicleStatus { get; set; }

        public string error = string.Empty;
        public bool sendError = false;
        public bool hiring = false;

        #endregion
        //Bookings
        public IEnumerable<IBooking> GetAllBookings() => _db.Get<IBooking>(null);

        //Get single
        public IBooking GetSingleBooking(int vehicleId)
        {
            var booking = _db.GetSingle<IBooking>(i => i.Id.Equals(vehicleId));
            if (booking == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                return booking;
            }
        }

        public async Task<IBooking?> RentVehicleAsync(int? vehicleId, int? customerId)
        {
            if (vehicleId is null or 0)
            {
                sendError = true;
                hiring = false;
                error = "Must add vehicle";
                throw new ArgumentNullException(error);
            }
            else if (customerId is null or 0)
            {
                sendError = true;
                hiring = false;
                error = "Must add customer";
                throw new ArgumentNullException(error);
            }
            else
            {
                var newBooking = _db.RentVehicle((int)vehicleId, (int)customerId);
                Booking = (Booking)newBooking;
                if (newBooking.Vehicle.Status == VehicleStatuses.Available)
                {
                    hiring = false;
                    sendError = true;
                    newBooking.Vehicle.Status = VehicleStatuses.Available;
                    error = "Vehicle is already booked";
                    return null;
                }
                else
                {
                    hiring = true;
                    _db.Add(newBooking);
                    sendError = false;
                    await Task.Delay(1500);
                    hiring = false;
                    return newBooking;
                }
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
    }
}
