using FluentValidation;
using ModelConverter.Common.Services;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services;
using ModelConverter.TaskManager.Services.Interfaces;
using ModelConverter.TaskManager.Validators;

namespace ModelConverter.TaskManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddScoped<IProcessIdProvider, ProcessIdProvider>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IProcessManager, ProcessManager>();
            services.AddSingleton<IProcessRepository<ConvertingProcess>, ConvertingProcessRepository>();
            services.AddScoped<IValidator<UploadRequest>, UploadRequestValidator>();
            services.AddScoped<IExceptionHandler, ExceptionHandler>();
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            builder.UseRouting();
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}