using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public partial class ResponsesPageViewModel : ObservableObject
    {
        private readonly IApplicationService _applicationService;

        public ObservableCollection<ApplicationEntity> Responses { get; }
            = new ObservableCollection<ApplicationEntity>();

        [ObservableProperty]
        private bool isBusy;

        public bool IsNotBusy => !IsBusy;

        public IAsyncRelayCommand LoadResponsesCommand { get; }

        public ResponsesPageViewModel(IApplicationService applicationService)
        {
            _applicationService = applicationService;
            LoadResponsesCommand = new AsyncRelayCommand(LoadAsync);
        }

        private async Task LoadAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Responses.Clear();

                // TODO completed: Получаем id текущего студента из Preferences
                int studentId = Preferences.Default.Get("CurrentStudentId", 0);
                if (studentId == 0)
                    return;

                // TODO completed: получаем отклики студента через сервис
                var list = await _applicationService.GetByStudentAsync(studentId);

                foreach (var resp in list)
                    Responses.Add(resp);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
    }
}
