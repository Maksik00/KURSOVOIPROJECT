using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.Views
{
    public partial class CreateInternshipPage : ContentPage
    {
        private readonly IInternshipService _internshipService;

        public CreateInternshipPage(IInternshipService internshipService)
        {
            InitializeComponent();
            _internshipService = internshipService;
        }

        private async void OnSaveInternshipClicked(object sender, EventArgs e)
        {
            string title = TitleEntry.Text?.Trim() ?? "";
            DateTime start = StartDatePicker.Date;
            DateTime end = EndDatePicker.Date;
            string requirements = RequirementsEditor.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(title) ||
                end < start ||
                string.IsNullOrWhiteSpace(requirements))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, проверьте все поля.", "OK");
                return;
            }

            try
            {
                // Предположим, что вы храните CurrentCompanyId в Preferences
                int companyId = Preferences.Default.Get("CurrentCompanyId", 0);
                if (companyId == 0)
                {
                    await DisplayAlert("Ошибка", "Компания не найдена. Пожалуйста, войдите снова.", "OK");
                    return;
                }

                var internship = new Internship
                {
                    Title = title,
                    StartDate = start,
                    EndDate = end,
                    Requirements = requirements,
                    IdCompany = companyId
                };

                await _internshipService.AddAsync(internship);

                await DisplayAlert("Успех", "Стажировка успешно создана", "OK");
                // После создания можно вернуться на CompanyProfilePage
                await Shell.Current.GoToAsync("CompanyProfile");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}
