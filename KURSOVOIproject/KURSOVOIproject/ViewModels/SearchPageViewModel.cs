using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;   // от Microsoft.Toolkit.Mvvm (MVVM Toolkit)
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    // Простая модель элемента фильтра
    public class FilterItemViewModel : ObservableObject
    {
        public string Name { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public FilterItemViewModel(string name)
        {
            Name = name;
        }
    }

    // Модель для отображения одной стажировки
    public class InternshipItemViewModel : ObservableObject
    {
        private readonly IInternshipService _internshipService;

        // Эти поля приходят из Internship модели/DTO
        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }

        // Конструктор, принимающий сервис, чтобы иметь возможность сохранять избранное
        public InternshipItemViewModel(IInternshipService internshipService)
        {
            _internshipService = internshipService;
        }
    }

    // Основная ViewModel для SearchPage
    public class SearchPageViewModel : ObservableObject
    {
        private readonly IInternshipService _internshipService;

        // Исходный полный список стажировок (загружается из сервиса)
        private ObservableCollection<InternshipItemViewModel> _allInternships;

        // Список отфильтрованных/сортированных стажировок
        public ObservableCollection<InternshipItemViewModel> FilteredInternships { get; }
            = new ObservableCollection<InternshipItemViewModel>();

        // Горизонтальные фильтры
        public ObservableCollection<FilterItemViewModel> Filters { get; }
    }
}
