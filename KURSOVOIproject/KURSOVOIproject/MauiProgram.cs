using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
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

            // 1) Указываем, что App – это главная точка входа
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 2) Подключаем SQLite (SflDbContext)
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "sfl.db");
            builder.Services.AddDbContext<SflDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            // 3) Регистрируем сервисы (DAOs / репозитории)
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ISpecializationService, SpecializationService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IInternshipService, InternshipService>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();

            // 4) Регистрируем ViewModel-ы
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<SearchPageViewModel>();
            builder.Services.AddTransient<StudentProfileViewModel>();
            builder.Services.AddTransient<CompanyProfileViewModel>();

            // 5) Регистрируем все View (Pages). Порядок неважен, главное, чтобы названия совпадали:
            builder.Services.AddTransient<LandingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<StudentProfilePage>();

            builder.Services.AddTransient<CompanyLoginPage>();
            builder.Services.AddTransient<CompanyRegistrationPage>();
            builder.Services.AddTransient<CreateInternshipPage>();
            builder.Services.AddTransient<CompanyProfilePage>();

            // 6) Регистрируем конвертер (например, BoolToHeartIconConverter) – 
            //    чтобы его можно было использовать через {StaticResource ...}
            builder.Services.AddSingleton<Converters.BoolToHeartIconConverter>();

            var app = builder.Build();

            // 7) При первом запуске создаём (или применяем миграции) к SQLite
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SflDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}
