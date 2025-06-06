using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public partial class InternshipDetailViewModel : ObservableObject
    {
        private readonly IInternshipService _internshipService;
        private readonly IApplicationService _applicationService;

        public int InternshipId { get; set; }

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string companyName;

        [ObservableProperty]
        private string requirements;

        [ObservableProperty]
        private bool isApplied;

        public IAsyncRelayCommand LoadDetailsCommand { get; }
        public IAsyncRelayCommand ApplyCommand { get; }
        public IAsyncRelayCommand RevokeCommand { get; }

        public InternshipDetailViewModel(
            IInternshipService internshipService,
            IApplicationService applicationService)
        {
            _internshipService = internshipService;
            _applicationService = applicationService;

            LoadDetailsCommand = new AsyncRelayCommand(LoadAsync);
            ApplyCommand = new AsyncRelayCommand(ApplyAsync);
            RevokeCommand = new AsyncRelayCommand(RevokeAsync);
        }

        private async Task LoadAsync()
        {
            // TODO: Загрузить стажировку из _internshipService по InternshipId
            // TODO: Заполнить Title, CompanyName, Requirements
            // TODO: Проверить, откликался ли уже текущий студент
        }

        private async Task ApplyAsync()
        {
            // TODO: Сохранить отклик (создать ApplicationEntity через _applicationService)
            IsApplied = true;
        }

        private async Task RevokeAsync()
        {
            // TODO: Удалить отклик (ApplicationEntity) через _applicationService
            IsApplied = false;
        }
    }
}
