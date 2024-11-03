using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shedule.Common.Repositories;
using Shedule.Models;
using Shedule.ViewModels.Templates;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Shedule.ViewModels
{
    public class AddItemViewModel
    {
        [Reactive] public ItemVM ItemVM { get; set; } = new();

        public List<string> Subjects { get; set; } = new();
        public List<string> Teachers { get; set; } = new();
        public List<string> Places { get; set; } = new();
        public List<CallVM> Calls { get; set; } = new();

        public ReactiveCommand<ItemVM, Unit> AddItemCommand { get; set; }

        ISheduleRepository _sheduleRepository;
        string _dayOfWeek;
        ObservableCollection<ItemVM> _items;
        public AddItemViewModel(ISheduleRepository sheduleRepository, string dayOfWeek, ObservableCollection<ItemVM> items)
        {
            _sheduleRepository = sheduleRepository;
            _dayOfWeek = dayOfWeek;
            _items = items;

            GetSubjects();
            GetTeachers();
            GetPlaces();
            GetCalls();

            AddItemCommand = ReactiveCommand.Create<ItemVM>(AddItem);
        }

        async void AddItem(ItemVM itemVM)
        {
            if (!itemVM.Validation()) return;
            itemVM.DayOfWeek = _dayOfWeek;

            var item = new Item
            {
                Id = itemVM.Id,
                Subject = itemVM.Subject,
                Teacher = itemVM.Teacher,
                Where = itemVM.Where,
                DayOfWeek = itemVM.DayOfWeek
            };

            await _sheduleRepository.Create<Item>(item);

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

            _items.Add(itemVM);

            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        void GetSubjects()
        {
            foreach(var subject in _sheduleRepository.GetAllItems<Subject>())
                Subjects.Add(subject.SubjectName);
        }
        void GetTeachers()
        {
            foreach (var teacher in _sheduleRepository.GetAllItems<Teacher>())
                Teachers.Add(teacher.TeacherName);
        }
        void GetPlaces()
        {
            foreach (var place in _sheduleRepository.GetAllItems<Place>())
                Places.Add(place.PlaceName);
        }
        void GetCalls()
        {
            foreach(var call in _sheduleRepository.GetAllItems<Call>())
            {
                var callVM = new CallVM
                {
                    Id= call.Id,
                    NumberPara = call.NumberPara,
                    StartTime = call.StartTime.ToTimeSpan(),
                    EndTime = call.EndTime.ToTimeSpan(),
                };
                Calls.Add(callVM);
            }
        }
    }
}