// File: StudentProfileViewModel.cs
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public class StudentProfileViewModel : BaseViewModel
    {
        private readonly IStudentService _studentService;
        private readonly ISpecializationService _specService;

        public StudentProfileViewModel(IStudentService studentService, ISpecializationService specService)
        {
            _studentService = studentService;
            _specService = specService;

            PickAvatarCommand = new Command(async () => await OnPickAvatar());
            SaveCommand = new Command(async () => await OnSave());

            LoadStudentData();
        }

        // Поля для привязки
        private string _name = "Студент";
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        private string _telNumber = "";
        public string TelNumber
        {
            get => _telNumber;
            set { if (_telNumber != value) { _telNumber = value; OnPropertyChanged(); } }
        }

        private int _course;
        public int Course
        {
            get => _course;
            set { if (_course != value) { _course = value; OnPropertyChanged(); } }
        }

        private string _specializationName = "Не задана";
        public string SpecializationName
        {
            get => _specializationName;
            set { if (_specializationName != value) { _specializationName = value; OnPropertyChanged(); } }
        }

        private string _skillsDisplay = "";
        public string SkillsDisplay
        {
            get => _skillsDisplay;
            set { if (_skillsDisplay != value) { _skillsDisplay = value; OnPropertyChanged(); } }
        }

        private ImageSource _avatar;
        public ImageSource Avatar
        {
            get => _avatar;
            set => SetProperty(ref _avatar, value);
        }

        private string _avatarPath = string.Empty;

        // Команды
        public ICommand PickAvatarCommand { get; }
        public ICommand SaveCommand { get; }

        // Загрузка данных из БД
        private async void LoadStudentData()
        {
            int studentId = Preferences.Default.Get("CurrentStudentId", 0);
            if (studentId == 0) return;

            var student = await _studentService.GetByIdAsync(studentId);
            if (student == null) return;

            Name = student.Name;
            TelNumber = student.TelNumber;
            Course = student.Course;
            SpecializationName = (await _specService.GetByIdAsync(student.IdSpecialization))?.Name ?? "Не задана";
            SkillsDisplay = student.SkillsString ?? "";

            _avatarPath = student.AvatarPath;
            Avatar = string.IsNullOrEmpty(_avatarPath)
                ? ImageSource.FromFile("default_avatar.png")
                : ImageSource.FromFile(_avatarPath);
        }

        // Выбор файла аватарки
        private async Task OnPickAvatar()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Выберите фото профиля" });
            if (result == null) return;

            string dir = Path.Combine(FileSystem.AppDataDirectory, "avatars");
            Directory.CreateDirectory(dir);

            string file = Path.Combine(dir, $"{Guid.NewGuid()}{Path.GetExtension(result.FileName)}");
            using var src = await result.OpenReadAsync();
            using var dest = File.OpenWrite(file);
            await src.CopyToAsync(dest);

            _avatarPath = file;
            Avatar = ImageSource.FromFile(file);
        }

        // Сохранение изменений
        private async Task OnSave()
        {
            int studentId = Preferences.Default.Get("CurrentStudentId", 0);
            if (studentId == 0) return;

            var student = await _studentService.GetByIdAsync(studentId);
            if (student == null) return;

            student.Name = Name;
            student.TelNumber = TelNumber;
            student.Course = Course;
            student.SkillsString = SkillsDisplay;
            student.AvatarPath = _avatarPath;

            await _studentService.UpdateAsync(student);
            await Shell.Current.DisplayAlert("Успех", "Профиль сохранён", "OK");
        }
    }
}
