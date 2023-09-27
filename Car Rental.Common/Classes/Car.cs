using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;

namespace Car_Rental.Common.Classes
{
    public class Car : IVehicle
    {
       public string VIN { get; set; }
        public string Maker { get; set; }
        public Vehicletypes VehicleType { get; set; }
        public double Odometer { get; set; }
        public double PriceKm { get; set; }
        public double PriceDay { get; set; }
        public VehicleStatuses Status { get; set; }

        public Car(string vin, string make, Vehicletypes vehicleTYpe, double odo, double pKm, double pDay, VehicleStatuses on)
        {
            Maker = make;
            VehicleType = vehicleTYpe;
            Odometer = odo;
            PriceKm = pKm;
            PriceDay = pDay;
            Status = on;
            VIN = vin;

        }
        public void UpdateOdomoter(double km)
        {
            Odometer += km;

        }

        public void ChangeStatus(VehicleStatuses stat)
        {
            if (stat == VehicleStatuses.Available)
                Status = VehicleStatuses.Booked;
            else
                Status = VehicleStatuses.Available;

        }
    }
}
