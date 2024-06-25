using Microsoft.EntityFrameworkCore;
using DataAccess;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Previewer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Document.Create(container =>
//{
//    container.Page(page =>
//    {
//        // page content
//    });
//}).ShowInPreviewer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ColmadoDB")));

builder.Services
        .AddDataAccess()
        .AddBusinessLogic();

var app = builder.Build();
QuestPDF.Settings.License = LicenseType.Community;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
