using CarRental.Common.Enum;
using CarRental.Common.Interface;
using System.Linq.Expressions;

namespace CarRental.Data.Interfaces
{
    public interface IData
    {
        List<T> Get<T>(Expression<Func<T, bool>>? expression);
        T? GetSingle<T>(Expression<Func<T, bool>>? expression);
        public void Add<T>(T item);

        int NextVehicleId { get; }
        int NextPersonId { get; }
        int NextBookingId { get; }

        IBooking RentVehicle(int vehicleId, int customerId);
        IBooking ReturnVehicle(int vehicleId);


        //Default interface methods
        public string[] VehicleStatusNames() => Enum.GetNames<VehicleStatuses>();
        public string[] VehicleTypeNames => Enum.GetNames<Vehicletypes>();
        public Vehicletypes GetVehicleType(string name) => (Vehicletypes)Enum.Parse(typeof(Vehicletypes), name);
    }
}
