<<<<<<< HEAD
using System;
=======
>>>>>>> 359d440 (InternshipADDING!)
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
<<<<<<< HEAD
    public partial class AdminPage : ContentPage
=======
    // Имя класса и базовый тип должны совпадать с XAML (TabbedPage)
    public partial class AdminPage : TabbedPage
>>>>>>> 359d440 (InternshipADDING!)
    {
        private readonly AdminPageViewModel _viewModel;

        public AdminPage(AdminPageViewModel viewModel)
        {
            InitializeComponent();
<<<<<<< HEAD
            BindingContext = _viewModel = viewModel;
        }

        // TODO: обработчики нажатий на пункты меню – «Статистика», «Пользователи», «Стажировки»
=======

            // Связываем ViewModel
            BindingContext = _viewModel = viewModel;
        }

        // TODO: обработчики выбора вкладок «Пользователи», «Стажировки», «Статистика»
>>>>>>> 359d440 (InternshipADDING!)
    }
}
