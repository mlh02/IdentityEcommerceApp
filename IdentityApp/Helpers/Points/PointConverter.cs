using System;

namespace IdentityApp.Helpers.Points
{
    public static class PointConverter
    {
        public static double ConvertPoints(int userPoints)
        {
            var userDollars = userPoints * 0.6;
            return userDollars;
        }
        public static int ConvertMoneyToPoints(int refundAmount)
        {
            var userDollars = refundAmount / 0.6;
            return Convert.ToInt32(userDollars);
        }
    }
}
