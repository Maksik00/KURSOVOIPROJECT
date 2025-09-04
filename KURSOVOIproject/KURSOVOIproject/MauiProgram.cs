using Microsoft.EntityFrameworkCore;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Data;
using KURSOVOIproject.Services;
using KURSOVOIproject.ViewModels;
using KURSOVOIproject.Views;


namespace KURSOVOIproject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "sfl.db");
            builder.Services.AddDbContext<SflDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            // Существующие сервисы
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ISpecializationService, SpecializationService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IInternshipService, InternshipService>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();

            // Новые сервисы
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IRecommendationService, RecommendationService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
            builder.Services.AddScoped<IMessagingService, MessagingService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            /*builder.Services.AddScoped<IUserService, UserService>();*/ // для админа: Student + Company

            // ViewModel’и
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<SearchPageViewModel>();
            builder.Services.AddTransient<StudentProfileViewModel>();
            builder.Services.AddTransient<CompanyProfileViewModel>();

            // Новые ViewModel’ы:
            builder.Services.AddTransient<ResponsesPageViewModel>();
            builder.Services.AddTransient<InternshipDetailViewModel>();
            builder.Services.AddTransient<MessagingViewModel>();
            builder.Services.AddTransient<AdminLoginViewModel>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddTransient<StatisticsPageViewModel>();
            builder.Services.AddTransient<HelpViewModel>();
            // (HelpViewModel может быть пустым – просто для навигации)

            // Views
            builder.Services.AddTransient<LandingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<StudentProfilePage>();

            builder.Services.AddTransient<CompanyLoginPage>();
            builder.Services.AddTransient<CompanyRegistrationPage>();
            builder.Services.AddTransient<CreateInternshipPage>();
            builder.Services.AddTransient<CompanyProfilePage>();

            // Новые Views:
            builder.Services.AddTransient<ResponsesPage>();
            builder.Services.AddTransient<InternshipDetailPage>();
            builder.Services.AddTransient<MessagingPage>();
            builder.Services.AddTransient<AdminLoginPage>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<StatisticsPage>();
            builder.Services.AddTransient<HelpPage>();

            // Конвертер
            builder.Services.AddSingleton<Converters.BoolToHeartIconConverter>();

            var app = builder.Build();
            // Применяем миграции
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SflDbContext>();
                db.Database.Migrate();
            }

            return app;
        }
    }
}
