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
            // TODO completed: загружаем детали стажировки и проверяем отклик
            var internship = await _internshipService.GetByIdAsync(InternshipId);
            if (internship == null)
                return;

            Title = internship.Title;
            CompanyName = internship.Company?.Name ?? string.Empty;
            Requirements = internship.Requirements;

            var studentId = Preferences.Default.Get("CurrentStudentId", 0);
            if (studentId != 0)
            {
                var apps = await _applicationService.GetByStudentAsync(studentId);
                IsApplied = apps.Any(a => a.IdInternship == InternshipId);
            }
        }

        private async Task ApplyAsync()
        {
            // TODO completed: создаём ApplicationEntity для текущего студента
            var studentId = Preferences.Default.Get("CurrentStudentId", 0);
            if (studentId == 0)
                return;

            var application = new ApplicationEntity
            {
                IdStudent = studentId,
                IdInternship = InternshipId,
                SubmissionDate = DateTime.UtcNow,
            };
            await _applicationService.AddAsync(application);
            IsApplied = true;
        }

        private async Task RevokeAsync()
        {
            // TODO completed: ищем заявку и удаляем её через сервис
            var studentId = Preferences.Default.Get("CurrentStudentId", 0);
            if (studentId == 0)
                return;
            var apps = await _applicationService.GetByStudentAsync(studentId);
            var app = apps.FirstOrDefault(a => a.IdInternship == InternshipId);
            if (app != null)
                await _applicationService.DeleteAsync(app.Id);
            IsApplied = false;
        }
    }
}
