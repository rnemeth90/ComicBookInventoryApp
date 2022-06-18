using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ComicBookInventory.Shared;
using ComicBookInventory.DataAccess;
using ComicBookInventory.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Serilog;

namespace ComicBookInventory.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public IConfiguration? Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1,0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("version-string");
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComicBookInventory.Api", Version = "v1" });
            });
            services.AddDbContext<ApiDbContext>(options =>
                                                options.UseSqlServer(Environment.GetEnvironmentVariable("CBI_SQL_CONNECTION_STRING"),
                                                b => b.MigrationsAssembly(typeof(ApiDbContext).Assembly.FullName)));

            //register the repositories
            #region Repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            // serialize as XML if application/xml mime type specified in call
            services.AddMvc().AddXmlDataContractSerializerFormatters();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                AppDbInitializer.Seed(app);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicBookApi v1"));
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            //app.ConfigureBuiltInExceptionHandler();

            app.UseCors(configurePolicy: options =>
            {
                options.WithMethods("GET", "POST", "PUT", "DELETE");
                options.WithOrigins("https://localhost:6001");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/healthz", () => "Healthy");
                endpoints.MapControllers();
            });
        }
    }
}