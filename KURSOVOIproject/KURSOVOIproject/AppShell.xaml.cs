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

            // Явная (дублирующая) регистрация маршрутов (не обязательна,
            // но гарантирует, что Shell «знает» про LandingPage и т. д.)
            Routing.RegisterRoute("LandingPage", typeof(LandingPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("SearchPage", typeof(SearchPage));

            // Подписываемся на событие Appearing у Shell
            this.Appearing += AppShell_Appearing;
        }

        private async void AppShell_Appearing(object sender, System.EventArgs e)
        {
            // Выполняем переход на LandingPage только ПРИ ПЕРВОМ ОТОБРАЖЕНИИ Shell
            if (_hasFirstAppeared)
                return;

            _hasFirstAppeared = true;

            // Абсолютная навигация: сбрасываем стек и открываем LandingPage как корень
            await Shell.Current.GoToAsync("LandingPage");
        }
    }
}
