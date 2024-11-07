using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Classes
{
    public class VehicleProcessor
    {
        #region
        //Data and resources
        private readonly IData _db;

        public Customer Customer = new();
        public Vehicle Vehicle = new();
        public Booking Booking = new();

        public VehicleStatuses vehicleStatus { get; set; }

        public string error = string.Empty;
        public bool sendError = false;
        public bool hiring = false;
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
                vehicleStatus = status;
                var vehicle = list;

                if (!vehicleStatus.Equals(VehicleStatuses.Available) && !vehicleStatus.Equals(VehicleStatuses.Booked))
                    return vehicle;
                else
                {
                    return vehicle.Where(v => v.Status.Equals(vehicleStatus));
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        //Get single
        public IVehicle? GetVehicle(int vehicleId)
        {
            return _db.Single<IVehicle>(v => v.Id.Equals(vehicleId));
        }
        //Get single
        public IVehicle? GetVehicle(string vin)
        {
            return _db.Single<IVehicle>(t => t.VIN.Equals(vin));
        }

        //Add vehicle/customer
        //add vehicle
        public void AddVehicle(string make, string vin, double odometer,
            double costKm, double costDay, VehicleStatuses status, Vehicletypes type)
        {
            if (make != null && vin != null && odometer != null
                && costKm != null && costDay != null && status != null && type != null)
            {
                sendError = false;
                if (Vehicle.VehicleType != Vehicletypes.Motorcycle)
                {
                    Vehicle = new Car(_db.NextVehicleId, vin, make, type, odometer, costKm, costDay, status);
                    _db.Add((IVehicle)Vehicle);
                }
                else
                {
                    Vehicle = new Motorcycle(_db.NextVehicleId, vin, make, type, odometer, costKm, costDay, status);
                    _db.Add((IVehicle)Vehicle);
                }
            }
            else
            {
                sendError = true;
                error = "The vehicle could not be added";
            }
        }
        //Calling default interface methods
        public string[] VehicleStatusNames => _db.VehicleStatusNames();
        public string[] VehicleTypeNames => _db.VehicleTypeNames;
        //public string[] VehicleMakesNames => _db.VehicleMakeNames;
        public Vehicletypes GetVehicleType(string name) => _db.GetVehicleType(name);
    }
}
