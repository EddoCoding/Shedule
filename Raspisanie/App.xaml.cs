namespace Raspisanie;

public partial class App : Application
{
    private static Base databaseMonday, databaseTuesday, databaseWednesday, databaseThursday, databaseFriday, databaseSaturday,
        databaseZvonki, databaseCoupleOne, databaseCoupleTwo, databaseCoupleThree, databaseCoupleFour, databaseCoupleFive, databaseCoupleSix, databaseCoupleSeven,
        databaseEndCoupleOne, databaseEndCoupleTwo, databaseEndCoupleThree, databaseEndCoupleFour, databaseEndCoupleFive, databaseEndCoupleSix, databaseEndCoupleSeven;

    public static Base DatabaseMonday
    {
        get
        {
            if (databaseMonday == null) { databaseMonday = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseMonday.db3")); }
            return databaseMonday;
        }
    }
    public static Base DatabaseTuesday
    {
        get
        {
            if (databaseTuesday == null) { databaseTuesday = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseTuesday.db3")); }
            return databaseTuesday;
        }
    }
    public static Base DatabaseWednesday
    {
        get
        {
            if (databaseWednesday == null) { databaseWednesday = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseWednesday.db3")); }
            return databaseWednesday;
        }
    }
    public static Base DatabaseThursday
    {
        get
        {
            if (databaseThursday == null) { databaseThursday = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseThursday.db3")); }
            return databaseThursday;
        }
    }
    public static Base DatabaseFriday
    {
        get
        {
            if (databaseFriday == null) { databaseFriday = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseFriday.db3")); }
            return databaseFriday;
        }
    }
    public static Base DatabaseSaturday
    {
        get
        {
            if (databaseSaturday == null) { databaseSaturday = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseSaturday.db3")); }
            return databaseSaturday;
        }
    }
    public static Base DatabaseZvonki
    {
        get
        {
            if (databaseZvonki == null) { databaseZvonki = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseZvonki.db3")); }
            return databaseZvonki;
        }
    }
    public static Base DatabaseStartCoupleOne
    {
        get
        {
            if (databaseCoupleOne == null) { databaseCoupleOne = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleOne.db3")); }
            return databaseCoupleOne;
        }
    }
    public static Base DatabaseStartCoupleTwo
    {
        get
        {
            if (databaseCoupleTwo == null) { databaseCoupleTwo = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleTwo.db3")); }
            return databaseCoupleTwo;
        }
    }
    public static Base DatabaseStartCoupleThree
    {
        get
        {
            if (databaseCoupleThree == null) { databaseCoupleThree = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleThree.db3")); }
            return databaseCoupleThree;
        }
    }
    public static Base DatabaseStartCoupleFour
    {
        get
        {
            if (databaseCoupleFour == null) { databaseCoupleFour = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleFour.db3")); }
            return databaseCoupleFour;
        }
    }
    public static Base DatabaseStartCoupleFive
    {
        get
        {
            if (databaseCoupleFive == null) { databaseCoupleFive = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleFive.db3")); }
            return databaseCoupleFive;
        }
    }
    public static Base DatabaseStartCoupleSix
    {
        get
        {
            if (databaseCoupleSix == null) { databaseCoupleSix = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleSix.db3")); }
            return databaseCoupleSix;
        }
    }
    public static Base DatabaseStartCoupleSeven
    {
        get
        {
            if (databaseCoupleSeven == null) { databaseCoupleSeven = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseCoupleSeven.db3")); }
            return databaseCoupleSeven;
        }
    }
    public static Base DatabaseEndCoupleOne
    {
        get
        {
            if (databaseEndCoupleOne == null) { databaseEndCoupleOne = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleOne.db3")); }
            return databaseEndCoupleOne;
        }
    }
    public static Base DatabaseEndCoupleTwo
    {
        get
        {
            if (databaseEndCoupleTwo == null) { databaseEndCoupleTwo = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleTwo.db3")); }
            return databaseEndCoupleTwo;
        }
    }
    public static Base DatabaseEndCoupleThree
    {
        get
        {
            if (databaseEndCoupleThree == null) { databaseEndCoupleThree = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleThree.db3")); }
            return databaseEndCoupleThree;
        }
    }
    public static Base DatabaseEndCoupleFour
    {
        get
        {
            if (databaseEndCoupleFour == null) { databaseEndCoupleFour = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleFour.db3")); }
            return databaseEndCoupleFour;
        }
    }
    public static Base DatabaseEndCoupleFive
    {
        get
        {
            if (databaseEndCoupleFive == null) { databaseEndCoupleFive = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleFive.db3")); }
            return databaseEndCoupleFive;
        }
    }
    public static Base DatabaseEndCoupleSix
    {
        get
        {
            if (databaseEndCoupleSix == null) { databaseEndCoupleSix = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleSix.db3")); }
            return databaseEndCoupleSix;
        }
    }
    public static Base DatabaseEndCoupleSeven
    {
        get
        {
            if (databaseEndCoupleSeven == null) { databaseEndCoupleSeven = new Base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEndCoupleSeven.db3")); }
            return databaseEndCoupleSeven;
        }
    }

    public App()
	{
		InitializeComponent();
		MainPage = new MainPage() { BindingContext = new MainPage() };
	}
}