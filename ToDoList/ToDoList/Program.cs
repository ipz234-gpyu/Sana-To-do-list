using ToDoList.Factory;
using ToDoList.Repository;
using ToDoList.Models;
using ToDoList.Repository.RepositoryModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add tasks repositories
builder.Services
    .AddSingleton<DapperRepositoryTasks>()
    .AddSingleton<DapperRepository<Tasks>>()
    .AddSingleton<XmlRepositoryTasks>()
    .AddSingleton<XmlRepository<Tasks>>();

// Add categories repositories
builder.Services
    .AddSingleton<DapperRepository<Category>>()
    .AddSingleton<XmlRepository<Category>>();

builder.Services.AddSingleton<SqlRepositoryFactory>();
builder.Services.AddSingleton<XmlRepositoryFactory>();
builder.Services.AddSingleton<RepositoryFactory>();
builder.Services.AddHttpContextAccessor();

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

// Use session
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
