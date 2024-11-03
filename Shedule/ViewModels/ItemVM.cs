using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Shedule.ViewModels
{
    public class ItemVM : ReactiveObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Reactive] public string Subject { get; set; } = string.Empty;
        [Reactive] public string Teacher { get; set; } = string.Empty;
        [Reactive] public string Where { get; set; } = string.Empty;
        public TimeOnly StartTimeLesson { get; set; }
        public TimeOnly EndTimeLesson { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;

        public bool Validation()
        {
            if (String.IsNullOrWhiteSpace(Subject))
            {
                Application.Current.MainPage.DisplayAlert("Валидация", "Введите название предмета", "Ok");
                return false;
            }
            if (String.IsNullOrWhiteSpace(Teacher))
            {
                Application.Current.MainPage.DisplayAlert("Валидация", "Введите ФИО преподавателя", "Ok");
                return false;
            }

            return true;
        }
    }
}