using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Softmad.LeadGeneration.Areas.Identity.Data;
using Softmad.LeadGeneration.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SoftmadLeadGenerationContextConnection") ?? throw new InvalidOperationException("Connection string 'SoftmadLeadGenerationContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SoftmadLeadGenerationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDaprClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();



app.Run();
