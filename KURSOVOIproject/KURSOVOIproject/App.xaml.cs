using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Назначаем AppShell в качестве главной страницы приложения
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage);
        }
    }
}
