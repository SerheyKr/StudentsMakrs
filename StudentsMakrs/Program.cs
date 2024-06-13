using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    public static IEmailSender GetEmailSender()
    {
        var scope = application.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        
        return services.GetRequiredService<IEmailSender>();
    }

    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddRazorPages(options =>
        //{
        //    options.Conventions.AllowAnonymousToPage("/Account/Login");
        //    options.Conventions.AllowAnonymousToPage("/Account/Register");
        //    options.Conventions.AuthorizeFolder("/students", "Admin");
        //});

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.ConfigureApplicationCookie(o => {
            o.ExpireTimeSpan = TimeSpan.FromDays(5);
            o.SlidingExpiration = true;
        }); 
        builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
       o.TokenLifespan = TimeSpan.FromHours(3));
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("Student", policy => policy.RequireRole("Student"));
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        builder.Services.AddSingleton<IStudentService, StudentServiceServer>();
        builder.Services.AddSingleton<IFacultyService, FacultyServiceServer>();
        builder.Services.AddSingleton<ISubjectService, SubjectServiceServer>();
        builder.Services.AddSingleton<IMarksService, MarkServiceServer>();

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

            context.SaveChanges();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = new[] { "Admin", "Student", "User" };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole(){ Name = role });
                }
            }

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync("sergey43433434@gmail.com"), "Admin");
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(StudentsMakrs.Client._Imports).Assembly);

        app.MapDelete("/Students/Delete/{id}", (string id, IStudentService service) => service.DeleteStudent(id))
        .RequireAuthorization("Admin");

        app.MapPost("/Students/Post", (IStudentService service, Student student) => service.PostStudent(student))
        .RequireAuthorization("Admin");
        app.MapPut("/Students/Put", (IStudentService service, Student student) => service.PutStudent(student));

        app.MapGet("/Students/All", (IStudentService service) => service.GetStudents())
        .RequireAuthorization("Admin");
        app.MapGet("/Students/Get/{id}", (string id, IStudentService service) => service.GetStudent(id))
        .RequireAuthorization("Admin");

        app.MapGet("/Certificate/Get/{ID}/{Password}", 
        (string ID, string Password, IStudentService service) => service.GetStudentAnon(new CertificateData()
        {
            ID = ID,
            Password = Password,
        }));

        app.MapPost("/Department/Post", (IFacultyService service, Department department) => service.PostDepartment(department))
        .RequireAuthorization("Admin");
        app.MapPost("/Faculty/Post", (IFacultyService service, Faculty department) => service.PostFaculty(department))
        .RequireAuthorization("Admin");

        app.MapGet("/Department/All", (IFacultyService service) => service.GetDepartments());
        app.MapGet("/Faculty/All", (IFacultyService service) => service.GetFaculties());

        app.MapDelete("/Department/Delete/{id}", (IFacultyService service, int id) => service.DeleteDepartment(id))
        .RequireAuthorization("Admin");
        app.MapDelete("/Faculty/Delete/{id}", (IFacultyService service, int id) => service.DeleteFaculty(id))
        .RequireAuthorization("Admin");

        app.MapPost("/Subject/Post", (ISubjectService service, Subject subject) => service.Post(subject))
        .RequireAuthorization("Admin");
        app.MapGet("/Subject/All", (ISubjectService service) => service.Gets());

        app.MapPost("/Marks/Post", (IMarksService service, Mark mark) => service.PostMark(mark))
        .RequireAuthorization("Admin");
        app.MapGet("/Marks/All", (IMarksService service) => service.GetMarks());

        app.MapDelete("/Subject/Delete/{t}", (ISubjectService service, int t) => service.Delete(t))
        .RequireAuthorization("Admin");
        app.MapDelete("/Marks/Delete/{t}", (IMarksService service, int t) => service.DeleteMark(t))
        .RequireAuthorization("Admin");

        app.MapPost("/Students/{subject}/AddSubject", (IStudentService service, int subject, Student student) => service.AddSubjectToStudent(student, subject))
        .RequireAuthorization("Admin");
        app.MapPut("/Students/{subject}/DeleteSubject", (IStudentService service, int subject, Student student) => service.RemoveSubject(student, subject))
        .RequireAuthorization("Admin");


        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();


        application = app;
        app.Run();
    }
}