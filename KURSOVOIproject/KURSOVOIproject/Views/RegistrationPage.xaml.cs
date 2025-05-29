using KURSOVOIproject.ViewModels;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject.Views
{
    public partial class RegistrationPage : ContentPage
    {
        // ViewModel придёт из DI (в MauiProgram мы его зарегистрировали)
        public RegistrationPage(RegistrationViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Загружаем справочник специализаций
            if (BindingContext is RegistrationViewModel vm
                && vm.LoadSpecializationsCommand.CanExecute(null))
            {
                vm.LoadSpecializationsCommand.Execute(null);
            }
        }
    }
}
