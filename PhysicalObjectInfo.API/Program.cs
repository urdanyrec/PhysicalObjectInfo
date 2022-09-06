using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhysicalObjectInfo.API.Service;
//using DependencyInjectionSample.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//http client обработчик опроса контроллеров по url



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhysicalObjectInfo.Infrastructure.Context>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("_myAllowSpecificOrigins", builder => builder.WithOrigins("http://external:80/")
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());
});

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IpollService, Poll>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthorization();

app.MapControllers();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
});*/


app.Run();

//app.RunAsync();


    /*var url = "http://192.168.3.12/api";
    var client = new HttpClient();

    var msg = new HttpRequestMessage(HttpMethod.Get, url);
    msg.Headers.Add("aaa", "bbb");
    var response = await client.SendAsync(msg);
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);*/
