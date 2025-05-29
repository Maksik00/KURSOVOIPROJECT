using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Shell.Current.GoToAsync("//RegistrationPage");
        }

        // Для Windows/macCatalyst и остальных MAUI-платформ
        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Тут мы говорим, чтобы приложение рендерило именно наш Shell
            return new Window(new AppShell());
        }
    }
}
