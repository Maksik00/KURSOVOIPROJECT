using Microsoft.Maui.Controls;
using KURSOVOIproject.Views;

namespace KURSOVOIproject
{
    public partial class AppShell : Shell
    {
        private bool _hasFirstAppeared = false;

        public AppShell()
        {
            InitializeComponent();

            // Регистрируем маршруты, чтобы Shell точно знал про все Page’ы
            Routing.RegisterRoute("Landing", typeof(LandingPage));
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("Registration", typeof(RegistrationPage));
            Routing.RegisterRoute("Search", typeof(SearchPage));
            Routing.RegisterRoute("StudentProfile", typeof(StudentProfilePage));

            Routing.RegisterRoute("CompanyLogin", typeof(CompanyLoginPage));
            Routing.RegisterRoute("CompanyRegistration", typeof(CompanyRegistrationPage));
            Routing.RegisterRoute("CreateInternship", typeof(CreateInternshipPage));
            Routing.RegisterRoute("CompanyProfile", typeof(CompanyProfilePage));

            // Подписываемся один раз на запуск Shell → сразу показываем Landing
            this.Appearing += AppShell_Appearing;
        }

        private async void AppShell_Appearing(object sender, System.EventArgs e)
        {
            if (_hasFirstAppeared)
                return;

            _hasFirstAppeared = true;

            // Абсолютная навигация: сбрасываем стек и открываем Landing как корень
            await Shell.Current.GoToAsync("//Landing");
        }
    }
}
