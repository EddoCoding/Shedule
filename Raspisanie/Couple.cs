using SQLite;

namespace Raspisanie
{
    public class Couple
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string IntCouple { get ; set; }
        public string Time { get; set;}
    }
}
