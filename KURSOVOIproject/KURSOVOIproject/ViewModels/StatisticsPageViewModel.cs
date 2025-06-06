using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public partial class StatisticsPageViewModel : ObservableObject
    {
        private readonly IAdminService _adminService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private Dictionary<string, int> internshipsBySpecialization;

        public IAsyncRelayCommand LoadStatisticsCommand { get; }

        public StatisticsPageViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            LoadStatisticsCommand = new AsyncRelayCommand(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                InternshipsBySpecialization = await _adminService.GetInternshipsCountBySpecializationAsync();
                // TODO: при желании преобразовать InternshipsBySpecialization в ObservableCollection
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
