using Car_Rental.Business.FactoryPattern;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes
{
    public class VehicleProcessor
    {
        #region
        //Data and resources
        private readonly IData _db;
        public VehicleProcessor(IData db) => _db = db;
        public Vehicle Vehicle = new();
        public VehicleStatuses vehicleStatus { get; set; }

        public string error = string.Empty;
        public bool sendError = false;
        #endregion

        //Vehicle
        //Get full list
        public VehicleStatuses UpdateVehicleStatus(VehicleStatuses vehicleStatuses = default)
        => vehicleStatus = vehicleStatuses;

        public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
        {
            var list = _db.Get<IVehicle>(null);
            if (list is not null)
            {
                return list;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        //Get single
        public IVehicle? GetVehicle(int vehicleId)
        {
            return _db.GetSingle<IVehicle>(v => v.Id.Equals(vehicleId));
        }
        //Get single
        public IVehicle? GetVehicle(string vin)
        {
            return _db.GetSingle<IVehicle>(t => t.VIN.Equals(vin));
        }

        //Add vehicle/customer
        //add vehicle
        public void AddVehicle(string? make, string? vin, double? odometer,
            double? costKm, double? costDay, VehicleStatuses? status, Vehicletypes? type)
        {
            if (make == null || vin == null || odometer == null
                || costKm == null || costDay == null || status == null || type == null)
            {
                sendError = true;
                error = "The vehicle could not be added";
            }
            else
            {
                sendError = false;
                if (Vehicle.VehicleType != Vehicletypes.Motorcycle)
                {
                    CarCreator creator = new();
                    Vehicle = (Car)creator.FactoryMethod();
                    Vehicle.Id = _db.NextVehicleId;
                    Vehicle.VIN = vin;
                    Vehicle.VehicleType = (Vehicletypes)type;
                    Vehicle.Maker = make;
                    Vehicle.Odometer = (double)odometer;
                    Vehicle.PriceKm = (double)costKm;
                    Vehicle.Status = (VehicleStatuses)status;
                    _db.Add(Vehicle);
                }
                else
                {
                    MotorcycleCreator creator = new();
                    Vehicle = (Motorcycle)creator.FactoryMethod();
                    Vehicle.Id = _db.NextVehicleId;
                    Vehicle.VIN = vin;
                    Vehicle.VehicleType = (Vehicletypes)type;
                    Vehicle.Maker = make;
                    Vehicle.Odometer = (double)odometer;
                    Vehicle.PriceKm = (double)costKm;
                    Vehicle.Status = (VehicleStatuses)status;
                    _db.Add(Vehicle);
                }
            }
        }

        //Calling default interface methods
        public string[] VehicleStatusNames => _db.VehicleStatusNames();
        public string[] VehicleTypeNames => _db.VehicleTypeNames;
        //public string[] VehicleMakesNames => _db.VehicleMakeNames;
        public Vehicletypes GetVehicleType(string name) => _db.GetVehicleType(name);
    }
}
