using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Softmad.LeadGeneration.Areas.Identity.Data;
using Softmad.LeadGeneration.AutoMapper;
using Softmad.LeadGeneration.Data;
var builder = WebApplication.CreateBuilder(args);

// Fetch the connection string from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(builder.Configuration["ConnectionStrings:AppConfig"]);

// Now retrieve the connection string from App Configuration
var connectionString = builder.Configuration["DefaultDatabaseConnectionString"]
                       ?? throw new InvalidOperationException("Connection string 'AccountsContextConnection' not found.");

builder.Services.AddDbContext<AccountsContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddHttpClient("MyApiClient", client =>
{
    //client.BaseAddress = new Uri("http://softmad.services.leadgeneration:8080/");
    client.BaseAddress = new Uri(builder.Configuration["ApiClientUri"]);

    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddDefaultUI().AddEntityFrameworkStores<AccountsContext>().AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin",
         policy => policy.RequireRole("admin"));
});
builder.Services.AddAutoMapper(typeof(MapProfiles));
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
