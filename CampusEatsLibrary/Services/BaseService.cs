namespace CampusEatsLibrary.Services
{
    public class BaseService : IBaseService
    {
        public DateTime GetCurrentDate()
        {
            TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime currentTimeInZone7 = TimeZoneInfo.ConvertTime(DateTime.Now, selectedTimeZone);

            return currentTimeInZone7;
        }
    }
}
