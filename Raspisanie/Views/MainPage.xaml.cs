using System.Windows.Input;

namespace Raspisanie;
public partial class MainPage : TabbedPage
{
    #region Свойства высот
    private int[] heightDay = { 0, 0, 0, 0, 0, 0 };
    public int HeightMonday
    {
        get => heightDay[0];
        set
        {
            heightDay[0] = value;
            OnPropertyChanged(nameof(HeightMonday));
        }
    }
    public int HeightTuesday
    {
        get => heightDay[1];
        set
        {
            heightDay[1] = value;
            OnPropertyChanged(nameof(HeightTuesday));
        }
    }
    public int HeightWednesday
    {
        get => heightDay[2];
        set
        {
            heightDay[2] = value;
            OnPropertyChanged(nameof(HeightWednesday));
        }
    }
    public int HeightThursday
    {
        get => heightDay[3];
        set
        {
            heightDay[3] = value;
            OnPropertyChanged(nameof(HeightThursday));
        }
    }
    public int HeightFriday
    {
        get => heightDay[4];
        set
        {
            heightDay[4] = value;
            OnPropertyChanged(nameof(HeightFriday));
        }
    }
    public int HeightSaturday
    {
        get => heightDay[5];
        set
        {
            heightDay[5] = value;
            OnPropertyChanged(nameof(HeightSaturday));
        }
    }
    #endregion
    #region Начальные значения времени пар
    public string[] InitialTimeCouple { get; set; } = { "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", };
    #endregion
    #region Команды
    public ICommand CommandVisibilityDay { get; set; }
    public ICommand CommandVisibilitySettingTimeCouple { get; set; }
    #endregion
    #region Выборка
    ItemCollection selectedMonday;
    ItemCollection selectedTuesday;
    ItemCollection selectedWednesday;
    ItemCollection selectedThursday;
    ItemCollection selectedFriday;
    ItemCollection selectedSaturday;
    #endregion
    #region Таймлайны
    TimeSpan[] TimeSpans { get; set; } = new TimeSpan[14];
    #endregion
    #region Свойства
    public string StatusDay { get; set; }
    public string[] TabbedItems { get; set; } = { "Расписание\nзанятий", "Расписание\nзвонков" };
    public static string[] TimeCouples { get; set; } = new string[14];
    private bool visibilitySettingTime = true;
    public bool VisibilitySettingTime
    {
        get => visibilitySettingTime;
        set
        {
            visibilitySettingTime = value;
            OnPropertyChanged(nameof(VisibilitySettingTime));
        }
    }
    #endregion

