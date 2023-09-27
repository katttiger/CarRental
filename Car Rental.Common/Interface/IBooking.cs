using Car_Rental.Common.Enum;

namespace Car_Rental.Common.Interface
{
    public interface IBooking
    {
        ICustomer Customer { get; init; }
        IVehicle Vehicle { get; init; }
        public double Odometer { get; set; }
        public double DrivenKm { get; set; }
        DateTime DayRented { get; init; }
        DateTime DayReturned { get; }
        double CostToPay { get; set; }
        public string VIN { get; init; }

        void ReturnVehicle(IVehicle vehicle, double kmDriven);
    }
}
