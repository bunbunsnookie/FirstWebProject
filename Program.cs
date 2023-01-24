using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Web_2k.Database;
using Web_2k.Models;
using Microsoft.Data.SqlClient;
using WebApplication1.Objects;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

// Add services to the container.
var services = builder.Services;

services.AddControllersWithViews();
services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "client/build";
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("");
        });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db) => await db.Users.ToListAsync());

/*app.MapGet("/api/users/{id:Guid}", async (Guid id, ApplicationContext db) =>
{
    // получаем пользовател€ по id
    User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, отправл€ем его
    return Results.Json(user);
});

app.MapPost("/api/users", async (string Login, string Password, ApplicationContext db) =>
{
    SqlConnection conn = new SqlConnection("Server = (localdb)\\mssqllocaldb; Database = applicationdb; Trusted_Connection = True;");
    conn.Open();
    string checkUser = "SELECT count(*) FROM [Users] WHERE Login='" + Login + "'" + " AND " + "Password = '" + Password + "'";
    SqlCommand cmd = new SqlCommand(checkUser, conn);
    bool exists = (int)cmd.ExecuteScalar() > 0;
    if (exists)
    {
        Console.WriteLine("User Exists");
    }
    conn.Close();

});


app.MapPut("/api/users", async (User userData, ApplicationContext db) =>
{
    // получаем пользовател€ по id
    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, измен€ем его данные и отправл€ем обратно клиенту
    user.Login = userData.Login;
    user.Password = userData.Password;
    await db.SaveChangesAsync();
    return Results.Json(user);
});*/




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "client";

    if (environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

app.Run();