    public MainPage()
	{
		InitializeComponent();
        DayOfWeek dayOfWeek = DateTime.Today.DayOfWeek;
        CheckDayOfWeek(dayOfWeek);
        StatusDay = $"Текущая дата: {TimeNow()} ({TranslateDayOfWeek(dayOfWeek)})"; 

        CommandVisibilityDay = new CommandGetVisibility(VisibilityDay);
        CommandVisibilitySettingTimeCouple = new CommandGetVisibility(VisibilitySettingTimeCouple);

        #region Получаем базы
        App.DatabaseMonday.GetData();
        App.DatabaseTuesday.GetData();
        App.DatabaseWednesday.GetData();
        App.DatabaseThursday.GetData();
        App.DatabaseFriday.GetData();
        App.DatabaseSaturday.GetData();
        #endregion
        #region Получаем время из баз
        App.DatabaseStartCoupleOne.GetDataCoupleTime();
        App.DatabaseStartCoupleTwo.GetDataCoupleTime();
        App.DatabaseStartCoupleThree.GetDataCoupleTime();
        App.DatabaseStartCoupleFour.GetDataCoupleTime();
        App.DatabaseStartCoupleFive.GetDataCoupleTime();
        App.DatabaseStartCoupleSix.GetDataCoupleTime();
        App.DatabaseStartCoupleSeven.GetDataCoupleTime();
        App.DatabaseEndCoupleOne.GetDataCoupleTime();
        App.DatabaseEndCoupleTwo.GetDataCoupleTime();
        App.DatabaseEndCoupleThree.GetDataCoupleTime();
        App.DatabaseEndCoupleFour.GetDataCoupleTime();
        App.DatabaseEndCoupleFive.GetDataCoupleTime();
        App.DatabaseEndCoupleSix.GetDataCoupleTime();
        App.DatabaseEndCoupleSeven.GetDataCoupleTime();
        #endregion
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        #region Загрузка баз в коллекции
        collectionMonday.ItemsSource = await App.DatabaseMonday.GetData();
        collectionTuesday.ItemsSource = await App.DatabaseTuesday.GetData();
        collectionWednesday.ItemsSource = await App.DatabaseWednesday.GetData();
        collectionThursday.ItemsSource = await App.DatabaseThursday.GetData();
        collectionFriday.ItemsSource = await App.DatabaseFriday.GetData();
        collectionSaturday.ItemsSource = await App.DatabaseSaturday.GetData();
        #endregion
        #region
        oneStartCouple.Text = await GetStringLineStartOneCouple();
        oneEndCouple.Text = await GetStringLineEndOneCouple();
        twoStartCouple.Text = await GetStringLineStartTwoCouple();
        twoEndCouple.Text = await GetStringLineEndTwoCouple();
        threeStartCouple.Text = await GetStringLineStartThreeCouple();
        threeEndCouple.Text = await GetStringLineEndThreeCouple();
        fourStartCouple.Text = await GetStringLineStartFourCouple();
        fourEndCouple.Text = await GetStringLineEndFourCouple();
        fiveStartCouple.Text = await GetStringLineStartFiveCouple();
        fiveEndCouple.Text = await GetStringLineEndFiveCouple();
        sixStartCouple.Text = await GetStringLineStartSixCouple();
        sixEndCouple.Text = await GetStringLineEndSixCouple();
        sevenStartCouple.Text = await GetStringLineStartSevenCouple();
        sevenEndCouple.Text = await GetStringLineEndSevenCouple();
        #endregion
        #region
        TimeCouples[0] = await GetStringLineStartOneCouple();
        TimeCouples[1] = await GetStringLineEndOneCouple();
        TimeCouples[2] = await GetStringLineStartTwoCouple();
        TimeCouples[3] = await GetStringLineEndTwoCouple();
        TimeCouples[4] = await GetStringLineStartThreeCouple();
        TimeCouples[5] = await GetStringLineEndThreeCouple();
        TimeCouples[6] = await GetStringLineStartFourCouple();
        TimeCouples[7] = await GetStringLineEndFourCouple();
        TimeCouples[8] = await GetStringLineStartFiveCouple();
        TimeCouples[9] = await GetStringLineEndFiveCouple();
        TimeCouples[10] = await GetStringLineStartSixCouple();
        TimeCouples[11] = await GetStringLineEndSixCouple();
        TimeCouples[12] = await GetStringLineStartSevenCouple();
        TimeCouples[13] = await GetStringLineEndSevenCouple();
        #endregion
        #region
        TimeSpans[0] = await GetTimeLineStartOneCouple();
        TimeSpans[1] = await GetTimeLineEndOneCouple();
        TimeSpans[2] = await GetTimeLineStartTwoCouple();
        TimeSpans[3] = await GetTimeLineEndTwoCouple();
        TimeSpans[4] = await GetTimeLineStartThreeCouple();
        TimeSpans[5] = await GetTimeLineEndThreeCouple();
        TimeSpans[6] = await GetTimeLineStartFourCouple();
        TimeSpans[7] = await GetTimeLineEndFourCouple();
        TimeSpans[8] = await GetTimeLineStartFiveCouple();
        TimeSpans[9] = await GetTimeLineEndFiveCouple();
        TimeSpans[10] = await GetTimeLineStartSixCouple();
        TimeSpans[11] = await GetTimeLineEndSixCouple();
        TimeSpans[12] = await GetTimeLineStartSevenCouple();
        TimeSpans[13] = await GetTimeLineEndSevenCouple();
        #endregion
        #region Бесконечный цикл
        for (; ; )
        {
            if (DateTime.Now.TimeOfDay < TimeSpans[0])
                Status.Text = "1 пара начнется через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[0]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[0] && DateTime.Now.TimeOfDay < TimeSpans[1])
                Status.Text = "1 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[1]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[1] && DateTime.Now.TimeOfDay < TimeSpans[2])
                Status.Text = "Перемена. Начало 2 пары через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[2]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[2] && DateTime.Now.TimeOfDay < TimeSpans[3])
                Status.Text = "2 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[3]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[3] && DateTime.Now.TimeOfDay < TimeSpans[4])
                Status.Text = "Перемена. Начало 3 пары через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[4]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[4] && DateTime.Now.TimeOfDay < TimeSpans[5])
                Status.Text = "3 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[5]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[5] && DateTime.Now.TimeOfDay < TimeSpans[6])
                Status.Text = "Перемена. Начало 4 пары через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[6]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[6] && DateTime.Now.TimeOfDay < TimeSpans[7])
                Status.Text = "4 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[7]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[7] && DateTime.Now.TimeOfDay < TimeSpans[8])
                Status.Text = "Перемена. Начало 5 пары через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[8]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[8] && DateTime.Now.TimeOfDay < TimeSpans[9])
                Status.Text = "5 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[9]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[9] && DateTime.Now.TimeOfDay < TimeSpans[10])
                Status.Text = "Перемена. Начало 6 пары через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[10]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[10] && DateTime.Now.TimeOfDay < TimeSpans[11])
                Status.Text = "6 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[11]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[11] && DateTime.Now.TimeOfDay < TimeSpans[12])
                Status.Text = "Перемена. Начало 7 пары через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[12]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[12] && DateTime.Now.TimeOfDay < TimeSpans[13])
                Status.Text = "7 пара. Конец через: " + DateTime.Now.TimeOfDay.Subtract(TimeSpans[13]).Negate().ToString().Substring(0, 8);
            else if (DateTime.Now.TimeOfDay > TimeSpans[13])
                Status.Text = "Пары закончились";

            await Task.Delay(1000);
        }
        #endregion
    }

