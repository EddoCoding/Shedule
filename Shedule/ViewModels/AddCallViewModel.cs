using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shedule.Common.Repositories;
using Shedule.Models;
using Shedule.ViewModels.Templates;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Shedule.ViewModels
{
    public class AddCallViewModel : ReactiveObject
    {
       [Reactive] public CallVM CallVM { get; set; } = new();

       public ReactiveCommand<CallVM, Unit> AddCallCommand { get; set; }

        ISheduleRepository _sheduleRepository;
        ObservableCollection<CallVM> _callTemplates;
        public AddCallViewModel(ISheduleRepository sheduleRepository, ObservableCollection<CallVM> callTemplates)
        {
            _sheduleRepository = sheduleRepository;
            _callTemplates = callTemplates;

            AddCallCommand = ReactiveCommand.Create<CallVM>(AddCall);
        }

        async void AddCall(CallVM callVM)
        {
            if (!callVM.Validation()) return;
        
            if(await _sheduleRepository.CheckNumberPara(callVM.NumberPara))
            {
                await Application.Current.MainPage.DisplayAlert("Валидация", $"{callVM.NumberPara} пара уже есть!", "Ok");
                return;
            }

            var call = new Call
            {
                Id = callVM.Id,
                NumberPara = callVM.NumberPara,
                
                StartTime = TimeOnly.FromTimeSpan(callVM.StartTime),
                EndTime = TimeOnly.FromTimeSpan(callVM.EndTime)
            };

            await _sheduleRepository.Create<Call>(call); 

            _callTemplates.Add(callVM);
        
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}