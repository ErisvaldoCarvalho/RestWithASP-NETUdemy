
using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using RestWithASP_NETUdemy.Model.Context;
using RestWithASP_NETUdemy.Business;

using RestWithASP_NETUdemy.Business.Implementations;
using RestWithASP_NETUdemy.Repository;
using RestWithASP_NETUdemy.Repository.Implementations;

namespace RestWithASP_NETUdemy
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();

                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed.", ex);
                    throw;
                }
            }

            services.AddMvc();

            services.AddApiVersioning(option => option.ReportApiVersions = true);

            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("Ambiente de desenvolvimento");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            //loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestWithASP_NETUdemy.Model.Context;
using RestWithASP_NETUdemy.Business;
using RestWithASP_NETUdemy.Business.Implementations;
using RestWithASP_NETUdemy.Repository;
using RestWithASP_NETUdemy.Repository.Implementations;
using Microsoft.VisualBasic;
using Evolve;
using Microsoft.AspNetCore.Http;




namespace RestWithASP_NETUdemy
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostEnvironment _enviromment { get; }
        public Startup(IConfiguration configuration, IHostEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _enviromment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Do the service register here and extra stuff you want
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
                //etc
            });

            var connectionString = _configuration["MySqlConection:MySqlConectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if (_enviromment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    //TODO: Linha diferente da aula 84. 87 Adicionando Suporte À Migrations Parte 2
                    //var evolve = new Evolve.Evolve("volve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw;
                }
            }

            services.AddControllers();
            services.AddApiVersioning();
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {

            //TODO: Resolver as duas linhas abaixo que estão dando erro.
            //loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            //app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
*/
