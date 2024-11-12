using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Car_Rental.Data.Classes
{
    //Data to be returned to the bp
    //
    public class CollectionData : IData
    {
        //List
        readonly List<ICustomer> _persons = new();
        readonly List<IVehicle> _vehicles = new();
        readonly List<IBooking> _bookings = new();

        //Generate IDs
        public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
        public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(b => b.Id) + 1;
        public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;


        public CollectionData() => SeedData();
        public void SeedData()
        {
            //Add customers
            Customer customer1 = new(1, "Stina", "Isakson", 12345);
            //Customer customer2 = new(2, "Albert", "Hanson", 74025);
            //Customer customer3 = new(3, "Isak", "Johnson", 59476);

            //Add vehicles
            Car car1 = new(1, "abd231", "volvo", Vehicletypes.Combi, 10000, 1, 200, VehicleStatuses.Available);
            //Car car2 = new Car(2, "cef567", "saab", Vehicletypes.Sedan, 20000, 1, 100, VehicleStatuses.Available);
            //Car car3 = new Car(3, "ghi702", "tesla", Vehicletypes.Sedan, 1000, 3, 100, VehicleStatuses.Available);
            //Car car4 = new Car(4, "jkl542", "jeep", Vehicletypes.Van, 5000, 1.5, 300, VehicleStatuses.Available);
            //Motorcycle car5 = new Motorcycle(5, "MNO571", "Yamaha", Vehicletypes.Motorcycle, 30000, 0.5, 50, VehicleStatuses.Available);

            _vehicles.Add(car1);
            _persons.Add(customer1);
        }


        //In razor, display in dropdown:
        public string[] VehicleStatusNames() => Enum.GetNames(typeof(VehicleStatuses));
        public string[] VehicleTypeNames => Enum.GetNames(typeof(Vehicletypes));
        public static string[] VehicleMakeNames() => Enum.GetNames(typeof(Maker));

        public Vehicletypes GetVehicleType(string name) => (Vehicletypes)Enum.Parse(typeof(Vehicletypes), name);

        public FieldInfo FieldInfo<T>(T item)
        {
            FieldInfo? fieldInfo = GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                   .FirstOrDefault(f => f.FieldType == typeof(List<T>));
            if (fieldInfo == null && item == null)
            {
                throw new InvalidOperationException($"No list of type {typeof(T)} found.");
            }
            else
            {
                return fieldInfo;
            }
        }

        //Get
        public List<T> Get<T>(Expression<Func<T, bool>>? expression)
        {
            FieldInfo? fInfo = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>));
            if (fInfo is not null)
            {
                var list = (List<T>)fInfo.GetValue(this);
                if (expression is not null)
                    list = list.Where(expression.Compile()).ToList();
                return list;
            }
            else
                throw new InvalidOperationException($"No list of type {typeof(T)} found.");
        }

        //Add
        public void Add<T>(T item)
        {
            FieldInfo fieldInfo = FieldInfo(item);
            if (item is not null)
            {
                List<T> list = fieldInfo.GetValue(this) as List<T>;
                list.Add(item);
            }
            else
            {
                throw new Exception();
            }
        }

        //SINGLE
        public T? GetSingle<T>(Expression<Func<T, bool>>? expression)
        {
            FieldInfo? fieldInfo = FieldInfo(expression);
            var list = (List<T>)fieldInfo.GetValue(this);
            var item = list.SingleOrDefault(expression.Compile());
            return item;
        }

        // Rent och return vehicle
        public IBooking RentVehicle(int vehicleId, int customerId)
        {
            var newVehicle = _vehicles.SingleOrDefault(v => v.Id == vehicleId);
            var newCustomer = _persons.SingleOrDefault(c => c.Id == customerId);

            if (newVehicle is not null && newCustomer is not null)
            {
                Booking newBooking = new(NextBookingId, newCustomer, newVehicle);
                foreach (var booking in _bookings)
                {
                    if (booking.Vehicle.Status == VehicleStatuses.Booked)
                    {
                        newBooking.Vehicle.Status = VehicleStatuses.Available;
                    }
                    else
                    {
                        continue;
                    }
                }
                return newBooking;
            }
            else
                throw new Exception("Vehicle could not be rented");
        }
        public IBooking ReturnVehicle(int vehicleId)
        {
            try
            {
                var newBooking = GetSingle<IBooking>(b => b.Vehicle.Id == vehicleId && b.isOpen == true);

                if (newBooking is not null)
                    return newBooking;
                else
                {
                    ArgumentNullException argumentNullException1 = new("Booking could not be found");
                    throw argumentNullException1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
