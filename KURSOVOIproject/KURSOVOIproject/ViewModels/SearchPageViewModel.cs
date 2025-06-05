using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;   // Для ObservableObject и [ObservableProperty]
using CommunityToolkit.Mvvm.Input;            // Для IAsyncRelayCommand и AsyncRelayCommand
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    // Отдельная ViewModel для одного элемента списка стажировок
    public class InternshipItemViewModel : ObservableObject
    {
        private readonly IInternshipService _internshipService;

        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Requirements { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }

        public IAsyncRelayCommand ToggleFavoriteCommand { get; }

        public InternshipItemViewModel(IInternshipService internshipService)
        {
            _internshipService = internshipService;
            ToggleFavoriteCommand = new AsyncRelayCommand(ToggleFavoriteAsync);
        }

        private async Task ToggleFavoriteAsync()
        {
            // TODO: Здесь можно сохранить / удалить стажировку из избранного в БД или Preferences
            IsFavorite = !IsFavorite;
            await Task.CompletedTask;
        }
    }

    // ViewModel для SearchPage (поиска стажировок)
    public partial class SearchPageViewModel : ObservableObject
    {
        private readonly IInternshipService _internshipService;

        // Весь список стажировок (для фильтрации)
        private readonly ObservableCollection<InternshipItemViewModel> _allInternships
            = new ObservableCollection<InternshipItemViewModel>();

        // Отфильтрованный список, привязанный к CollectionView
        public ObservableCollection<InternshipItemViewModel> FilteredInternships { get; }
            = new ObservableCollection<InternshipItemViewModel>();

        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    SearchCommand.Execute(null);
                }
            }
        }

        [ObservableProperty]
        private bool isBusy;

        public bool IsNotBusy => !IsBusy;

        public IAsyncRelayCommand LoadInternshipsCommand { get; }
        public IAsyncRelayCommand SearchCommand { get; }

        public SearchPageViewModel(IInternshipService internshipService)
        {
            _internshipService = internshipService;

            // Команды для загрузки и фильтрации
            LoadInternshipsCommand = new AsyncRelayCommand(LoadAllAsync);
            SearchCommand = new AsyncRelayCommand(FilterAsync);
        }

        /// <summary>
        /// Загружает все стажировки из сервиса и заполняет коллекции.
        /// </summary>
        private async Task LoadAllAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                _allInternships.Clear();
                FilteredInternships.Clear();

                // Предполагаем, что IInternshipService.GetAllWithCompaniesAsync()
                // возвращает список Internship c загруженным навигационным свойством Company
                var list = await _internshipService.GetAllWithCompaniesAsync();

                foreach (var internship in list)
                {
                    var itemVm = new InternshipItemViewModel(_internshipService)
                    {
                        Id = internship.Id,
                        Title = internship.Title,
                        CompanyName = internship.Company?.Name ?? string.Empty,
                        Requirements = internship.Requirements,
                        StartDate = internship.StartDate,
                        EndDate = internship.EndDate,
                        IsFavorite = false // По умолчанию «не в избранном»
                    };

                    _allInternships.Add(itemVm);
                    FilteredInternships.Add(itemVm);
                }
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        /// <summary>
        /// Фильтрует _allInternships по текущей строке поиска SearchQuery
        /// и заполняет FilteredInternships.
        /// </summary>
        private Task FilterAsync()
        {
            if (IsBusy)
                return Task.CompletedTask;

            foreach (var item in _allInternships)
            {
                bool shouldShow = string.IsNullOrWhiteSpace(SearchQuery)
                    || item.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                    || item.CompanyName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);

                if (shouldShow && !FilteredInternships.Contains(item))
                {
                    FilteredInternships.Add(item);
                }
                else if (!shouldShow && FilteredInternships.Contains(item))
                {
                    FilteredInternships.Remove(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
