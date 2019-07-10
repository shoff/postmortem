using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PostMortem.Web
{
    using AutoMapper;
    using Config;
    using Converters;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Services;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddAutoMapper(typeof(ProjectProfile).Assembly);
            services.Configure<MongoOptions>(this.Configuration.GetSection("MongoOptions"));
            services.AddHttpClient<INameGeneratorClient, NameGeneratorClient>();
            services.AddTransient<IRepository, Repository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors();
            //    options =>  {
            //    options.AddDefaultPolicy (
            //        builder =>  { 
            //            builder
            //                .AllowAnyOrigin()
            //                .AllowAnyMethod()
            //                .AllowAnyHeader();
            //        });
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Postmortem API",
                    Description = "Postmortem API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Postmortem", Email = "steve.hoff@identifix.com" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Postmortem API");
            });
            app.UseMvc();
        }
    }
}
