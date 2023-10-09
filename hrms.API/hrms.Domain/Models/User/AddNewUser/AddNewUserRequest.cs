namespace hrms.Domain.Models.User.AddNewUser
{
    public class AddNewUserRequest
    {
        public int? UserId { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public AddNewUserProfilePersonalInfo? PersonalInfo { get; set; }
        public AddNewUserProfilePosition? Position { get; set; }
        public List<SelectedWeekDayAndFormTimeToTime> WorkingSchedules { get; set; } = new List<SelectedWeekDayAndFormTimeToTime>();

    }
}
