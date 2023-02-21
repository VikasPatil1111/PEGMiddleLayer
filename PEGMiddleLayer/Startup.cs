using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEGMiddleLayer.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EntityFrameworkCore.Ase;

using PEGMiddleLayer.Data;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Sales.Masters;
using PEGMiddleLayer.Models.Sales.Utility;
using PEGMiddleLayer.Models.Dashboard;
using PEGMiddleLayer.Models.Dashboard.Booking;
using PEGMiddleLayer.Models.Dashboard.MISReport;
using PEGMiddleLayer.Models.Dashboard.Backlog;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.Dashboard.Invoice;
using PEGMiddleLayer.DIPattern;
using PEGMiddleLayer.Models.Dashboard.InvoiceMargin;

namespace PEGMiddleLayer
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
            services.AddCors();

            services.AddControllers();
            services.Configure<ForwardedHeadersOptions>(options => {
                options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
            });
            services.AddControllers().AddNewtonsoftJson();
            //for Entity Framework SQL Server6
             services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SQLConnection")),ServiceLifetime.Transient);
            // services.AddDbContext<CompanyDbContext>(option => option.UseAse(Configuration.GetConnectionString("SybaseConnection")), ServiceLifetime.Transient);
            //services.AddDbContext<UsersDbContext>(option => option.UseAse(Configuration.GetConnectionString("SybaseConnection")));
            //services.AddDbContext<ApplicationDbContext>(option => 
            //option.UseAse(Configuration.GetConnectionString("SybaseConnection")));
            //for Entity Framework SQL Server6
            //services.AddDbContext<ApplicationDbContextAse>(option => option.UseSqlServer(Configuration.GetConnectionString("SybaseConnection")));

            //services.AddDbContext<ApplicationDbContext>(option => option.Uses(Configuration.GetConnectionString("SQLConnection")));

            //for Indentity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICompanyMasterRepository, CompanyRepository>();
            services.AddScoped<ICustomerMasterRepository, CustomerMasterRepository>();
            services.AddScoped<IConsignee_Type_MasterRepository, Consignee_Type_MasterRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IBookingDashboardRepository, BookingDashboardRepository>();
            services.AddScoped<IMISReportRepository, MISReportRepository>();
            services.AddScoped<IBackLogOrderRepository, BackLogOrderRepository>();
            services.AddScoped<ItblBI_PeriodRepository, tblBI_PeriodRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ICommanRepository, CommanRepository>();
            services.AddScoped<IInvoiceMarginRepository, InvoiceMarginRepository>();
            //Adding Authenticatio
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })

                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime =true,//newly added on 26122022 
                        ValidateIssuerSigningKey = true, //newly added on 26122022 
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:secret"]))

                    };
                });
            services.AddDbContext<CompanyDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //  app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PEGMiddleLayer v1"));
            }

            // app.UseSession();

            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
          
            app.UseRouting();

            app.UseCors(options => options
                .WithOrigins(new[] {  "http://192.9.201.12", 
                    "http://localhost:3000", 
                    "http://192.10.200.199:80/" })  
                // http://192.9.201.12/
             //  .WithOrigins(new[] { "http://192.9.201.12:80" })
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
            );

              app.UseMiddleware<AuthenticationMiddleware>();
            app.UseAuthentication(); //newly added on 26122022 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
