using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Interface;
using Car_Rental.Common.Enum;


namespace Car_Rental.Data.Interfaces
{
    public interface IData
    {
        IEnumerable<ICustomer> GetPersons();
        IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default);
        IEnumerable<IBooking> GetBookings();
    }
}
