using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_rental.ExtensionMethods;

namespace Car_Rental.Common.Classes
{
    public class Booking : IBooking
    {
        public int Id { get; set; }
        public ICustomer Customer { get; init; }
        public IVehicle Vehicle { get; init; }
        public double DrivenKm { get; set; }
        public DateTime DayRented { get; init; }
        public DateTime DayReturned { get; private set; }
        public double DailyCost { get; set; }
        public double KmCost { get; set; }
        public double CostToPay { get; set; }
        public bool isOpen { get; set; }

        public Booking(int id, ICustomer cust, IVehicle v, bool IsOpen=true)
        {
            Id = id;
            Customer = cust;
            Vehicle = v;
            DayRented = DateTime.Now;
            Vehicle.ChangeStatus(VehicleStatuses.Available);
            DailyCost = Vehicle.PriceDay;
            KmCost = Vehicle.PriceKm;
            isOpen = IsOpen;
        }
        public Booking()
        {

        }

        public double ReturnVehicle(IVehicle vehicle, double drivenKm)
        {
            //Change status to "not booked"
            vehicle.ChangeStatus(VehicleStatuses.Booked);

            //DayReturned=DayRented.AddDays(daysHired);
            DayReturned = DateTime.Now;

            DailyCost = VExtensions.Duration(DayRented, DayReturned) * DailyCost;
            KmCost = VExtensions.KmDriven(drivenKm, vehicle.PriceKm);
           
            //Update odometer
            vehicle.UpdateOdomoter(drivenKm);

            //Cost
            CostToPay = DailyCost * KmCost;
            return CostToPay;
        }
    }
}
