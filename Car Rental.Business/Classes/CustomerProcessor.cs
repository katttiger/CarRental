using Car_Rental.Common.Classes;
using Car_Rental.Common.Enum;
using Car_Rental.Common.Interface;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;

namespace Car_Rental.Business.Classes
{
    public class CustomerProcessor
    {
        #region
        //Data and resources
        private readonly IData _db;
        public CustomerProcessor(IData db) => _db = db;

        public Customer Customer = new();

        public VehicleStatuses VehicleStatus { get; set; }

        public int NextVehicleId => throw new NotImplementedException();

        public int NextPersonId => throw new NotImplementedException();

        public int NextBookingId => throw new NotImplementedException();

        public string error = string.Empty;
        public bool sendError = false;
        #endregion

        //Customer
        //Get full list
        public IEnumerable<ICustomer> GetCustomers() => _db.Get<ICustomer>(null);
        //Get single
        public ICustomer? GetCustomer(string ssn)
        {
            var customer = _db.GetSingle<ICustomer>(s => s.SSN.Equals(ssn));
            if (customer == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                return customer;
            }
        }

        //Add customer
        public void AddCustomer(int? ssn, string? fName, string? lName)
        {
            if (ssn != 0 && ssn != null)
            {
                if (fName != null && lName != null)
                {
                    sendError = false;
                    Customer = new(_db.NextPersonId, fName, lName, (int)ssn);
                    _db.Add((ICustomer)Customer);
                    //edit = false;
                }
                else
                {
                    error = "Customer could not be added";
                    sendError = true;
                }
            }
            else
            {
                error = "SSN cannot be 0 or empty";
                sendError = true;
            }
        }

        public List<T> Get<T>(Expression<Func<T, bool>>? expression)
        {
            throw new NotImplementedException();
        }

        public T? GetSingle<T>(Expression<Func<T, bool>>? expression)
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T item)
        {
            throw new NotImplementedException();
        }

        public IBooking RentVehicle(int vehicleId, int customerId)
        {
            throw new NotImplementedException();
        }

        public IBooking ReturnVehicle(int vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