    #region ButtonКлики
    private async void AddInMonday(object sender, EventArgs e)
    {
        await App.DatabaseMonday.Save(new ItemCollection
        {
            Couple = await DisplayPromptAsync("Пара", "Введите название пары", "Добавить") + " ",
            Teacher = await DisplayPromptAsync("Преподаватель", "Введите преподавателя", "Добавить") + " ",
            Audience = await DisplayPromptAsync("Аудитория", "Введите аудиторию", "Добавить") + " "
        });
        collectionMonday.ItemsSource = await App.DatabaseMonday.GetData();
    }
    private async void AddInTuesday(object sender, EventArgs e)
    {
        await App.DatabaseTuesday.Save(new ItemCollection
        {
            Couple = await DisplayPromptAsync("Пара", "Введите название пары", "Добавить") + " ",
            Teacher = await DisplayPromptAsync("Преподаватель", "Введите преподавателя", "Добавить") + " ",
            Audience = await DisplayPromptAsync("Аудитория", "Введите аудиторию", "Добавить") + " "
        });
        collectionTuesday.ItemsSource = await App.DatabaseTuesday.GetData();
    }
    private async void AddInWednesday(object sender, EventArgs e)
    {
        await App.DatabaseWednesday.Save(new ItemCollection
        {
            Couple = await DisplayPromptAsync("Пара", "Введите название пары", "Добавить") + " ",
            Teacher = await DisplayPromptAsync("Преподаватель", "Введите преподавателя", "Добавить") + " ",
            Audience = await DisplayPromptAsync("Аудитория", "Введите аудиторию", "Добавить") + " "
        });
        collectionWednesday.ItemsSource = await App.DatabaseWednesday.GetData();
    }
    private async void AddInThursday(object sender, EventArgs e)
    {
        await App.DatabaseThursday.Save(new ItemCollection
        {
            Couple = await DisplayPromptAsync("Пара", "Введите название пары", "Добавить") + " ",
            Teacher = await DisplayPromptAsync("Преподаватель", "Введите преподавателя", "Добавить") + " ",
            Audience = await DisplayPromptAsync("Аудитория", "Введите аудиторию", "Добавить") + " "
        });
        collectionThursday.ItemsSource = await App.DatabaseThursday.GetData();
    }
    private async void AddInFriday(object sender, EventArgs e)
    {
        await App.DatabaseFriday.Save(new ItemCollection
        {
            Couple = await DisplayPromptAsync("Пара", "Введите название пары", "Добавить") + " ",
            Teacher = await DisplayPromptAsync("Преподаватель", "Введите преподавателя", "Добавить") + " ",
            Audience = await DisplayPromptAsync("Аудитория", "Введите аудиторию", "Добавить") + " "
        });
        collectionFriday.ItemsSource = await App.DatabaseFriday.GetData();
    }
    private async void AddInSaturday(object sender, EventArgs e)
    {
        await App.DatabaseSaturday.Save(new ItemCollection
        {
            Couple = await DisplayPromptAsync("Пара", "Введите название пары", "Добавить") + " ",
            Teacher = await DisplayPromptAsync("Преподаватель", "Введите преподавателя", "Добавить") + " ",
            Audience = await DisplayPromptAsync("Аудитория", "Введите аудиторию", "Добавить") + " "
        });
        collectionSaturday.ItemsSource = await App.DatabaseSaturday.GetData();
    }

