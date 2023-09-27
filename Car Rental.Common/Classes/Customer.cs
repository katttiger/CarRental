using Car_Rental.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes
{
    public class Customer : ICustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
        public Customer(string fname, string lname, int ssn)
        {
            FirstName = fname;
            LastName = lname;
            SSN=ssn;
        }
    }
}
