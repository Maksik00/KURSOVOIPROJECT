using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public class CreateInternshipViewModel : BaseViewModel
    {
        private readonly IInternshipService _internshipService;
        private readonly ISpecializationService _specService;
        private readonly ICompanyService _companyService;

        public ObservableCollection<Specialization> Specializations { get; }
            = new ObservableCollection<Specialization>();

        private Specialization _selectedSpecialization;
        public Specialization SelectedSpecialization
        {
            get => _selectedSpecialization;
            set
            {
                if (_selectedSpecialization != value)
                {
                    _selectedSpecialization = value;
                    OnPropertyChanged(nameof(SelectedSpecialization));
                }
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string _location = string.Empty;
        public string Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        private string _requirements = string.Empty;
        public string Requirements
        {
            get => _requirements;
            set
            {
                if (_requirements != value)
                {
                    _requirements = value;
                    OnPropertyChanged(nameof(Requirements));
                }
            }
        }

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                    if (EndDate < _startDate)
                        EndDate = _startDate;
                }
            }
        }

        private DateTime _endDate = DateTime.Today.AddDays(7);
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                    OnPropertyChanged(nameof(IsNotBusy));
                    ((Command)SaveCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsNotBusy => !IsBusy;

        public ICommand SaveCommand { get; }

        public CreateInternshipViewModel(
            IInternshipService internshipService,
            ISpecializationService specService,
            ICompanyService companyService)
        {
            _internshipService = internshipService;
            _specService = specService;
            _companyService = companyService;

            // команда блокируется, пока IsBusy = true
            SaveCommand = new Command(async () => await SaveAsync(), () => IsNotBusy);

            LoadSpecializations();
        }

        private async void LoadSpecializations()
        {
            try
            {
                var list = await _specService.GetAllAsync();
                Specializations.Clear();
                foreach (var s in list)
                    Specializations.Add(s);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Ошибка",
                    $"Не удалось загрузить специализации: {ex.Message}",
                    "OK");
            }
        }

        private async Task SaveAsync()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(Title)
                || SelectedSpecialization == null
                || string.IsNullOrWhiteSpace(Location)
                || string.IsNullOrWhiteSpace(Requirements))
            {
                await Shell.Current.DisplayAlert(
                    "Ошибка",
                    "Пожалуйста, заполните все поля.",
                    "OK");
                return;
            }

            IsBusy = true;
            try
            {
                int companyId = Preferences.Default.Get("CurrentCompanyId", 0);
                var company = await _companyService.GetByIdAsync(companyId);
                if (company == null)
                {
                    await Shell.Current.DisplayAlert(
                        "Ошибка",
                        "Не удалось определить вашу организацию.",
                        "OK");
                    return;
                }

                // проверяем, что специализация ещё есть в БД
                var spec = await _specService.GetByIdAsync(SelectedSpecialization.Id);
                if (spec == null)
                {
                    await Shell.Current.DisplayAlert(
                        "Ошибка",
                        "Выбранная специализация больше недоступна.",
                        "OK");
                    return;
                }

                var internship = new Internship
                {
                    Title = Title.Trim(),
                    Location = Location.Trim(),
                    Requirements = Requirements.Trim(),
                    StartDate = StartDate,
                    EndDate = EndDate,
                    IdCompany = company.Id,
                    IdSpecialization = spec.Id
                };

                await _internshipService.AddAsync(internship);
                // навигация по абсолютному пути
                await Shell.Current.GoToAsync("///CompanyProfile");
            }
            catch (DbUpdateException dbEx)
            {
                var realMsg = dbEx.InnerException?.Message ?? dbEx.Message;
                Debug.WriteLine(realMsg);
                await Shell.Current.DisplayAlert(
                    "Ошибка при сохранении",
                    realMsg,
                    "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                await Shell.Current.DisplayAlert(
                    "Ошибка",
                    ex.Message,
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
