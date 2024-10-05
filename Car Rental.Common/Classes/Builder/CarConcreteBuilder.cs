using Car_Rental.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void BuildVIN(string vin)
        {
            this._vehicle.VIN = vin;
        }
        void BuildMaker(string maker)
        {

            this._vehicle.Maker = maker;
        }
        void BuildOdometer(double odometerValue)
        {

            this._vehicle.Odometer = odometerValue;
        }
        void BuildPriceKm(double pricePerKm)
        {

            this._vehicle.PriceKm = pricePerKm;

        }
        void BuildVehicleType(Vehicletypes vehicletype)
        {

            this._vehicle.VehicleType = vehicletype;

        }
        void BuildVehicleStatus(VehicleStatuses vehiclestatus)
        {

            this._vehicle.Status = vehiclestatus;
        }

        public Vehicle GetProduct()
        {
            Vehicle result = this._vehicle;
            this.Reset();
            return result;
        }

    }
}
