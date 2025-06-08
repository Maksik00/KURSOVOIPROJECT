using System;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Landing", typeof(Views.LandingPage));

<<<<<<< HEAD
            // Студент
            Routing.RegisterRoute("Login", typeof(Views.LoginPage));
            Routing.RegisterRoute("Registration", typeof(Views.RegistrationPage));
            Routing.RegisterRoute("Search", typeof(Views.SearchPage));
            Routing.RegisterRoute("StudentProfile", typeof(Views.StudentProfilePage));

            // Компания
=======
            // Регистрация всех маршрутов
            Routing.RegisterRoute("Landing", typeof(Views.LandingPage));
            Routing.RegisterRoute("Registration", typeof(Views.RegistrationPage));
            Routing.RegisterRoute("Search", typeof(Views.SearchPage));
            Routing.RegisterRoute("Login", typeof(Views.LoginPage));
            Routing.RegisterRoute("AllInternships", typeof(Views.AllInternshipsPage));
            Routing.RegisterRoute("Responses", typeof(Views.ResponsesPage));
            Routing.RegisterRoute("StudentProfile", typeof(Views.StudentProfilePage));

>>>>>>> 359d440 (InternshipADDING!)
            Routing.RegisterRoute("CompanyLogin", typeof(Views.CompanyLoginPage));
            Routing.RegisterRoute("CompanyRegistration", typeof(Views.CompanyRegistrationPage));
            Routing.RegisterRoute("CompanyProfile", typeof(Views.CompanyProfilePage));
            Routing.RegisterRoute("CreateInternship", typeof(Views.CreateInternshipPage));

<<<<<<< HEAD
            // Администратор
=======
            Routing.RegisterRoute("Help", typeof(Views.HelpPage));

>>>>>>> 359d440 (InternshipADDING!)
            Routing.RegisterRoute("AdminLogin", typeof(Views.AdminLoginPage));
            Routing.RegisterRoute("Admin", typeof(Views.AdminPage));
            Routing.RegisterRoute("Statistics", typeof(Views.StatisticsPage));
            Routing.RegisterRoute("UsersList", typeof(Views.UsersListPage));
<<<<<<< HEAD
            Routing.RegisterRoute("AllInternships", typeof(Views.AllInternshipsPage));
            Routing.RegisterRoute("Responses", typeof(Views.ResponsesPage));
            Routing.RegisterRoute("Help", typeof(Views.HelpPage));
=======
        }

        private async void OnAdminLoginClicked(object sender, EventArgs e)
        {
            await GoToAsync("AdminLogin");
        }

        private async void OnAdminPanelClicked(object sender, EventArgs e)
        {
            await GoToAsync("//Admin");
        }

        private async void OnHelpClicked(object sender, EventArgs e)
        {
            await GoToAsync("Help");
>>>>>>> 359d440 (InternshipADDING!)
        }
    }
}