    private async void DeleteMonday(object sender, EventArgs e)
    {
        if (selectedMonday != null)
        {
            await App.DatabaseMonday.Delete(selectedMonday);
            collectionMonday.ItemsSource = await App.DatabaseMonday.GetData();
            selectedMonday = null;
        }
        else await DisplayAlert("Сообщение", "Выберте пару для удаления", "Ok");
    }
    private async void DeleteTuesday(object sender, EventArgs e)
    {
        if (selectedTuesday != null)
        {
            await App.DatabaseTuesday.Delete(selectedTuesday);
            collectionTuesday.ItemsSource = await App.DatabaseTuesday.GetData();
            selectedTuesday = null;
        }
        else await DisplayAlert("Сообщение", "Выберте пару для удаления", "Ok");
    }
    private async void DeleteWednesday(object sender, EventArgs e)
    {
        if (selectedWednesday != null)
        {
            await App.DatabaseWednesday.Delete(selectedWednesday);
            collectionWednesday.ItemsSource = await App.DatabaseWednesday.GetData();
            selectedWednesday = null;
        }
        else await DisplayAlert("Сообщение", "Выберте пару для удаления", "Ok");
    }
    private async void DeleteThursday(object sender, EventArgs e)
    {
        if (selectedThursday != null)
        {
            await App.DatabaseThursday.Delete(selectedThursday);
            collectionThursday.ItemsSource = await App.DatabaseThursday.GetData();
            selectedThursday = null;
        }
        else await DisplayAlert("Сообщение", "Выберте пару для удаления", "Ok");
    }
    private async void DeleteFriday(object sender, EventArgs e)
    {
        if (selectedFriday != null)
        {
            await App.DatabaseFriday.Delete(selectedFriday);
            collectionFriday.ItemsSource = await App.DatabaseFriday.GetData();
            selectedFriday = null;
        }
        else await DisplayAlert("Сообщение", "Выберте пару для удаления", "Ok");
    }
    private async void DeleteSaturday(object sender, EventArgs e)
    {
        if (selectedSaturday != null)
        {
            await App.DatabaseSaturday.Delete(selectedSaturday);
            collectionSaturday.ItemsSource = await App.DatabaseSaturday.GetData();
            selectedSaturday = null;
        }
        else await DisplayAlert("Сообщение", "Выберте пару для удаления", "Ok");
    }
  
