namespace hrms.Domain.Models.User.AddNewUser
{
    public class AddNewUserProfilePersonalInfo
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PersonalNumber { get; set; }
        public int? GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
    }
}
