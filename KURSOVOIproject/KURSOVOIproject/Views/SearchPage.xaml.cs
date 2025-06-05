using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;

namespace KURSOVOIproject.Views
{
    public partial class SearchPage : ContentPage
    {
        private readonly IInternshipService _internshipService;
        public ObservableCollection<Internship> Internships { get; set; }

        public SearchPage(IInternshipService internshipService)
        {
            InitializeComponent();
            _internshipService = internshipService;
            Internships = new ObservableCollection<Internship>();
            InternshipsCollection.ItemsSource = Internships;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAllInternshipsAsync();
        }

        private async Task LoadAllInternshipsAsync()
        {
            Internships.Clear();
            var list = await _internshipService.GetAllWithCompaniesAsync();
            foreach (var i in list)
                Internships.Add(i);
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string query = SearchEntry.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(query))
            {
                await LoadAllInternshipsAsync();
                return;
            }

            Internships.Clear();
            var filtered = await _internshipService.SearchByTitleAsync(query);
            foreach (var i in filtered)
                Internships.Add(i);
        }

        private async void OnFilterForYouClicked(object sender, EventArgs e)
        {
            // TODO: Ваша логика фильтра "Для вас"
            await DisplayAlert("Фильтр", "Включён фильтр 'Для вас'", "OK");
        }

        private async void OnFilterInternshipClicked(object sender, EventArgs e)
        {
            // TODO: Ваша логика фильтра "Стажировка"
            await DisplayAlert("Фильтр", "Включён фильтр 'Стажировка'", "OK");
        }

        private async void OnSaveInternshipClicked(object sender, EventArgs e)
        {
            // Сохранить стажировку в "Избранное" (например, через IInternshipService)
            await DisplayAlert("Избранное", "Стажировка сохранена в избранное", "OK");
        }

        // Навигация по нижнему меню:
        private async void OnNavSearchClicked(object sender, EventArgs e)
        {
            // Уже на Search, ничего не делаем
        }

        private async void OnNavFavoritesClicked(object sender, EventArgs e)
        {
            // TODO: Переход на страницу Избранного
            await DisplayAlert("Избранное", "Перейти на Favorites", "OK");
        }

        private async void OnNavResponsesClicked(object sender, EventArgs e)
        {
            // TODO: Переход на страницу Откликов
            await DisplayAlert("Отклики", "Перейти на Responses", "OK");
        }

        private async void OnNavMessagesClicked(object sender, EventArgs e)
        {
            // TODO: Переход на страницу Сообщений
            await DisplayAlert("Сообщения", "Перейти на Messages", "OK");
        }

        private async void OnNavProfileClicked(object sender, EventArgs e)
        {
            // Переход на профиль студента
            bool isStudentLoggedIn = Preferences.Default.Get("IsStudentLoggedIn", false);
            if (isStudentLoggedIn)
                await Shell.Current.GoToAsync("//StudentProfile");
            else
                await DisplayAlert("Ошибка", "Пожалуйста, войдите или зарегистрируйтесь", "OK");
        }
    }
}
