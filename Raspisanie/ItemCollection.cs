using SQLite;

namespace Raspisanie
{
    public class ItemCollection : INPC
    {
        public string starttime = "";
        public string endtime = "";

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Couple { get; set; }
        public string Teacher { get; set; }
        public string Audience { get; set; }
        public string StartTime
        {
            get => starttime;
            set
            {

                switch(Couple[0])
                {
                    case '1': starttime = MainPage.TimeCouples[0];
                        break;
                    case '2': starttime = MainPage.TimeCouples[2];
                        break;
                    case '3': starttime = MainPage.TimeCouples[4];
                        break;
                    case '4': starttime = MainPage.TimeCouples[6];
                        break;
                    case '5': starttime = MainPage.TimeCouples[8];
                        break;
                    case '6': starttime = MainPage.TimeCouples[10];
                        break;
                    case '7': starttime = MainPage.TimeCouples[12];
                        break;
                }
            }
        }
        public string EndTime
        {
            get => endtime;
            set
            {
                switch (Couple[0])
                {
                    case '1':
                        endtime = MainPage.TimeCouples[1];
                        break;
                    case '2':
                        endtime = MainPage.TimeCouples[3];
                        break;
                    case '3':
                        endtime = MainPage.TimeCouples[5];
                        break;
                    case '4':
                        endtime = MainPage.TimeCouples[7];
                        break;
                    case '5':
                        endtime = MainPage.TimeCouples[9];
                        break;
                    case '6':
                        endtime = MainPage.TimeCouples[11];
                        break;
                    case '7':
                        endtime = MainPage.TimeCouples[13];
                        break;
                }
            }
        }
    }
}