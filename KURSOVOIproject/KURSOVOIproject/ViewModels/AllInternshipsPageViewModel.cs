using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public partial class AllInternshipsPageViewModel : ObservableObject
    {
        private readonly IInternshipService _internshipService;

        [ObservableProperty]
        private bool isBusy;

        public ObservableCollection<Internship> Internships { get; }
            = new ObservableCollection<Internship>();

        public IAsyncRelayCommand LoadInternshipsCommand { get; }
        public IAsyncRelayCommand<int> DeleteInternshipCommand { get; }

        public AllInternshipsPageViewModel(IInternshipService internshipService)
        {
            _internshipService = internshipService;
            LoadInternshipsCommand = new AsyncRelayCommand(LoadAllAsync);
            DeleteInternshipCommand = new AsyncRelayCommand<int>(DeleteAsync);
        }

        private async Task LoadAllAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Internships.Clear();
                // TODO completed: получаем список всех стажировок
                var list = await _internshipService.GetAllAsync();
                foreach (var i in list)
                {
                    Internships.Add(i);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteAsync(int internshipId)
        {
            // TODO completed: вызываем сервис для удаления и перезагружаем список
            await _internshipService.DeleteAsync(internshipId);
            await LoadAllAsync();
        }
    }
}
