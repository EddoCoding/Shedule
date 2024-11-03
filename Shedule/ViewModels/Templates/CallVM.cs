using ReactiveUI;

namespace Shedule.ViewModels.Templates
{
    public class CallVM : ReactiveObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int NumberPara { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool Validation()
        {
            if(NumberPara < 1 || NumberPara > 10)
            {
                Application.Current.MainPage.DisplayAlert("Валидация", "Номер пары должен быть от 1 до 10", "Ok");
                return false;
            }
            if (StartTime == default || StartTime >= EndTime)
            {
                Application.Current.MainPage.DisplayAlert("Валидация", "Некорректное значение начала пары", "Ok");
                return false;
            }
            if (EndTime == default || EndTime <= StartTime)
            {
                Application.Current.MainPage.DisplayAlert("Валидация", "Некорректное значение конца пары", "Ok");
                return false;
            }

            return true;
        }
    }
}