namespace EmployeeApplication.Models
{
    public class HolidaysEntity
    {
        public int HolidayId { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
    }
}
