using Car_Rental.Common.Interface;

namespace Car_Rental.Common.Classes
{
    public class Customer : ICustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
        public Customer(string fname, string lname, int ssn) => (FirstName, LastName, SSN) = (fname, lname, ssn);
        public Customer(int id, string fname, string lname, int ssn) => (Id, FirstName, LastName, SSN) = (id, fname, lname, ssn);
        public Customer()
        {
             
        }
    }
}
