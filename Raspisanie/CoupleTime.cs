using SQLite;

namespace Raspisanie
{
    public class CoupleTime
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public TimeSpan Timespan { get; set; }
    }
}
