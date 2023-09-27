using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;

namespace Car_Rental.Common.Classes
{
    public class Booking : IBooking
    {
        public ICustomer Customer { get; init; }
        public IVehicle Vehicle { get; init; }
        public string VIN {  get; init; }
        public double Odometer { get; set; }
        public double DrivenKm { get; set; }
        public DateTime DayRented { get; init; }
        public DateTime DayReturned { get; private set; }
        public double CostToPay { get; set; }

        private double _totalDays;

        public Booking(Customer cust, IVehicle v)
        {
            Customer = cust;
            Vehicle = v;
            VIN = v.VIN;
            DayRented = DateTime.Now;
            Odometer = v.Odometer;
            Vehicle.ChangeStatus(VehicleStatuses.Available);
        }
       
        public void ReturnVehicle(IVehicle vehicle, double drivenKm)
        {
            //Change status to "not booked"
            vehicle.ChangeStatus(VehicleStatuses.Booked);

            //DayReturned=DayRented.AddDays(daysHired);
            DayReturned = DateTime.Now; 

            CalculateDate(DayRented, DayReturned);

            //Update odometer
            vehicle.UpdateOdomoter(drivenKm);

            //Cost
            CostToPay = _totalDays * vehicle.PriceDay + drivenKm * vehicle.PriceKm;
            

            void CalculateDate(DateTime dRent, DateTime dReturn)
            {
                var tDays = (dReturn - dRent).TotalDays;
                if (tDays < 1)
                    _totalDays = 1;
                else
                    _totalDays = tDays;
            }
        }
    }
}
