using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentsMakrs;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Services;

namespace StudentsMakrs.Client
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();


            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
            builder.Services.AddSingleton<IStudentService, StudentServiceClient>();
            builder.Services.AddSingleton<IFacultyService, FacultyServiceClient>();
            builder.Services.AddSingleton<ISubjectService, SubjectServiceClient>();
            builder.Services.AddSingleton<IMarksService, MarkServiceClient>();


            await builder.Build().RunAsync();
        }
    }
}