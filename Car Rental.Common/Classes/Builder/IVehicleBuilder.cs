using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes.Builder
{
    public interface IVehicleBuilder
    {
        void Reset() { }
        void BuildVIN() { }
        void BuildMaker() { }
        void BuildOdometer() { }
        void BuildPriceKm() { }
        void PriceDay() { }
        void BuildVehicleType() { }
        void BuildVehicleStatus() { }
    }

}
