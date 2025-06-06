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

            // Студент
            Routing.RegisterRoute("Login", typeof(Views.LoginPage));
            Routing.RegisterRoute("Registration", typeof(Views.RegistrationPage));
            Routing.RegisterRoute("Search", typeof(Views.SearchPage));
            Routing.RegisterRoute("StudentProfile", typeof(Views.StudentProfilePage));

            // Компания
            Routing.RegisterRoute("CompanyLogin", typeof(Views.CompanyLoginPage));
            Routing.RegisterRoute("CompanyRegistration", typeof(Views.CompanyRegistrationPage));
            Routing.RegisterRoute("CompanyProfile", typeof(Views.CompanyProfilePage));
            Routing.RegisterRoute("CreateInternship", typeof(Views.CreateInternshipPage));

            // Администратор
            Routing.RegisterRoute("AdminLogin", typeof(Views.AdminLoginPage));
            Routing.RegisterRoute("Admin", typeof(Views.AdminPage));
            Routing.RegisterRoute("Statistics", typeof(Views.StatisticsPage));
            Routing.RegisterRoute("UsersList", typeof(Views.UsersListPage));
            Routing.RegisterRoute("AllInternships", typeof(Views.AllInternshipsPage));
            Routing.RegisterRoute("Responses", typeof(Views.ResponsesPage));
            Routing.RegisterRoute("Help", typeof(Views.HelpPage));
        }
    }
}
