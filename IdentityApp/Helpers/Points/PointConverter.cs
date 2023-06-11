namespace IdentityApp.Helpers.Points
{
    public static class PointConverter
    {
        public static double ConvertPoints(int userPoints)
        {
            var userDollars = userPoints * 0.6;
            return userDollars;
        }
    }
}
