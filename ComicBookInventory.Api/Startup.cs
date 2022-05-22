using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ComicBookInventory.Shared;
using ComicBookInventory.DataAccess;

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
            //services.AddTransient<ComicBookRepository>();
            //services.AddTransient<AuthorRepository>();
            //services.AddTransient<CharacterRepository>();   
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IComicBookRepository, ComicBookRepository>();
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
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/healthz", () => "Healthy");
                endpoints.MapControllers();
            });
        }
    }
}