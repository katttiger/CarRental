using Car_Rental.Common.Classes;
using System.Runtime.CompilerServices;

namespace Car_rental.ExtensionMethods
{
    public class VExtensions
    {
        public static int Duration(DateTime dRent, DateTime dReturn)
        {
            int _totalDays = 0;
            var tDays = (dReturn - dRent).TotalDays;
            if (tDays < 2)
                _totalDays = 1;
            else
                _totalDays = (int)tDays;
            return _totalDays;

        }
        public static double KmDriven(double kmDriven, double CostKm)
        {
            return kmDriven * CostKm;
        }
        public static double DaysDriven(double daysHired, double costDay)
        {
            return daysHired * costDay;
        }    }
}
