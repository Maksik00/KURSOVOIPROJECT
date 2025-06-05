using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public class CompanyProfileViewModel : BaseViewModel
    {
        private readonly ICompanyService _companyService;

        private string _name = "Компания";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city = "";
        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        private string _street = "";
        public string Street
        {
            get => _street;
            set
            {
                if (_street != value)
                {
                    _street = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        private string _building = "";
        public string Building
        {
            get => _building;
            set
            {
                if (_building != value)
                {
                    _building = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        private string _contactInfo = "";
        public string ContactInfo
        {
            get => _contactInfo;
            set
            {
                if (_contactInfo != value)
                {
                    _contactInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        // Составное свойство «Адрес»
        public string Address => $"{City}, {Street}, д. {Building}";

        public CompanyProfileViewModel(ICompanyService companyService)
        {
            _companyService = companyService;
            LoadCompanyData();
        }

        private async void LoadCompanyData()
        {
            int compId = Preferences.Default.Get("CurrentCompanyId", 0);
            if (compId == 0) return;

            Company? company = await _companyService.GetByIdAsync(compId);
            if (company == null) return;

            Name = company.Name;
            City = company.City;
            Street = company.Street;
            Building = company.Building;
            ContactInfo = company.Email; // теперь используем реальное поле Email

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Street));
            OnPropertyChanged(nameof(Building));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(ContactInfo));
        }
    }
}
