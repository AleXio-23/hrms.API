namespace hrms.Domain.Models.User
{
    public class UsersWorkScheduleDTO
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public int? WeekWorkingDayId { get; set; } 

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }
    }
}
