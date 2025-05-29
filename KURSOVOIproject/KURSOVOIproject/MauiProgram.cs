using Microsoft.EntityFrameworkCore;
using KURSOVOIproject.Data;        // <-- заменили YourAppNamespace на KURSOVOIproject
using Microsoft.Maui.Storage;
using System.IO;
using KURSOVOIproject.Services;
using KURSOVOIproject.ViewModels;
using KURSOVOIproject.Views;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        // … ваши остальные настройки

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "sfl.db");
        builder.Services.AddDbContext<SflDbContext>(opts =>
            opts.UseSqlite($"Filename={dbPath}"));
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<IInternshipService, InternshipService>();
        builder.Services.AddScoped<IApplicationService, ApplicationService>();
        builder.Services.AddScoped<ISpecializationService, SpecializationService>();
        builder.Services.AddTransient<RegistrationViewModel>();
        builder.Services.AddTransient<RegistrationPage>();


        return builder.Build();
    }
}
