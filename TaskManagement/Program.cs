using TaskManagement.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext
//builder.Services.AddDbContext<TaskManagementContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// UNCOMMENT THIS
builder.Services.AddDbContext<TaskManagementContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("DefaultConnection")
   ));

// COMMENT THIS AFTER
// builder.Services.AddDbContext<TaskManagementContext>(options => {
//     options.UseMySQL("server=localhost;database=taskmanagement;user=root;password='';");
// });

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
