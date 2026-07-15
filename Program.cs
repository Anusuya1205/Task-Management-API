using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Task_Management_API;
using Task_Management_API.BL;
using Task_Management_API.DL;
using static Task_Management_API.TaskExpaiyUpdation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1"
    });
});

// Dependency Injection
builder.Services.AddTransient<ConnClass>();
builder.Services.AddTransient<BL_Class>();
builder.Services.AddTransient<BL_Task>();
builder.Services.AddHostedService<TaskExpaiyUpdation>();

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json",
            "Task Management API v1");
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();