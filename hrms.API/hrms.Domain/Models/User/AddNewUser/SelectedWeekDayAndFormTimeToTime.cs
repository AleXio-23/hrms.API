namespace hrms.Domain.Models.User.AddNewUser
{
    public class SelectedWeekDayAndFormTimeToTime
    {
        public int? WeekDayId { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

    }
}
