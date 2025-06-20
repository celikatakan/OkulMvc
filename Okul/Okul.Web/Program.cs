using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations.Bolum;
using Okul.Business.Operations.Ders;
using Okul.Business.Operations.Not;
using Okul.Business.Operations.Ogrenci;
using Okul.Business.Operations.Ogretmen;
using Okul.Data.Models;
using Okul.Data.Repository;
using Okul.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<OkulDbContext>(options => options.UseSqlServer(cs));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOgrenciService, OgrenciManager>();
builder.Services.AddScoped<IBolumService, BolumManager>();
builder.Services.AddScoped<IOgrenciService, OgrenciManager>();
builder.Services.AddScoped<IOgretmenService, OgretmenManager>();
builder.Services.AddScoped<IDersService, DersManager>();
builder.Services.AddScoped<INotService, NotManager>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ogretmen}/{action=Index}/{id?}");

app.Run();



