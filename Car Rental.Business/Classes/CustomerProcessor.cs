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
    public class CustomerProcessor
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
        //Customer
        //Get full list
        public IEnumerable<ICustomer> GetCustomers() => _db.Get<ICustomer>(null);
        //Get single
        public ICustomer? GetCustomer(string ssn)
        {
            var customer = _db.Single<ICustomer>(s => s.SSN.Equals(ssn));
            try
            {
                if (customer is null)
                    throw new ArgumentNullException();
                return customer;
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        //Add customer
        public void AddCustomer(int ssn, string fName, string lName)
        {
            if (ssn != 0)
            {
                if (_db.NextPersonId != null && fName != null && lName != null && ssn != null)
                {
                    sendError = false;
                    Customer = new(_db.NextPersonId, fName, lName, ssn);
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
    }
}
