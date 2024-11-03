using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Shedule.ViewModels
{
    public class VisualMainViewModel : ReactiveObject
    {
        [Reactive] public bool VisibilityMonday { get; set; }
        [Reactive] public bool VisibilityTuesday { get; set; }
        [Reactive] public bool VisibilityWednesday { get; set; }
        [Reactive] public bool VisibilityThursday { get; set; }
        [Reactive] public bool VisibilityFriday { get; set; }
        [Reactive] public bool VisibilitySaturday { get; set; }
        [Reactive] public bool VisibilitySunday { get; set; }

        public void ShowCloseShedule(string day)
        {
            if (day == "Понедельник") VisibilityMonday = !VisibilityMonday;
            else if (day == "Вторник") VisibilityTuesday = !VisibilityTuesday;
            else if (day == "Среда") VisibilityWednesday = !VisibilityWednesday;
            else if (day == "Четверг") VisibilityThursday = !VisibilityThursday;
            else if (day == "Пятница") VisibilityFriday = !VisibilityFriday;
            else if (day == "Суббота") VisibilitySaturday = !VisibilitySaturday;
            else if (day == "Воскресенье") VisibilitySunday = !VisibilitySunday;
        }
    }
}