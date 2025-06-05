using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel; // MVVM Toolkit
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace KURSOVOIproject.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;
        private readonly ISpecializationService _specService;

        public RegistrationViewModel(
            IStudentService studentService,
            ISpecializationService specService)
        {
            _studentService = studentService;
            _specService = specService;
            Specializations = new ObservableCollection<Specialization>();

            LoadSpecializationsCommand = new AsyncRelayCommand(LoadSpecializationsAsync);
            RegisterCommand = new AsyncRelayCommand(RegisterAsync);
        }

        // ===========================
        // ===== ПОЛЯ И СВОЙСТВА ====
        // ===========================

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string telNumber;

        [ObservableProperty]
        private int course;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private Specialization selectedSpecialization;

        public ObservableCollection<Specialization> Specializations { get; }

        [ObservableProperty]
        private bool isBusy;

        public bool IsNotBusy => !IsBusy;

        // ===========================
        // ======= КОМАНДЫ ==========
        // ===========================

        public IAsyncRelayCommand LoadSpecializationsCommand { get; }
        public IAsyncRelayCommand RegisterCommand { get; }

        // ===========================
        // ======= МЕТОДЫ ===========
        // ===========================

        private async Task LoadSpecializationsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Specializations.Clear();
                var list = await _specService.GetAllAsync();
                foreach (var sp in list)
                    Specializations.Add(sp);
            }
            catch
            {
                // Игнорируем ошибку или сообщаем пользователю
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RegisterAsync()
        {
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Name)
                || string.IsNullOrWhiteSpace(TelNumber)
                || SelectedSpecialization == null
                || string.IsNullOrWhiteSpace(Password)
                || Course <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ошибка", "Заполните все поля корректно", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var student = new Student
                {
                    Name = Name,
                    TelNumber = TelNumber,
                    Course = Course,
                    IdSpecialization = SelectedSpecialization.Id,
                    Password = Password
                };

                await _studentService.AddAsync(student);

                // Сохраняем в Preferences
                Preferences.Default.Set("CurrentStudentId", student.Id);
                Preferences.Default.Set("IsStudentLoggedIn", true);

                // После успешной регистрации — переходим на Search
                await Shell.Current.GoToAsync("//Search");

                await Application.Current.MainPage.DisplayAlert(
                    "Успех", "Регистрация прошла успешно!", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ошибка", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
    }
}
