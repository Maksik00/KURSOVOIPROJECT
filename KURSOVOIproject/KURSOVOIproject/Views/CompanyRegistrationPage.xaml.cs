using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.Views
{
    public partial class CompanyRegistrationPage : ContentPage
    {
        private readonly ICompanyService _companyService;

        public CompanyRegistrationPage(ICompanyService companyService)
        {
            InitializeComponent();
            _companyService = companyService;
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Считываем данные из полей
            string name = NameEntry.Text?.Trim() ?? "";
            string city = CityEntry.Text?.Trim() ?? "";
            string street = StreetEntry.Text?.Trim() ?? "";
            string building = BuildingEntry.Text?.Trim() ?? "";
            string password = PasswordEntry.Text?.Trim() ?? "";

            // Проверяем, что всё заполнено
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(street) ||
                string.IsNullOrWhiteSpace(building) ||
                string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }

            try
            {
                // Создаём новый объект Company
                var newCompany = new Company
                {
                    Name = name,
                    City = city,
                    Street = street,
                    Building = building,
                    Password = password
                };

                // Добавляем в базу через сервис
                await _companyService.AddAsync(newCompany);

                // Уведомляем пользователя и сразу предлагаем авторизоваться
                await DisplayAlert("Успех", "Компания успешно зарегистрирована!", "OK");

                // После регистрации — переходим на страницу логина для компаний
                await Shell.Current.GoToAsync("//CompanyLoginPage");
            }
            catch (Exception ex)
            {
                // Если компания с таким именем уже есть или другая ошибка
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void OnAlreadyHaveAccountClicked(object sender, EventArgs e)
        {
            // Просто навигируем на страницу логина для компаний
            await Shell.Current.GoToAsync("//CompanyLoginPage");
        }
    }
}