    #endregion
    #region Методы
    void CheckDayOfWeek(DayOfWeek dayOfWeek)
    {
        switch (dayOfWeek.ToString())
        {
            case "Monday":
                HeightMonday = 415;
                break;
            case "Tuesday":
                HeightTuesday = 415;
                break;
            case "Wednesday":
                HeightWednesday = 415;
                break;
            case "Thursday":
                HeightThursday = 415;
                break;
            case "Friday":
                HeightFriday = 415;
                break;
            case "Saturday":
                HeightSaturday = 415;
                break;
        }
    }
    string TranslateDayOfWeek(DayOfWeek dayOfWeek)
    {
        string nameDay = string.Empty;
        switch (dayOfWeek.ToString())
        {
            case "Monday":
                nameDay = "Понедельник";
                break;
            case "Tuesday":
                nameDay = "Вторник";
                break;
            case "Wednesday":
                nameDay = "Среда";
                break;
            case "Thursday":
                nameDay = "Четверг";
                break;
            case "Friday":
                nameDay = "Пятница";
                break;
            case "Saturday":
                nameDay = "Суббота";
                break;
            case "Sunday":
                nameDay = "Воскресенье";
                break;
        }
        return nameDay;
    }
    string TimeNow()
    {
        DateTime dateTime = DateTime.Now;
        return dateTime.ToString("dd.MM.yyyy");
    }
    void VisibilityDay(object parameter)
    {
        switch (parameter)
        {
            case "Понедельник":
                if (HeightMonday == 0) { HeightMonday = 415; }
                else HeightMonday = 0;
                HeightTuesday = 0;
                HeightWednesday = 0;
                HeightThursday = 0;
                HeightFriday = 0;
                HeightSaturday = 0;
                break;
            case "Вторник":
                if (HeightTuesday == 0) { HeightTuesday = 415; }
                else HeightTuesday = 0;
                HeightMonday = 0;
                HeightWednesday = 0;
                HeightThursday = 0;
                HeightFriday = 0;
                HeightSaturday = 0;
                break;
            case "Среда":
                if (HeightWednesday == 0) { HeightWednesday = 415; }
                else HeightWednesday = 0;
                HeightMonday = 0;
                HeightTuesday = 0;
                HeightThursday = 0;
                HeightFriday = 0;
                HeightSaturday = 0;
                break;
            case "Четверг":
                if (HeightThursday == 0) { HeightThursday = 415; }
                else HeightThursday = 0;
                HeightMonday = 0;
                HeightTuesday = 0;
                HeightWednesday = 0;
                HeightFriday = 0;
                HeightSaturday = 0;
                break;
            case "Пятница":
                if (HeightFriday == 0) { HeightFriday = 415; }
                else HeightFriday = 0;
                HeightMonday = 0;
                HeightTuesday = 0;
                HeightWednesday = 0;
                HeightThursday = 0;
                HeightSaturday = 0;
                break;
            case "Суббота":
                if (HeightSaturday == 0) { HeightSaturday = 415; }
                else HeightSaturday = 0;
                HeightMonday = 0;
                HeightTuesday = 0;
                HeightWednesday = 0;
                HeightThursday = 0;
                HeightFriday = 0;
                break;
        }
    }
    void VisibilitySettingTimeCouple(object parameter)
    {
        if (VisibilitySettingTime == false) { VisibilitySettingTime = true; }
        else { VisibilitySettingTime = false; }
    }
    #endregion
    #region События выборки
    private void SelectionChangedMonday(object sender, SelectionChangedEventArgs e) => selectedMonday = e.CurrentSelection[0] as ItemCollection;
    private void SelectionChangedTuesday(object sender, SelectionChangedEventArgs e) => selectedTuesday = e.CurrentSelection[0] as ItemCollection;
    private void SelectionChangedWednesday(object sender, SelectionChangedEventArgs e) => selectedWednesday = e.CurrentSelection[0] as ItemCollection;
    private void SelectionChangedThursday(object sender, SelectionChangedEventArgs e) => selectedThursday = e.CurrentSelection[0] as ItemCollection;
    private void SelectionChangedFriday(object sender, SelectionChangedEventArgs e) => selectedFriday = e.CurrentSelection[0] as ItemCollection;
    private void SelectionChangedSaturday(object sender, SelectionChangedEventArgs e) => selectedSaturday = e.CurrentSelection[0] as ItemCollection;
    #endregion
    #region ButtonКликиForGetStartTimeCouple
    async void ClickGetStartTimeOneCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начала первой пары", "Назначьте время первой пары.\nФормат 00:00");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleOne.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleOne.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[0] = await GetTimeLineStartOneCouple();
        oneStartCouple.Text = await GetStringLineStartOneCouple();
    }
    async void ClickGetStartTimeTwoCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начало второй пары", "Назначьте время второй пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleTwo.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleTwo.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[1] = await GetTimeLineStartTwoCouple();
        twoStartCouple.Text = await GetStringLineStartTwoCouple();
    }
    async void ClickGetStartTimeThreeCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начало третьей пары", "Назначьте время третьей пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleThree.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleThree.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[2] = await GetTimeLineStartThreeCouple();
        threeStartCouple.Text = await GetStringLineStartThreeCouple();
    }
    async void ClickGetStartTimeFourCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начало четвёртой пары", "Назначьте время четвёртой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleFour.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleFour.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[3] = await GetTimeLineStartFourCouple();
        fourStartCouple.Text = await GetStringLineStartFourCouple();
    }
    async void ClickGetStartTimeFiveCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начало пятой пары", "Назначьте время пятой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleFive.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleFive.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[4] = await GetTimeLineStartFiveCouple();
        fiveStartCouple.Text = await GetStringLineStartFiveCouple();
    }
    async void ClickGetStartTimeSixCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начало шестой пары", "Назначьте время шестой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleSix.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleSix.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[5] = await GetTimeLineStartSixCouple();
        sixStartCouple.Text = await GetStringLineStartSixCouple();
    }
    async void ClickGetStartTimeSevenCouple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время начало седьмой пары", "Назначьте время седьмой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseStartCoupleSeven.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseStartCoupleSeven.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[6] = await GetTimeLineStartSevenCouple();
        sevenStartCouple.Text = await GetStringLineStartSevenCouple();
    }
    #endregion
    #region ButtonКликиForGetEndTimeCouple
    async void ClickGetEndTimeOneCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания первой пары", "Назначьте время первой пары.\nФормат 00:00");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleOne.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleOne.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[7] = await GetTimeLineEndOneCouple();
        oneEndCouple.Text = await GetStringLineEndOneCouple();
    }
    async void ClickGetEndTimeTwoCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания второй пары", "Назначьте время второй пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleTwo.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleTwo.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[8] = await GetTimeLineEndTwoCouple();
        twoEndCouple.Text = await GetStringLineEndTwoCouple();
    }
    async void ClickGetEndTimeThreeCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания третьей пары", "Назначьте время третьей пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleThree.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleThree.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[9] = await GetTimeLineEndThreeCouple();
        threeEndCouple.Text = await GetStringLineEndThreeCouple();
    }
    async void ClickGetEndTimeFourCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания четвёртой пары", "Назначьте время четвёртой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleFour.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleFour.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[10] = await GetTimeLineEndFourCouple();
        fourEndCouple.Text = await GetStringLineEndFourCouple();
    }
    async void ClickGetEndTimeFiveCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания пятой пары", "Назначьте время пятой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleFive.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleFive.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[11] = await GetTimeLineEndFiveCouple();
        fiveEndCouple.Text = await GetStringLineEndFiveCouple();
    }
    async void ClickGetEndTimeSixCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания шестой пары", "Назначьте время шестой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleSix.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleSix.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[12] = await GetTimeLineEndSixCouple();
        sixEndCouple.Text = await GetStringLineEndSixCouple();
    }
    async void ClickGetEndTimeSevenCopuple(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Время окончания седьмой пары", "Назначьте время седьмой пары.\nФормат 00:00", "Ok");
        TimeSpan time = TimeSpan.Zero;
        await App.DatabaseEndCoupleSeven.DeleteAll();
        if (result != null && TimeSpan.TryParse(result, out time))
        {
            await App.DatabaseEndCoupleSeven.Save(new CoupleTime
            {
                Timespan = time
            });
        }
        TimeSpans[13] = await GetTimeLineEndSevenCouple();
        sevenEndCouple.Text = await GetStringLineEndSevenCouple();
    }
    #endregion
    #region АсинкМетодыПреобразованияStartAndEnd
    async Task<TimeSpan> GetTimeLineStartOneCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleOne.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if(list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineStartTwoCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleTwo.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineStartThreeCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleThree.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineStartFourCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleFour.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineStartFiveCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleFive.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineStartSixCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleSix.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineStartSevenCouple()
    {
        List<CoupleTime> list = await App.DatabaseStartCoupleSeven.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    
    async Task<TimeSpan> GetTimeLineEndOneCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleOne.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineEndTwoCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleTwo.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineEndThreeCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleThree.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineEndFourCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleFour.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineEndFiveCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleFive.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineEndSixCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleSix.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    async Task<TimeSpan> GetTimeLineEndSevenCouple()
    {
        List<CoupleTime> list = await App.DatabaseEndCoupleSeven.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (list.Count > 0) { timeline = list.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline;
    }
    #endregion
    #region GetStringFromTimeSpan
    async Task<string> GetStringLineStartOneCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleOne.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0,5);
    }
    async Task<string> GetStringLineStartTwoCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleTwo.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    async Task<string> GetStringLineStartThreeCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleThree.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    async Task<string> GetStringLineStartFourCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleFour.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    async Task<string> GetStringLineStartFiveCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleFive.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    async Task<string> GetStringLineStartSixCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleSix.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    async Task<string> GetStringLineStartSevenCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseStartCoupleSeven.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }

     async Task<string> GetStringLineEndOneCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleOne.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
     async Task<string> GetStringLineEndTwoCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleTwo.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
     async Task<string> GetStringLineEndThreeCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleThree.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
     async Task<string> GetStringLineEndFourCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleFour.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
     async Task<string> GetStringLineEndFiveCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleFive.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
     async Task<string> GetStringLineEndSixCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleSix.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    async Task<string> GetStringLineEndSevenCouple()
    {
        List<CoupleTime> liststring = await App.DatabaseEndCoupleSeven.GetDataCoupleTime();
        TimeSpan timeline = TimeSpan.Zero;
        if (liststring.Count > 0) { timeline = liststring.Select(p => p.Timespan).Aggregate((ts1, ts2) => ts1 + ts2); }
        return timeline.ToString().Substring(0, 5);
    }
    #endregion
}