using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Interface
{
    public interface ICustomer
    {
        int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
    }
}
