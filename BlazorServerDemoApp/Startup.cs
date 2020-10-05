using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorServerDemoApp.Utilities;
using BlazorServerDemoApp.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataLibrary.Db;
using DataLibrary.Data;

namespace BlazorServerDemoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default"
            });
            services.AddSingleton<IDataAccess, SqlDb>();
            services.AddSingleton<IFoodData, FoodData>();
            services.AddSingleton<IOrderData, OrderData>();
            services.AddSingleton<IBookData, BookData>();
            services.AddSingleton<ICourseData, CourseData>();
            services.AddSingleton<IDoctorData, DoctorData>();
            services.AddSingleton<IMedicalVisitData, MedicalVisitData>();
            services.AddSingleton<IMedicineData, MedicineData>();
            services.AddSingleton<IRefillData, RefillData>();
            services.AddSingleton<ITrackingData, TrackingData>();
            services.AddSingleton<IAccountData, AccountData>();
            services.AddSingleton<ILedgerData, LedgerData>();
            services.AddSingleton<ISugarData, SugarData>();

            services.AddAutoMapper
                (typeof(AutoMapperProfile).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IDoctorData doctorData,
            IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var ui = new DoctorUI(doctorData, mapper);
                ui.CreateTemplates();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
