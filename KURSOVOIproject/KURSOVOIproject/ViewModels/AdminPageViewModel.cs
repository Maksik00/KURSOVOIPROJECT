using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Services;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject.ViewModels
{
    public partial class AdminPageViewModel : ObservableObject
    {
        private readonly IAdminService _adminService;

        [ObservableProperty]
        private bool isBusy;

        public IAsyncRelayCommand ShowStatisticsCommand { get; }
        public IAsyncRelayCommand ShowUsersCommand { get; }
        public IAsyncRelayCommand ShowInternshipsCommand { get; }

        public AdminPageViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            ShowStatisticsCommand = new AsyncRelayCommand(NavigateToStatisticsAsync);
            ShowUsersCommand = new AsyncRelayCommand(NavigateToUsersAsync);
            ShowInternshipsCommand = new AsyncRelayCommand(NavigateToInternshipsAsync);
        }

        private async Task NavigateToStatisticsAsync()
        {
            // TODO completed: переход на страницу StatisticsPage
            await Shell.Current.GoToAsync("//Statistics");
        }

        private async Task NavigateToUsersAsync()
        {
            // TODO completed: переход на страницу просмотра всех пользователей
            await Shell.Current.GoToAsync("//UsersList");
        }

        private async Task NavigateToInternshipsAsync()
        {
            // TODO completed: переход на страницу просмотра/редактирования всех стажировок
            await Shell.Current.GoToAsync("//AllInternships");
        }
    }
}
