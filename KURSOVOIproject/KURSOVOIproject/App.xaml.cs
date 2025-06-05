using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;      // Чтобы увидеть Preferences.Default
using Microsoft.Maui.Hosting;

namespace KURSOVOIproject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // 1) Назначаем AppShell в качестве главной страницы
            MainPage = new AppShell();

            // 2) Сразу после старта проверяем, залогинен ли кто-нибудь, 
            //    чтобы перейти сразу в нужный профиль (студент или компания).
            bool isStudentLogged = Preferences.Default.Get("IsStudentLoggedIn", false);
            bool isCompanyLogged = Preferences.Default.Get("IsCompanyLoggedIn", false);

            if (isStudentLogged)
            {
                // Сбрасываем стек и переходим сразу в профиль студента
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Обратите внимание: маршрут "StudentProfile" определён в AppShell.xaml
                    await Shell.Current.GoToAsync("//StudentProfile");
                });
            }
            else if (isCompanyLogged)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Маршрут "CompanyProfile" определён в AppShell.xaml
                    await Shell.Current.GoToAsync("//CompanyProfile");
                });
            }
        }

        // Для WinUI (desktop) нужно явно переопределить CreateWindow
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage);
        }
    }
}
