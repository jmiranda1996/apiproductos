using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiProductos.Data;
using ApiProductos.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiProductos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DbConnection.striconn = Configuration["Connection:MSSQL"];
            DbConnection.dbprovid = DbProvider.MSSQL;

            services.AddSingleton<IProductsRepository, ProductsRepository>();

            services.AddSwaggerGen(x =>
            {
                OpenApiInfo Info = new OpenApiInfo()
                {
                    Version = "v0",
                    Title = "Api productos",
                    Description = "",
                    TermsOfService = new Uri("https://www.google.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Jerson Miranda",
                        Email = "jerson.miranda@outlook.es"
                    }
                };

                x.SwaggerDoc("Terceros", Info);

            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
            });

            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Policy");

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/Terceros/swagger.json", "Terceros");
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
