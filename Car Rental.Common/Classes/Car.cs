using CarRental.Common.Enum;
using CarRental.Common.Interface;

namespace CarRental.Common.Classes
{
    public class Car : Vehicle
    {
        public Car(int id, string vin, string make, Vehicletypes vehicleType,
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

        public Car()
        {

        }

        
    }
}
