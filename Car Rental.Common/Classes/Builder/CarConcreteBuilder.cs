using Car_Rental.Common.Enum;

namespace Car_Rental.Common.Classes.Builder
{
    public class VehicleConcreteBuilder : IVehicleBuilder
    {
        Vehicle _vehicle = new();
        public VehicleConcreteBuilder()
        {
            this.Reset();
        }
        public void Reset()
        {
            this._vehicle = new();
        }
        public void BuildVIN(string vin)
        {            
            this._vehicle.VIN = vin;
        }
        public void BuildMaker(string maker)
        {
            this._vehicle.Maker = maker;
        }
        public void BuildOdometer(double odometerValue)
        {

            this._vehicle.Odometer = odometerValue;
        }
        public void BuildPriceKm(double pricePerKm)
        {

            this._vehicle.PriceKm = pricePerKm;

        }

        public void BuildVehicleType(Vehicletypes vehicletype)
        {

            this._vehicle.VehicleType = vehicletype;

        }
        public void BuildVehicleStatus(VehicleStatuses vehiclestatus)
        {

            this._vehicle.Status = vehiclestatus;
        }

        public Vehicle GetProduct()
        {
            Vehicle result = this._vehicle;
            result.Id = 1;
            this.Reset();
            return result;
        }

    }
}
