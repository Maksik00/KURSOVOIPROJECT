// File: CompanyProfileViewModel.cs
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
    public class CompanyProfileViewModel : BaseViewModel
    {
        private readonly ICompanyService _companyService;

        public CompanyProfileViewModel(ICompanyService companyService)
        {
            _companyService = companyService;

            PickAvatarCommand = new Command(async () => await OnPickAvatar());
            SaveCommand = new Command(async () => await OnSave());

            LoadCompanyData();
        }

        // Поля для привязки
        private string _name = "Компания";
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        private string _city = "";
        public string City
        {
            get => _city;
            set { if (_city != value) { _city = value; OnPropertyChanged(); OnPropertyChanged(nameof(Address)); } }
        }

        private string _street = "";
        public string Street
        {
            get => _street;
            set { if (_street != value) { _street = value; OnPropertyChanged(); OnPropertyChanged(nameof(Address)); } }
        }

        private string _building = "";
        public string Building
        {
            get => _building;
            set { if (_building != value) { _building = value; OnPropertyChanged(); OnPropertyChanged(nameof(Address)); } }
        }

        public string Address => $"{City}, {Street}, д. {Building}";

        private string _contactInfo = "";
        public string ContactInfo
        {
            get => _contactInfo;
            set { if (_contactInfo != value) { _contactInfo = value; OnPropertyChanged(); } }
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
        private async void LoadCompanyData()
        {
            int compId = Preferences.Default.Get("CurrentCompanyId", 0);
            if (compId == 0) return;

            var company = await _companyService.GetByIdAsync(compId);
            if (company == null) return;

            Name = company.Name;
            City = company.City;
            Street = company.Street;
            Building = company.Building;
            ContactInfo = company.Email;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Street));
            OnPropertyChanged(nameof(Building));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(ContactInfo));

            _avatarPath = company.AvatarPath;
            Avatar = string.IsNullOrEmpty(_avatarPath)
                ? ImageSource.FromFile("default_company.png")
                : ImageSource.FromFile(_avatarPath);
        }

        // Выбор логотипа/аватарки компании
        private async Task OnPickAvatar()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Выберите логотип компании" });
            if (result == null) return;

            string dir = Path.Combine(FileSystem.AppDataDirectory, "company_avatars");
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
            int compId = Preferences.Default.Get("CurrentCompanyId", 0);
            if (compId == 0) return;

            var company = await _companyService.GetByIdAsync(compId);
            if (company == null) return;

            company.Name = Name;
            company.City = City;
            company.Street = Street;
            company.Building = Building;
            company.Email = ContactInfo;
            company.AvatarPath = _avatarPath;

            await _companyService.UpdateAsync(company);
            await Shell.Current.DisplayAlert("Успех", "Данные компании сохранены", "OK");
        }
    }
}
