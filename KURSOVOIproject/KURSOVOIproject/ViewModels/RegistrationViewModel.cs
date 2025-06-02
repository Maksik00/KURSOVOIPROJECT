using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;
using Microsoft.Maui.Controls;

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

        // Поля формы
        [ObservableProperty] string name;
        [ObservableProperty] string telNumber;
        [ObservableProperty] int course;
        [ObservableProperty] string password;
        [ObservableProperty] Specialization selectedSpecialization;

        // Сборник специализаций
        public ObservableCollection<Specialization> Specializations { get; }

        // Флаги
        [ObservableProperty] bool isBusy;
        public bool IsNotBusy => !IsBusy;

        // Команды
        public IAsyncRelayCommand LoadSpecializationsCommand { get; }
        public IAsyncRelayCommand RegisterCommand { get; }

        // Загрузка списка специализаций
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
            finally
            {
                IsBusy = false;
            }
        }

        // Обработка регистрации
        private async Task RegisterAsync()
        {
            if (IsBusy) return;
            if (string.IsNullOrWhiteSpace(Name)
             || string.IsNullOrWhiteSpace(TelNumber)
             || SelectedSpecialization == null
             || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ошибка", "Заполните все поля", "OK");
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

                // После успешной регистрации — пока оставим здесь просто сообщение.
                await Application.Current.MainPage.DisplayAlert(
                    "Успех", "Регистрация прошла успешно!", "OK");

                // Позже, когда появятся другие страницы, можно будет перейти:
                // await Shell.Current.GoToAsync("//SearchPage");
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
