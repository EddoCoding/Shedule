namespace Shedule.Models
{
    public class Call
    {
        public Guid Id { get; set; }
        public int NumberPara { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}