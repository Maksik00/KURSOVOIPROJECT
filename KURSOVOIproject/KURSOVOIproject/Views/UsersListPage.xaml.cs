using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class UsersListPage : ContentPage
    {
        private readonly UsersListPageViewModel _viewModel;
        public UsersListPage(UsersListPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
<<<<<<< HEAD
            // TODO: _viewModel.LoadUsersCommand.Execute(null);
=======
            // TODO completed: загружаем список пользователей
            await _viewModel.LoadUsersCommand.ExecuteAsync(null);
>>>>>>> 359d440 (InternshipADDING!)
        }
    }
}
