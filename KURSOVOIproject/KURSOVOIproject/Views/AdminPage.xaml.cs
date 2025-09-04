using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class AdminPage : ContentPage
    {
        private readonly AdminPageViewModel _viewModel;

        public AdminPage(AdminPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        // TODO: обработчики нажатий на пункты меню – «Статистика», «Пользователи», «Стажировки»
    }
}
