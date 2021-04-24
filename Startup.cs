using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Label.API.Config;
using Label.API.DataContext;
using Label.API.Repositories;
using Label.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Label.API
{
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

            services.AddAutoMapper(typeof(Startup));

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<LabelContext>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Label.API", Version = "v1" });
            });
            services.AddCors(options => { options.AddPolicy("AnyOrigin", builder => { builder.AllowAnyOrigin().AllowAnyMethod(); }); });



            services.AddTransient<ILabelContext, LabelContext>();
            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<IRecordlabelRepository, RecordlabelRepository>();
            services.AddTransient<ISongRepository, SongRepository>();

            services.AddTransient<ILabelService, LabelService>();

            // services.AddMvc();
            // 1. Add Authentication Services
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(options =>
            // {
            //     options.Authority = "https://dev-ynue-bw8.eu.auth0.com/";
            //     options.Audience = "https://labelapi.com";
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Label.API v1"));
            }

            app.UseHttpsRedirection();

            // Testing


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // app.UseStaticFiles();

            // 2. Enable authentication middleware
            // app.UseAuthentication();

            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //       name: "default",
            //       template: "{controller=Home}/{action=Index}/{id?}");
            // });
        }
    }
}
