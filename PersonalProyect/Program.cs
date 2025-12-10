using PersonalProyect;
using PersonalProyect.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureCustomSettings();
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<PermissionsSeeder>();
    await seeder.SeedAsync();
}
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<UserRolesSeeder>();
    await seeder.SeedAsync();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Necesario para que funcionen los endpoints con [Route("api/[controller]")]
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
