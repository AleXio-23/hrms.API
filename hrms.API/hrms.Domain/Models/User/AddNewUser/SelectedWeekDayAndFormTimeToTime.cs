namespace hrms.Domain.Models.User.AddNewUser
{
    public class SelectedWeekDayAndFormTimeToTime
    {
        public int? WeekDayId { get; set; }
        public TimeOnly TimeFrom { get; set; }
        public TimeOnly TimeTo { get; set; }

    }
}
