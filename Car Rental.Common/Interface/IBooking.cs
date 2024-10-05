using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;

namespace Car_Rental.Common.Interface
{
    public interface IBooking
    {
        int Id { get; }
        ICustomer Customer { get; init; }
        IVehicle Vehicle { get; init; }
        public double DrivenKm { get; set; }
        DateTime DayRented { get; init; }
        DateTime DayReturned { get; }
        double CostToPay { get; set; }
        public bool isOpen { get; set; }

        public double ReturnVehicle(IVehicle vehicle, double kmDriven);
    }
}
