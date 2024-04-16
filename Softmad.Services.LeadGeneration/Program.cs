using Microsoft.EntityFrameworkCore;
using Softmad.Services.LeadGeneration.Data;
using Softmad.Services.LeadGeneration.Repository;
using Softmad.Services.LeadGeneration.Repository.Interfaces;
using Softmad.Services.LeadGeneration.Services;
using Softmad.Services.LeadGeneration.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("LeadsConnection"))); //move this to SQL Server

#region Application Services
// builder.Services.Lifetime<Interface, Implimentation>();
builder.Services.AddScoped<ILeadGenerationService, LeadGenerationService>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
