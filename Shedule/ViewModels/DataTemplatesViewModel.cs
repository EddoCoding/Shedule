using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shedule.Common.Repositories;
using Shedule.Models;
using Shedule.ViewModels.Templates;
using Shedule.Views;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Shedule.ViewModels
{
    public class DataTemplatesViewModel : ReactiveObject
    {
        ISheduleRepository sheduleRepository = new SheduleRepository();

        public ObservableCollection<SubjectVM> SubjectTemplates { get; set; } = new();
        public ObservableCollection<TeacherVM> TeacherTemplates { get; set; } = new();
        public ObservableCollection<PlaceVM> PlaceTemplates { get; set; } = new();
        public ObservableCollection<CallVM> CallTemplates { get; set; } = new();

        [Reactive] public SubjectVM SelectedSubject { get; set; }
        [Reactive] public TeacherVM SelectedTeacher { get; set; }
        [Reactive] public PlaceVM SelectedPlace { get; set; }
        [Reactive] public CallVM SelectedCall { get; set; }

        //КОМАНДЫ ПРЕДМЕТА
        public ReactiveCommand<Unit, Unit> OpenDisplayPromptAddSubjectCommand { get; set; }
        public ReactiveCommand<SubjectVM, Unit> DeleteSubjectCommand { get; set; }
        //КОМАНДЫ ПРЕПОДАВАТЕЛЯ
        public ReactiveCommand<Unit, Unit> OpenDisplayPromptAddTeacherCommand { get; set; }
        public ReactiveCommand<TeacherVM, Unit> DeleteTeacherCommand { get; set; }
        //КОМАНДЫ МЕСТА ПРОВЕДЕНИЯ
        public ReactiveCommand<Unit, Unit> OpenDisplayPromptAddPlaceCommand { get; set; }
        public ReactiveCommand<PlaceVM, Unit> DeletePlaceCommand { get; set; }
        //КОМАНДЫ ЗВОНКОВ
        public ReactiveCommand<Unit,Unit> OpenPageAddCallCommand { get; set; }
        public ReactiveCommand<CallVM, Unit> DeleteCallCommand { get; set; }

        public DataTemplatesViewModel()
        {
            GetSubjects();
            GetTeachers();
            GetPlaces();
            GetCalls();

            OpenDisplayPromptAddSubjectCommand = ReactiveCommand.Create(OpenDisplayPromptAddSubject);
            DeleteSubjectCommand = ReactiveCommand.Create<SubjectVM>(DeleteSubject);

            OpenDisplayPromptAddTeacherCommand = ReactiveCommand.Create(OpenDisplayPromptAddTeacher);
            DeleteTeacherCommand = ReactiveCommand.Create<TeacherVM>(DeleteTeacher);

            OpenDisplayPromptAddPlaceCommand = ReactiveCommand.Create(OpenDisplayPromptAddPlace);
            DeletePlaceCommand = ReactiveCommand.Create<PlaceVM>(DeletePlace);

            OpenPageAddCallCommand = ReactiveCommand.Create(OpenPageAddCall);
            DeleteCallCommand = ReactiveCommand.Create<CallVM>(DeleteCall);
        }

        void GetSubjects()
        {
            foreach (var subject in sheduleRepository.GetAllItems<Subject>())
            {
                var subjectVM = new SubjectVM
                {
                    Id = subject.Id,
                    SubjectName = subject.SubjectName
                };
                SubjectTemplates.Add(subjectVM);
            }
        }
        void GetTeachers()
        {
            foreach (var teacher in sheduleRepository.GetAllItems<Teacher>())
            {
                var teacherVM = new TeacherVM
                {
                    Id = teacher.Id,
                    TeacherName = teacher.TeacherName
                };
                TeacherTemplates.Add(teacherVM);
            }
        }
        void GetPlaces()
        {
            foreach (var place in sheduleRepository.GetAllItems<Place>())
            {
                var placeVM = new PlaceVM
                {
                    Id = place.Id,
                    PlaceName = place.PlaceName
                };
                PlaceTemplates.Add(placeVM);
            }
        }
        void GetCalls()
        {
            foreach (var call in sheduleRepository.GetAllItems<Call>())
            {
                var callVM = new CallVM
                {
                    Id = call.Id,
                    NumberPara = call.NumberPara,
                    StartTime = call.StartTime.ToTimeSpan(),
                    EndTime = call.EndTime.ToTimeSpan()
                };
                CallTemplates.Add(callVM);
            }
        }


        //МЕТОДЫ ПРЕДМЕТА
        async void OpenDisplayPromptAddSubject()
        {
            var subjectName = await Application.Current.MainPage.DisplayPromptAsync("", "Введите название предмета!", "Ввод", "Отмена");
            if (String.IsNullOrWhiteSpace(subjectName)) return;

            if (await sheduleRepository.CheckSubjectName(subjectName))
            {
                await Application.Current.MainPage.DisplayAlert("Валидация", "Такой предмет уже есть!", "Ok");
                return;
            }

            var subjectVM = new SubjectVM
            {
                SubjectName = subjectName
            };

            await sheduleRepository.Create<Subject>(new Subject
            {
                Id = subjectVM.Id,
                SubjectName = subjectVM.SubjectName
            });

            SubjectTemplates.Add(subjectVM);
        }
        async void DeleteSubject(SubjectVM subjectVM)
        {
            if(subjectVM == null)
            {
                await Application.Current.MainPage.DisplayAlert("", "Предмет не выбран!", "Ok");
                return;
            }
            await sheduleRepository.Delete<Subject>(subjectVM.Id);
            SubjectTemplates.Remove(subjectVM);
        }
        //МЕТОДЫ ПРЕПОДАВАТЕЛЯ
        async void OpenDisplayPromptAddTeacher()
        {
            var teacherName = await Application.Current.MainPage.DisplayPromptAsync("", "Введите ФИО преподавателя!", "Ввод", "Отмена");
            if (String.IsNullOrWhiteSpace(teacherName)) return;

            if (await sheduleRepository.CheckTeacherName(teacherName))
            {
                await Application.Current.MainPage.DisplayAlert("Валидация", "Такой преподаватель уже есть!", "Ok");
                return;
            }

            var teacherVM = new TeacherVM
            {
                TeacherName = teacherName
            };

            await sheduleRepository.Create<Teacher>(new Teacher
            {
                Id = teacherVM.Id,
                TeacherName = teacherVM.TeacherName
            });
            
            TeacherTemplates.Add(teacherVM);
        }
        async void DeleteTeacher(TeacherVM teacherVM)
        {
            if (teacherVM == null)
            {
                await Application.Current.MainPage.DisplayAlert("", "Преподаватель не выбран!", "Ok");
                return;
            }
            await sheduleRepository.Delete<Teacher>(teacherVM.Id);
            TeacherTemplates.Remove(teacherVM);
        }
        //МЕТОДЫ МЕСТА ПРОВЕДЕНИЯ
        async void OpenDisplayPromptAddPlace()
        {
            var placeName = await Application.Current.MainPage.DisplayPromptAsync("", "Введите место проведения!", "Ввод", "Отмена");
            if (String.IsNullOrWhiteSpace(placeName)) return;

            if (await sheduleRepository.CheckPlaceName(placeName))
            {
                await Application.Current.MainPage.DisplayAlert("Валидация", "Такое место проведения уже есть!", "Ok");
                return;
            }

            var placeVM = new PlaceVM
            {
                PlaceName = placeName
            };

            await sheduleRepository.Create<Place>(new Place
            {
                Id = placeVM.Id,
                PlaceName = placeVM.PlaceName
            });

            PlaceTemplates.Add(placeVM);
        }
        async void DeletePlace(PlaceVM placeVM)
        {
            if (placeVM == null)
            {
                await Application.Current.MainPage.DisplayAlert("", "Место проведения не выбрано!", "Ok");
                return;
            }
            await sheduleRepository.Delete<Place>(placeVM.Id);
            PlaceTemplates.Remove(placeVM);
        }
        //МЕТОДЫ ЗВОНКОВ
        async void OpenPageAddCall()
        {
            var vm = new AddCallViewModel(sheduleRepository, CallTemplates);
            var view = new AddCallPage(vm);

            await Application.Current.MainPage.Navigation.PushModalAsync(view);
        }
        async void DeleteCall(CallVM callVM)
        {
            if (callVM == null)
            {
                await Application.Current.MainPage.DisplayAlert("", "Пара не выбрана!", "Ok");
                return;
            }

            await sheduleRepository.Delete<Call>(callVM.Id);
            CallTemplates.Remove(callVM);
        }
    }
}