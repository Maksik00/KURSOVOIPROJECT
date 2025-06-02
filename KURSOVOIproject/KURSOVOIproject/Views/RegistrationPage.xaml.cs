using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.Models;
using System.Collections.Generic;

namespace KURSOVOIproject.Views
{
    public partial class RegistrationPage : ContentPage
    {
        private readonly IStudentService _studentService;
        private readonly ISpecializationService _specService;
        private List<Specialization> _allSpecs;

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

            // Загружаем список специализаций
            _allSpecs = await _specService.GetAllAsync();
            SpecPicker.ItemsSource = _allSpecs;
            SpecPicker.ItemDisplayBinding = new Binding("Name");
        }

        private async void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {
            string name = NameEntry.Text?.Trim();
            string phone = PhoneEntry.Text?.Trim();
            string courseText = CourseEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();
            var selectedSpec = SpecPicker.SelectedItem as Specialization;

            if (string.IsNullOrWhiteSpace(name)
             || string.IsNullOrWhiteSpace(phone)
             || string.IsNullOrWhiteSpace(courseText)
             || selectedSpec == null
             || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            if (!int.TryParse(courseText, out int course))
            {
                await DisplayAlert("Ошибка", "Курс должен быть числом", "OK");
                return;
            }

            var student = new Student
            {
                Name = name,
                TelNumber = phone,
                Course = course,
                IdSpecialization = selectedSpec.Id,
                Password = password
            };

            try
            {
                await _studentService.AddAsync(student);
                await DisplayAlert("Успех", "Регистрация прошла успешно", "OK");

                // После регистрации переходим на LoginPage
                await Shell.Current.GoToAsync("LoginPage");
            }
            catch (System.Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}
