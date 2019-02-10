using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MailSubscriptionsApi
{
    public class Startup
    {
        public IConfiguration configuration;
        private ILogger<Startup> logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<SubscriptionsContext>(config =>
            {
                var connectionString = this.configuration.GetConnectionString("default");
                config.UseSqlServer(connectionString);
            });
            services.AddTransient<IMailingService, MailingService>();
            services.AddTransient<IRestService, RestService>();
            services.AddScoped<ITopicsService, TopicsService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var corsOrigin = this.configuration.GetValue<string>(ConfigurationConstants.CorsOriginKey, string.Empty);
            app.UseCors(policy =>
            {
                policy.WithOrigins(corsOrigin);
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });

            this.logger.LogInformation($"Cors origin set {corsOrigin}");
            app.UseMvc();
        }
    }
}
