using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;

namespace Car_Rental.Common.Classes
{
    public class Motorcycle : Vehicle
    {
    
        public Motorcycle(int id, string vin, string make, Vehicletypes vehicleType,
              double odo, double pKm, double pDay, VehicleStatuses on)
        {
            Id = id;
            Maker = make;
            VehicleType = vehicleType;
            Odometer = odo;
            PriceKm = pKm;
            Status = on;
            VIN = vin;
        }
        public Motorcycle()
        {

        }
    }
}
