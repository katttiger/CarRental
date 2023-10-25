using Car_rental.ExtensionMethods;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Car_Rental.Data.Classes
{
    //Data to be returned to the bp
    public class CollectionData : IData
    {
        //List
        readonly List<ICustomer> _persons = new List<ICustomer>();
        readonly List<IVehicle> _vehicles = new List<IVehicle>();
        readonly List<IBooking> _bookings = new List<IBooking>();

        //Id of next element
        //=>if 0=>1, but if the Id == max, add 1 position.
        public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
        public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(b => b.Id) + 1;
        public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;

        //In razor, display in dropdown:
        public string[] VehicleStatusNames() => Enum.GetNames(typeof(VehicleStatuses));
        public string[] VehicleTypeNames => Enum.GetNames(typeof(Vehicletypes));
        public Vehicletypes GetVehicleType(string name) => (Vehicletypes)Enum.Parse(typeof(Vehicletypes), name);

        //Get
        public List<T> Get<T>(Expression<Func<T, bool>>? expression)
        {
            FieldInfo? fInfo = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>));
            if (fInfo is not null)
            {
                var list = (List<T>)fInfo.GetValue(this);
                if (expression is not null)
                {
                    list = list.Where(expression.Compile()).ToList();
                }
                return list;
            }
            else
                throw new InvalidOperationException($"No list of type {typeof(T)} found.");
        }

        //Add
        public void Add<T>(T item)
        {
            try
            {
                //Verkar inte hitta någonting. Är det fel på parametrarna?
                FieldInfo fInfo = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>));

                if (fInfo is not null && item is not null)
                {
                    var list = (List<T>)fInfo.GetValue(this);
                    list.Add(item);
                }
                else
                    throw new ArgumentNullException($"{typeof(T)} could not be added");

            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        //SINGLE
        public T? Single<T>(Expression<Func<T, bool>>? expression)
        {
            try
            {
                FieldInfo? fInfo = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                 .FirstOrDefault(f => f.FieldType == typeof(List<T>));
                ;

                if (fInfo is not null)
                {
                    var list = (List<T>)fInfo.GetValue(this);
                    var item = list.SingleOrDefault(expression.Compile());
                    return item;
                }
                else
                    throw new ArgumentNullException($"{typeof(T)} could not be found");
                
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        //Data att injicera i listorna
        public CollectionData() => SeedData();
        public void SeedData()
        {
            //Add customers
            Customer Cust1 = new Customer(1, "Stina", "Isakson", 12345);
            Customer Cust2 = new Customer(2, "Albert", "Hanson", 74025);
            Customer Cust3 = new Customer(3, "Isak", "Johnson", 59476);

            //Add vehicles
            Car car1 = new Car(1, "ABD231", "Volvo", Vehicletypes.Combi, 10000, 1, 200, VehicleStatuses.Available);
            Car car2 = new Car(2, "CEF567", "Saab", Vehicletypes.Sedan, 20000, 1, 100, VehicleStatuses.Available);
            Car car3 = new Car(3, "GHI702", "Tesla", Vehicletypes.Sedan, 1000, 3, 100, VehicleStatuses.Available);
            Car car4 = new Car(4, "JKL542", "Jeep", Vehicletypes.Van, 5000, 1.5, 300, VehicleStatuses.Available);
            Motorcycle car5 = new Motorcycle(5, "MNO571", "Yamaha", Vehicletypes.Motorcycle, 30000, 0.5, 50, VehicleStatuses.Available);

            _persons.Add(Cust1);
            _persons.Add(Cust2);
            _persons.Add(Cust3);

            _vehicles.Add(car1);
            _vehicles.Add(car2);
            _vehicles.Add(car3);
            _vehicles.Add(car4);
            _vehicles.Add(car5);

        }

        // Rent och return vehicle

        public IBooking RentVehicle(int vehicleId, int customerId)
        {
            var newV = _vehicles.SingleOrDefault(v => v.Id == vehicleId);
            var newC = _persons.SingleOrDefault(c => c.Id == customerId);
            //Skapa bokning
            Booking newB = new Booking(NextBookingId, newC, newV);
            if (newV is not null && newC is not null)
            {
                foreach (var b in _bookings)
                {
                    if (b.Vehicle.VIN == newB.VIN && b.Vehicle.Status == VehicleStatuses.Booked)
                    {
                        newB.Vehicle.Status = VehicleStatuses.Available;
                    }
                    else
                    {
                        continue;
                    }
                }
                return newB;
            }
            else
                throw new Exception("Vehicle could not be rented");
        }
        public IBooking ReturnVehicle(int vehicleId)
        {
            try
            {
                var newB = Single<IBooking>(b => b.Vehicle.Id == vehicleId && b.isOpen == true);

                if (newB is not null)
                    return newB;
                else
                    throw new ArgumentNullException("Booking could not be found");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
