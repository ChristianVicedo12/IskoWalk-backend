using Microsoft.EntityFrameworkCore;
using IskoWalk.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// FIX: Use specific MySQL version instead of AutoDetect
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(9, 5, 0))
    ),
    ServiceLifetime.Scoped);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages().WithStaticAssets();

app.Run();
