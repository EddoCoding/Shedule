namespace Shedule.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        public string Where { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
    }
}