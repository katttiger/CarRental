using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Data.Classes
{
    //Data to be returned to the bp
    public class CollectionData : IData
    {
        //----------List-------------
        public readonly List<ICustomer> _persons = new List<ICustomer>();
        public readonly List<IVehicle> _vehicles = new List<IVehicle>();
        public readonly List<IBooking> _bookings = new List<IBooking>();

        //----------------GetItem metoder m.h.a. interface-------
        public IEnumerable<ICustomer> GetPersons() => _persons;
        public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) => _vehicles;
        public IEnumerable<IBooking> GetBookings() => _bookings;

        //------------AddItem metoder m.h.a interface-----------------
        public List<IVehicle> AddVehicles(IVehicle vehicle)
        {
            _vehicles.Add(vehicle);
            return new List<IVehicle>();
        }
        public List<ICustomer> AddCustomer(ICustomer customer)
        {
            _persons.Add(customer);
            return new List<ICustomer>();

        }
        public List<IBooking> AddBookings(IBooking booking)
        {
            _bookings.Add(booking);
            return new List<IBooking>();
        }
        //---------------------------------------------

        //Data att injicera i listorna
        public CollectionData() => SeedData();
        public void SeedData()
        {
            //Add customers
            Customer Cust1 = new Customer("Stina", "Isakson", 12345);
            Customer Cust2 = new Customer("Albert", "Hanson", 74025);
            Customer Cust3 = new Customer("Isak", "Johnson", 59476);

            //Add vehicles
            Car car1 = new Car("ABD231", "Volvo", Vehicletypes.Combi, 10000, 1, 200, VehicleStatuses.Available);
            Car car2 = new Car("CEF567", "Saab", Vehicletypes.Sedan, 20000, 1, 100, VehicleStatuses.Available);
            Car car3 = new Car("GHI702", "Tesla", Vehicletypes.Sedan, 1000, 3, 100, VehicleStatuses.Available);
            Car car4 = new Car("JKL542", "Jeep", Vehicletypes.Van, 5000, 1.5, 300, VehicleStatuses.Available);
            Motorcycle car5 = new Motorcycle("MNO571", "Yamaha", Vehicletypes.Motorcycle, 30000, 0.5, 50, VehicleStatuses.Available);

            //Add bookings
            Booking b1 = new Booking(Cust2, car2);
            Booking b2 = new Booking(Cust1, car3);

            _persons.Add(Cust1);
            _persons.Add(Cust2);
            _persons.Add(Cust3);

            _vehicles.Add(car1);
            _vehicles.Add(car2);
            _vehicles.Add(car3);
            _vehicles.Add(car4);
            _vehicles.Add(car5);

            _bookings.Add(b1);
            _bookings.Add(b2);

            b2.ReturnVehicle(car3, 2000);

        }
    }
}
