<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
=======
﻿using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
>>>>>>> 359d440 (InternshipADDING!)
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;
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
<<<<<<< HEAD
=======

>>>>>>> 359d440 (InternshipADDING!)
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

<<<<<<< HEAD
            // SQLite
=======
            // ─── Путь к файлу БД ───────────────────────────────
>>>>>>> 359d440 (InternshipADDING!)
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "sfl.db");

<<<<<<< HEAD
            // Существующие сервисы
=======
            // ─── Регистрация EF Core DbContext ────────────────
            builder.Services.AddDbContext<SflDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // ─── Конвертеры ────────────────────────────────────
            builder.Services.AddSingleton<Converters.BoolToHeartIconConverter>();

            // ─── Сервисы ───────────────────────────────────────
>>>>>>> 359d440 (InternshipADDING!)
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ISpecializationService, SpecializationService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IInternshipService, InternshipService>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IRecommendationService, RecommendationService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
            builder.Services.AddScoped<IMessagingService, MessagingService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

<<<<<<< HEAD
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
=======
            // ─── Shell ─────────────────────────────────────────
            builder.Services.AddSingleton<AppShell>();

            // ─── Pages + ViewModels ───────────────────────────
            RegisterPage<AdminLoginPage, AdminLoginViewModel>(builder);
            RegisterPage<AdminPage, AdminPageViewModel>(builder);
            RegisterPage<AllInternshipsPage, AllInternshipsPageViewModel>(builder);
            RegisterPage<CompanyProfilePage, CompanyProfileViewModel>(builder);
            RegisterPage<CreateInternshipPage, CreateInternshipViewModel>(builder);
            RegisterPage<HelpPage, HelpViewModel>(builder);
            RegisterPage<InternshipDetailPage, InternshipDetailViewModel>(builder);
            RegisterPage<LoginPage, LoginViewModel>(builder);
            RegisterPage<MessagingPage, MessagingViewModel>(builder);
            RegisterPage<RegistrationPage, RegistrationViewModel>(builder);
            RegisterPage<ResponsesPage, ResponsesPageViewModel>(builder);
            RegisterPage<SearchPage, SearchPageViewModel>(builder);
            RegisterPage<StatisticsPage, StatisticsPageViewModel>(builder);
            RegisterPage<StudentProfilePage, StudentProfileViewModel>(builder);
            RegisterPage<UsersListPage, UsersListPageViewModel>(builder);

            var app = builder.Build();

            // ─── Инициализация БД и сидирование ───────────────
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SflDbContext>();

                // Полное пересоздание схемы
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // Сидируем только если нет записей
                if (!db.Specializations.Any())
                {
                    db.Specializations.AddRange(
                        new Specialization { Name = "Программирование" },
                        new Specialization { Name = "Правоведение" },
                        new Specialization { Name = "Логистика" },
                        new Specialization { Name = "Банковское дело" },
                        new Specialization { Name = "Коммерция" }
                    );
                    db.SaveChanges();
                }
>>>>>>> 359d440 (InternshipADDING!)
            }

            return app;
        }

        /// <summary>
        /// Универсальный метод для регистрации Page + VM
        /// </summary>
        private static void RegisterPage<TPage, TViewModel>(MauiAppBuilder builder)
            where TPage : class
            where TViewModel : class
        {
            builder.Services.AddTransient<TViewModel>();
            builder.Services.AddTransient<TPage>();
        }
    }
}
