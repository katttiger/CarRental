using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;

namespace Car_Rental.Common.Classes
{
    public class Vehicle : IVehicle
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string Maker { get; set; }
        public Vehicletypes VehicleType { get; set; }
        public double Odometer { get; set; }
        public double PriceKm { get; set; }
        public double PriceDay => (int)VehicleType;
        public VehicleStatuses Status { get; set; }
        public void UpdateOdomoter(double km) => Odometer += km;
        public void ChangeStatus(VehicleStatuses stat)
        {
            if (stat == VehicleStatuses.Available)
                Status = VehicleStatuses.Booked;
            else
                Status = VehicleStatuses.Available;
        }
        public Vehicle()
        {

        }
    }
}
