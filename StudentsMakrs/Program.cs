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

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


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

        //builder.Services.AddCors(
        //    options =>
        //    {
        //        options.AddPolicy("Admin", policy =>
        //        {
        //            policy.AllowAnyMethod();
        //            policy.AllowAnyOrigin();
        //            policy.AllowAnyHeader();
        //        });
        //        options.AddPolicy("Student", policy =>
        //        {
        //            policy.AllowAnyMethod();
        //            policy.AllowAnyOrigin();
        //            policy.AllowAnyHeader();
        //        });
        //        options.AddPolicy("User", policy =>
        //        {
        //            policy.AllowAnyMethod();
        //            policy.AllowAnyOrigin();
        //            policy.AllowAnyHeader();
        //        });
        //    }
        //);

        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
        //    options.AddPolicy("StudentOnly", policy => policy.RequireClaim("Student"));
        //    options.AddPolicy("UserOnly", policy => policy.RequireClaim("User"));
        //});

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
        app.MapPut("/Students/Put", (IStudentService service, Student student) => service.PutStudent(student));

        app.MapGet("/Students/All", (IStudentService service) => service.GetStudents());
        app.MapGet("/Students/Get/{id}", (string id, IStudentService service) => service.GetStudent(id));

        app.MapPost("/Department/Post", (IFacultyService service, Department department) => service.PostDepartment(department));
        app.MapPost("/Faculty/Post", (IFacultyService service, Faculty department) => service.PostFaculty(department));

        app.MapGet("/Department/All", (IFacultyService service) => service.GetDepartments());
        app.MapGet("/Faculty/All", (IFacultyService service) => service.GetFaculties());

        app.MapDelete("/Department/Delete/{id}", (IFacultyService service, int id) => service.DeleteDepartment(id));
        app.MapDelete("/Faculty/Delete/{id}", (IFacultyService service, int id) => service.DeleteFaculty(id));

        app.MapPost("/Subject/Post", (ISubjectService service, Subject subject) => service.Post(subject));
        app.MapGet("/Subject/All", (ISubjectService service) => service.Gets());

        app.MapPost("/Marks/Post", (IMarksService service, Mark mark) => service.PostMark(mark));
        app.MapGet("/Marks/All", (IMarksService service) => service.GetMarks());

        app.MapDelete("/Subject/Delete/{t}", (ISubjectService service, int t) => service.Delete(t));
        app.MapDelete("/Marks/Delete/{t}", (IMarksService service, int t) => service.DeleteMark(t));

        app.MapPost("/Students/{subject}/AddSubject", (IStudentService service, int subject, Student student) => service.AddSubjectToStudent(student, subject));
        app.MapPut("/Students/{subject}/DeleteSubject", (IStudentService service, int subject, Student student) => service.RemoveSubject(student, subject));

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        //app.UseAuthentication();
        //app.UseAuthorization();

        application = app;

        app.Run();
    }
}