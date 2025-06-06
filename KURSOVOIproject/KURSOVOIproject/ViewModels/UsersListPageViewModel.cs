using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;  
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public partial class UsersListPageViewModel : ObservableObject
    {
        private readonly IAdminService _adminService;
        // TODO: возможно, понадобится также IStudentService и ICompanyService

        [ObservableProperty]
        private bool isBusy;

        public ObservableCollection<string> Users { get; }
            = new ObservableCollection<string>();

        public IAsyncRelayCommand LoadUsersCommand { get; }

        public UsersListPageViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            LoadUsersCommand = new AsyncRelayCommand(LoadUsersAsync);
        }

        private async Task LoadUsersAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Users.Clear();
                // TODO: получить из _adminService список студентов/компаний, 
                // например new List<string> { "Student1", "Company1" } и 
                // добавить в Users
                Users.Add("Student1");
                Users.Add("CompanyA");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
