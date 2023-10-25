using Car_Rental.Common.Enum;
using System.Runtime.CompilerServices;

namespace Car_rental.ExtensionMethods
{
    public class VExtentions
    {
        public static int Duration(DateTime dRent, DateTime dReturn, int CostDay)
        {

            int _totalDays = 0;
            var tDays = (dReturn - dRent).TotalDays;
            if (tDays < 1)
                _totalDays = 1;
            else
                _totalDays = (int)tDays;
            var cost = _totalDays * CostDay;
            return cost;
        }

        public static double PriceKm(double odometer, double distance, double CostKm)
        {
            double drivenKm = distance - odometer;
            if (drivenKm > odometer)
            {
                drivenKm = distance;
            }
            else
            {
                drivenKm = 0;
            }
            double cost =drivenKm*CostKm;
            return cost;
        }
    }
}
