using Car_Rental.Common.Enum;

namespace Car_Rental.Common.Classes.Builder
{
    public class BuilderVehicleDirector
    {
        private IVehicleBuilder _vehicleBuilder;

        public IVehicleBuilder VehicleBuilder
        {
            set { _vehicleBuilder = value; }
        }

        public void BuildMinimalFeaturedProduct()
        {
            this._vehicleBuilder.BuildVehicleType();
            this._vehicleBuilder.BuildMaker();
            this._vehicleBuilder.BuildVehicleType();
            this._vehicleBuilder.BuildOdometer();
        }

        public void BuildFullFeaturedProduct()
        {
            this._vehicleBuilder.BuildVIN();
            this._vehicleBuilder.BuildMaker();
            this._vehicleBuilder.BuildOdometer();
            this._vehicleBuilder.BuildPriceKm();
            this._vehicleBuilder.BuildVehicleType();
            this._vehicleBuilder.BuildVehicleStatus();
        }

        public Vehicle ConstructSkoda(VehicleConcreteBuilder builder)
        {
            builder.Reset();
            builder.BuildVIN("SkodaVIN123");
            builder.BuildMaker(Maker.Toyota.ToString());
            builder.BuildVehicleType(Vehicletypes.Combi);
            builder.BuildOdometer(4000);
            builder.BuildPriceKm(1.2);
            builder.BuildVehicleStatus(VehicleStatuses.Available);
            return builder.GetProduct();
        }
        
        public Vehicle ConstructMotorcycle(VehicleConcreteBuilder builder)
        {
            builder.Reset();
            builder.BuildVehicleType(Vehicletypes.Motorcycle);
            builder.BuildMaker(Maker.Toyota.ToString());
            builder.BuildVehicleStatus(VehicleStatuses.Available);
            builder.BuildPriceKm(1.2);
            return builder.GetProduct();
        }
    }
}
