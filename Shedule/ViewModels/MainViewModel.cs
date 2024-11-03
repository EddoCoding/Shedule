using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shedule.Common;
using Shedule.Common.Repositories;
using Shedule.Models;
using Shedule.ViewModels.Templates;
using Shedule.Views;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Shedule.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        DataContext dataContext = new DataContext();
        ISheduleRepository sheduleRepository = new SheduleRepository();

        [Reactive] public VisualMainViewModel Visual { get; set; } = new();

        public DateOnly CurrentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Reactive] public string Status { get; set; } = string.Empty;

        public List<CallVM> Calls { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleMonday { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleTuesday { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleWednesday { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleThursday { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleFriday { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleSaturday { get; set; } = new();
        public ObservableCollection<ItemVM> SheduleSunday { get; set; } = new();

        [Reactive] public ItemVM SelectedItem { get; set; }

        public ReactiveCommand<string, Unit> ShowCloseSheduleCommand { get; set; }
        public ReactiveCommand<string, Unit> OpenPageAddItemCommand { get; set; }
        public ReactiveCommand<ItemVM,Unit> DeleteItemCommand { get; set; }

        public MainViewModel()
        {
            dataContext.Database.EnsureCreated();

            ShowShedule();
            GetCalls();
            GetDataShedule();

            ShowCloseSheduleCommand = ReactiveCommand.Create<string>(Visual.ShowCloseShedule);
            OpenPageAddItemCommand = ReactiveCommand.Create<string>(OpenPageAddItem);
            DeleteItemCommand = ReactiveCommand.Create<ItemVM>(DeleteItem);

            //Очко
            Task.Run(async () =>
            {
                while (true)
                {
                    bool foundPara = false;
                    TimeSpan? TimeRemaining = null;

                    foreach (var para in Calls)
                    {
                        if (DateTime.Now.TimeOfDay > para.StartTime && DateTime.Now.TimeOfDay < para.EndTime)
                        {
                            TimeSpan remainingTime = para.EndTime - DateTime.Now.TimeOfDay;
                            string formattedTime = string.Format("{0:D2}:{1:D2}", (int)remainingTime.TotalHours, remainingTime.Minutes);

                            Status = $"Идёт {para.NumberPara} пара. До конца пары осталось: {formattedTime}.";
                            foundPara = true;
                            break;
                        }

                        else if (!foundPara && para.StartTime > DateTime.Now.TimeOfDay)
                        {
                            TimeRemaining = para.StartTime - DateTime.Now.TimeOfDay;
                            break;
                        }
                    }

                    if (!foundPara)
                    {
                        bool isAfterLastCall = true;
                        for (int i = 0; i < Calls.Count; i++)
                        {
                            if (DateTime.Now.TimeOfDay > Calls[i].StartTime && DateTime.Now.TimeOfDay < Calls[i].EndTime)
                            {
                                TimeSpan remainingTime = Calls[i].EndTime - DateTime.Now.TimeOfDay;
                                string formattedTime = string.Format("{0:D2}:{1:D2}", (int)remainingTime.TotalHours, remainingTime.Minutes);

                                Status = $"Идёт {Calls[i].NumberPara} пара. До конца пары осталось: {formattedTime}.";
                                foundPara = true;
                                break;
                            }
                            else if (DateTime.Now.TimeOfDay > Calls[i].EndTime)
                            {
                                if (i == Calls.Count - 1)
                                {
                                    Status = "Пары закончились.";
                                    foundPara = true;
                                    break;
                                }
                                else isAfterLastCall = false;
                            }
                        }

                        if (!foundPara && isAfterLastCall)
                        {
                            Status = "Пары закончились.";
                        }
                        else if (!foundPara && !isAfterLastCall)
                        {
                            for (int i = 0; i < Calls.Count - 1; i++)
                            {
                                if (DateTime.Now.TimeOfDay > Calls[i].EndTime && DateTime.Now.TimeOfDay < Calls[i + 1].StartTime)
                                {
                                    TimeSpan timeUntilNextPara = Calls[i + 1].StartTime - DateTime.Now.TimeOfDay;
                                    string formattedTime = string.Format("{0:D2}:{1:D2}", (int)timeUntilNextPara.TotalHours, timeUntilNextPara.Minutes);

                                    Status = $"Перемена! До начала {Calls[i + 1].NumberPara} пары осталось: {formattedTime}.";
                                    foundPara = true;
                                    break;
                                }
                            }
                        }
                        if (!foundPara)
                        {
                            if (Calls.Count > 0 && DateTime.Now.TimeOfDay < Calls[0].StartTime)
                            {
                                TimeSpan timeUntilFirstPara = Calls[0].StartTime - DateTime.Now.TimeOfDay;
                                string formattedTime = string.Format("{0:D2}:{1:D2}", (int)timeUntilFirstPara.TotalHours, timeUntilFirstPara.Minutes);

                                Status = $"До начала {Calls[0].NumberPara} пары осталось: {formattedTime}.";
                                foundPara = true;
                            }
                        }
                    }

                    await Task.Delay(5000);
                }
            }); 
        }

        async void OpenPageAddItem(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Понедельник":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleMonday)));
                    break;
                case "Вторник":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleTuesday)));
                    break;
                case "Среда":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleWednesday)));
                    break;
                case "Четверг":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleThursday)));
                    break;
                case "Пятница":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleFriday)));
                    break;
                case "Суббота":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleSaturday)));
                    break;
                case "Воскресенье":
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AddItemPage
                        (new AddItemViewModel(sheduleRepository, dayOfWeek, SheduleSunday)));
                    break;
            }
        }
        async void DeleteItem(ItemVM ItemVM)
        {
            if (ItemVM == null)
            {
                Application.Current.MainPage.DisplayAlert("", "Пара не выбрана для удаления!", "Ok");
                return;
            }

            await sheduleRepository.Delete<Item>(ItemVM.Id);
            switch (ItemVM.DayOfWeek)
            {
                case "Понедельник":
                    SheduleMonday.Remove(ItemVM);
                    break;
                case "Вторник":
                    SheduleTuesday.Remove(ItemVM);
                    break;
                case "Среда":
                    SheduleWednesday.Remove(ItemVM);
                    break;
                case "Четверг":
                    SheduleThursday.Remove(ItemVM);
                    break;
                case "Пятница":
                    SheduleFriday.Remove(ItemVM);
                    break;
                case "Суббота":
                    SheduleSaturday.Remove(ItemVM);
                    break;
                case "Воскресенье":
                    SheduleSunday.Remove(ItemVM);
                    break;
            }
        }

        void GetCalls()
        {
            foreach(var call in sheduleRepository.GetAllItems<Call>())
            {
                var callVM = new CallVM
                {
                    Id = call.Id,
                    NumberPara = call.NumberPara,
                    StartTime = call.StartTime.ToTimeSpan(),
                    EndTime = call.EndTime.ToTimeSpan()
                };
                Calls.Add(callVM);
            }
        }
        void GetDataShedule()
        {
            foreach(var item in sheduleRepository.GetAllItems<Item>())
            {
                ItemVM itemVM = new ItemVM
                {
                    Id = item.Id,
                    Subject = item.Subject,
                    Teacher = item.Teacher,
                    Where = item.Where,
                    DayOfWeek = item.DayOfWeek
                };

                int numberPara;
                if (char.IsDigit(itemVM.Subject[0]) && int.TryParse(itemVM.Subject[0].ToString(), out numberPara))
                {
                    itemVM.StartTimeLesson = TimeOnly.FromTimeSpan(Calls
                        .Where(p => p.NumberPara == numberPara)
                        .Select(p => p.StartTime)
                        .FirstOrDefault());

                    itemVM.EndTimeLesson = TimeOnly.FromTimeSpan(Calls
                        .Where(p => p.NumberPara == numberPara)
                        .Select(p => p.EndTime)
                        .FirstOrDefault());
                }
                switch (item.DayOfWeek)
                {
                    case "Понедельник":
                        SheduleMonday.Add(itemVM);
                        break;
                    case "Вторник":
                        SheduleTuesday.Add(itemVM);
                        break;
                    case "Среда":
                        SheduleWednesday.Add(itemVM);
                        break;
                    case "Четверг":
                        SheduleThursday.Add(itemVM);
                        break;
                    case "Пятница":
                        SheduleFriday.Add(itemVM);
                        break;
                    case "Суббота":
                        SheduleSaturday.Add(itemVM);
                        break;
                    case "Воскресенье":
                        SheduleSunday.Add(itemVM);
                        break;
                }
            }
        }
        void ShowShedule()
        {
            switch(CurrentDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    Visual.VisibilityMonday = true;
                    break;
                case DayOfWeek.Tuesday:
                    Visual.VisibilityTuesday = true;
                    break;
                case DayOfWeek.Wednesday:
                    Visual.VisibilityWednesday = true;
                    break;
                case DayOfWeek.Thursday:
                    Visual.VisibilityThursday = true;
                    break;
                case DayOfWeek.Friday:
                    Visual.VisibilityFriday = true;
                    break;
                case DayOfWeek.Saturday:
                    Visual.VisibilitySaturday = true;
                    break;
                case DayOfWeek.Sunday:
                    Visual.VisibilitySunday = true;
                    break;
            }
            
        }
    }
}