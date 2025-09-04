using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;
using Microsoft.Maui.Storage;

namespace KURSOVOIproject.Views
{
    public partial class StudentProfilePage : ContentPage
    {
        public StudentProfilePage(StudentProfileViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnEditProfileClicked(object sender, System.EventArgs e)
        {
            // TODO completed: пока выводим заглушку
            await DisplayAlert("Информация", "Редактирование профиля в разработке", "OK");
        }

        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            Preferences.Default.Remove("CurrentStudentId");
            Preferences.Default.Set("IsStudentLoggedIn", false);

            await Shell.Current.GoToAsync("//Landing");
        }
    }
}
