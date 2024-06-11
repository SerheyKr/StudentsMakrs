using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;
using StudentsMakrs.Client.Pages;
using StudentsMakrs.Components;
using StudentsMakrs.Components.Account;
using StudentsMakrs.Data;
using StudentsMakrs.Services;
using static System.Net.Mime.MediaTypeNames;

public static class Program
{
    private static WebApplication application;
    public static ApplicationDbContext GetDBContext()
    {
        var scope = application.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;

        return services.GetRequiredService<ApplicationDbContext>();
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        builder.Services.AddSingleton<IStudentService, StudentServiceServer>();
        builder.Services.AddSingleton<IFacultyService, FacultyServiceServer>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(StudentsMakrs.Client._Imports).Assembly);

        app.MapDelete("/Students/Delete/{id}", (string id, IStudentService service) => service.DeleteStudent(id));

        app.MapPost("/Students/Post", (IStudentService service, Student student) => service.PostStudent(student));
        app.MapPost("/Department/Post", (IFacultyService service, Department department) => service.PostDepartment(department));
        app.MapPost("/Faculty/Post", (IFacultyService service, Faculty department) => service.PostFaculty(department));

        app.MapGet("/Department/All", (IFacultyService service) => service.GetDepartments());
        app.MapGet("/Faculty/All", (IFacultyService service) => service.GetFaculties());
        app.MapGet("/Students/All", (IStudentService service) => service.GetStudents());

        app.MapGet("/Students/Get/{id}", (string id, IStudentService service) => service.GetStudent(id));

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        application = app;

        app.Run();
    }
}