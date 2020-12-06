using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Components;
using Sample.Contracts;

namespace Sample.Api
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
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(cfg => 
            { 
                // cfg.AddConsumer<SubmitOrderConsumer>(); 
                // cfg.AddMediator();
                cfg.AddBus(providerContext => Bus.Factory.CreateUsingRabbitMq());
                cfg.AddRequestClient<SubmitOrder>(new Uri($"exchange:{KebabCaseEndpointNameFormatter.Instance.Consumer<SubmitOrderConsumer>()}"));
                cfg.AddRequestClient<CheckOrder>();
            });
            services.AddMassTransitHostedService();

            services.AddOpenApiDocument(cfg => cfg.PostProcess = d => d.Info.Title = "SampleApi");

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
