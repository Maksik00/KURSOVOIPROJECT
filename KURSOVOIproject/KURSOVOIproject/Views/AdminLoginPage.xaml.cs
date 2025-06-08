<<<<<<< HEAD
﻿using System;
using Microsoft.Maui.Controls;
=======
﻿using Microsoft.Maui.Controls;
>>>>>>> 359d440 (InternshipADDING!)
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
<<<<<<< HEAD
=======
    // Имя класса и базовый тип должны совпадать с XAML (ContentPage)
>>>>>>> 359d440 (InternshipADDING!)
    public partial class AdminLoginPage : ContentPage
    {
        private readonly AdminLoginViewModel _viewModel;

        public AdminLoginPage(AdminLoginViewModel viewModel)
        {
            InitializeComponent();
<<<<<<< HEAD
            BindingContext = _viewModel = viewModel;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // TODO: вызвать метод _viewModel.LoginAsync() и по результату навигация
            await _viewModel.LoginAsync();
        }

        private async void OnRegisterAdminClicked(object sender, EventArgs e)
        {
            // TODO: в будущем можно добавить регистрацию админа
            await DisplayAlert("Info", "Регистрация администратора пока недоступна", "OK");
        }
=======

            // Связываем ViewModel
            BindingContext = _viewModel = viewModel;
        }

        // TODO: сюда можно добавить команды/обработчики входа администратора
>>>>>>> 359d440 (InternshipADDING!)
    }
}
