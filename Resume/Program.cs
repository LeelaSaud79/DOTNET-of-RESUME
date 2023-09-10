using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Resume.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ResumeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ResumeContext") ?? throw new InvalidOperationException("Connection string 'ResumeContext' not found.")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CROSPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


//builder.Services.AddDbContext<DbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("ResumeContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
