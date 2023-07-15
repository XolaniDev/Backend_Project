/*
 * [2019] - [2021] Eblocks Software (Pty) Ltd, All Rights Reserved.
 * NOTICE: All information contained herein is, and remains the property of Eblocks
 * Software (Pty) Ltd.
 * and its suppliers (if any). The intellectual and technical concepts contained herein
 * are proprietary
 * to Eblocks Software (Pty) Ltd. and its suppliers (if any) and may be covered by South 
 * African, U.S.
 * and Foreign patents, patents in process, and are protected by trade secret and / or 
 * copyright law.
 * Dissemination of this information or reproduction of this material is forbidden unless
 * prior written
 * permission is obtained from Eblocks Software (Pty) Ltd.
*/

#pragma warning disable CS1591

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using af.assessment.api.Converters;
using af.assessment.api.Data;
using af.assessment.api.Services;
using af.assessment.api.Stores;
using af.assessment.api.Utilities;
using FluentValidation.AspNetCore;
using af.assessment.api.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace af.assessment.api
{
    /// <summary>
    ///      The configuration class of the Startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        ///     A <see cref="IWebHostEnvironment"/> representing the hosting environment of the application.
        /// </summary>
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        ///     Instatiating the <see cref="CORS_POLICY"/>
        /// </summary>
        private readonly string CORS_POLICY = "CorsPolicy";

        /// <summary>
        ///     
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_POLICY, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers()
                .AddFluentValidation(fv =>
                   fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddAuthentication();

            //check to remove this 

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<VaccineDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                });

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IVaccineStore, VaccineStore>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserToken, UserToken>();

            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IMemberStore, MemberStore>();
            services.AddTransient<IRegisterDtoConverter, RegisterDtoConverter>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserStore, UserStore>();
            services.AddTransient<IUserDtoConverter, UserDtoConverter>();

            services.AddTransient<IVaccineCardService, VaccineCardService>();
            services.AddTransient<IFamilyMemberStore, FamilyMemberStore>();
            services.AddTransient<IVaccineCardDtoConverter, VaccineCardDtoConverter>();

            services.AddTransient<IResetPasswordService, ResetPasswordService>();
            services.AddTransient<IResetPasswordStore, ResetPasswordStore>();

            // Connection String to Postgres Database : 
            var connectionString = Configuration["ConnectionStrings:Postgres"];

            services.AddDbContext<VaccineDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Swagger Api for the Digitized Child Vaccine Application",
                    Description = $"An API that is so so cool served by machine - {Environment.MachineName}"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            IFileProvider fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CORS_POLICY);

            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Digitization of Child Vaccine Card API");
            });

            UpdateDatabase(app);
        }

        /// <summary>
        ///     Updates the database with the latest migrations.
        /// </summary>
        /// <param name="app">A <see cref="IApplicationBuilder"/> representing the application builder.</param>
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<VaccineDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}