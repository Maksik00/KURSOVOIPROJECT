using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.Models;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models; // Для Specialization
using System.Collections.Generic;

namespace KURSOVOIproject.Views
{
    public partial class RegistrationPage : ContentPage
    {
        private readonly IStudentService _studentService;
        private readonly ISpecializationService _specService;
        private List<Specialization> _allSpecs = new();

        public RegistrationPage(
            IStudentService studentService,
            ISpecializationService specService)
        {
            InitializeComponent();
            _studentService = studentService;
            _specService = specService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Загрузить список специальностей
            _allSpecs = await _specService.GetAllAsync();
            SpecPicker.ItemsSource = _allSpecs;
            SpecPicker.ItemDisplayBinding = new Binding("Name");
        }

        private async void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {
            string name = NameEntry.Text?.Trim() ?? "";
            string phone = PhoneEntry.Text?.Trim() ?? "";
            string courseText = CourseEntry.Text?.Trim() ?? "";
            string password = PasswordEntry.Text?.Trim() ?? "";
            var selectedSpec = SpecPicker.SelectedItem as Specialization;

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(courseText) ||
                string.IsNullOrEmpty(password) ||
                selectedSpec == null)
            {
                await DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            if (!int.TryParse(courseText, out int course) || course <= 0)
            {
                await DisplayAlert("Ошибка", "Курс должен быть положительным числом", "OK");
                return;
            }

            var newStudent = new Student
            {
                Name = name,
                TelNumber = phone,
                Course = course,
                IdSpecialization = selectedSpec.Id,
                Password = password
            };

            await _studentService.CreateAsync(newStudent);

            Preferences.Default.Set("CurrentStudentId", newStudent.Id);
            Preferences.Default.Set("IsStudentLoggedIn", true);

            await Shell.Current.GoToAsync("//Search");
        }

        private async void OnLoginTapped(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//Login");
        }
    }
}
