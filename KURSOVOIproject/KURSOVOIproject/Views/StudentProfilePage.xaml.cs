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
            // TODO: реализовать логику редактирования (можно открыть новую страницу или диалог)
            await DisplayAlert("Редактировать", "Здесь можно редактировать профиль студента.", "OK");
        }

        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            Preferences.Default.Remove("CurrentStudentId");
            Preferences.Default.Set("IsStudentLoggedIn", false);

            await Shell.Current.GoToAsync("//Landing");
        }
    }
}
