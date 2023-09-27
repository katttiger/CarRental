using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;
using Car_Rental.Common.Enum;

namespace Car_Rental.Business.Classes
{ //Ta emot requests och förändringar från Car Rental
    //Importera data från data-filen
    public class BookingProcessor
    {
        private readonly IData _db;
        public BookingProcessor(IData db) => _db = db;

        public IEnumerable<IBooking> GetBookings()
        {
            return _db.GetBookings();
        }
        public IEnumerable<ICustomer> GetCustomers()
        {
            return _db.GetPersons();
        }

        //Sortera m.h.a. LINQ & lambda (Where) Where(Lambda_>v->v)| Vehiclestatus=vs
        public IEnumerable<IVehicle> GetVehicles(VehicleStatuses vs = default)
        {
            //Fetchlist
            //IF staus=default=>Return
            var ist = _db.GetVehicles();

            //Alla som är bokade
            if (vs.Equals(VehicleStatuses.Booked))
            {
                //[var varName]=[nameOfClass].[Where][X => X compares X.Enum.Equals("")]
                return ist.Where(v => v.Status.Equals(vs));

            }
            //Alla som inte är bokade
            else if (vs.Equals(VehicleStatuses.Available))
            {
                return ist.Where(v => v.Status.Equals(VehicleStatuses.Available));
            }
            else
            {
                return _db.GetVehicles();
            }
            //db.List<IVehicle>
        }
    }
}
