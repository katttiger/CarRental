using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes.Builder
{
    public class BuilderVehicleMechanic
    {
        private IVehicleBuilder _vehicleBuilder;

        public IVehicleBuilder VehicleBuilder
        { set { _vehicleBuilder = value; } }


        public void BuildFullFeaturedProduct()
        {
            this._vehicleBuilder.BuildVIN();
            this._vehicleBuilder.BuildMaker();
            this._vehicleBuilder.BuildOdometer();
            this._vehicleBuilder.BuildPriceKm();
            this._vehicleBuilder.BUildVehicleType();
            this._vehicleBuilder.BuildVehicleStatus();
        }
    }
}
