namespace hrms.Domain.Models.Vacations.GetAllLeavesForUser
{
    public class GetAllLeavesResponse
    {
        public List<GetAllLeavesData> RecordsData { get; set; }
        public int RecordsTotal { get; set; } = 0;
    }
}
