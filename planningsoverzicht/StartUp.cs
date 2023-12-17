using Microsoft.AspNetCore.Http;
using planningsoverzicht.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using planningsoverzicht.Services;
using System.Timers;
using planningsoverzicht.DB;
using System.Drawing.Text;

namespace planningsoverzicht;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var aTimer = new System.Timers.Timer(60 * 10 * 1000); //one hour in milliseconds
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.Start();

        services.AddScoped<IWerknemerData, InMemoryWerknemerData>();
        services.AddScoped<ITeamData, InMemoryTeamData>();
        services.AddScoped<ILidmaatschapData, InMemoryTeamData>();
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                        .WithMethods("GET", "POST")
                        .WithHeaders("content-type", "accept");
            });
        });

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("./Swagger/v1/Swagger.json", "PlanningOverzicht");
                c.RoutePrefix = string.Empty;
            });
        }
        app.UseRouting();
        app.UseCors();
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
        });
    }
    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        ConvertPlanning convertPlanning = new ConvertPlanning();
        InMemoryWerknemerData werknemerData = new InMemoryWerknemerData();
        AfspraakDB agendaDB = new AfspraakDB();

        agendaDB.UpdateAgendas(convertPlanning.ConvertPlanningToDB(werknemerData.GetAll().ToList()));
    }
}
