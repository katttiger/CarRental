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

        public VehicleStatuses vehicleStatus { get; set; }

        public string error = string.Empty;
        public bool sendError = false;
        public bool hiring = false;

        #endregion
        //Bookings
        public IEnumerable<IBooking> GetAllBookings() => _db.Get<IBooking>(null);

        //Get single
        public IBooking GetSingleBooking(int vehicleId)
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


       

    }
}
