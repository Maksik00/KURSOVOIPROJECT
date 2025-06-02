using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;
using System.IO;
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

            // 1) Указываем MAUI, что App – это наше приложение
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    // Регистрация шрифтов (опционально)
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 2) Настраиваем путь к SQLite базе (копируется в AppDataDirectory)
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "sfl.db");
            builder.Services.AddDbContext<SflDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            // 3) Регистрируем сервисы (репозитории)
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ISpecializationService, SpecializationService>();
            // (другие сервисы добавим позже по мере необходимости)

            // 4) Регистрируем все ViewModel и Pages через DI:

            // → Начальный экран (LandingPage)
            builder.Services.AddTransient<LandingPage>();

            // → Экран входа
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            // → Экран регистрации
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<RegistrationPage>();

            // → Заглушка для SearchPage (чтобы Login мог на неё перейти)
            builder.Services.AddTransient<SearchPage>();

            var app = builder.Build();

            // 5) При первом запуске создаём базу (Таблицы)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SflDbContext>();
                db.Database.EnsureCreated();
            }

            return app;
        }
    }
}
