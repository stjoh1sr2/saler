using System;
using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using salerapp.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalerContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SalerContext")));

// Add services to the container.
builder.Services.AddRazorPages();

// Add session support
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}"
    );
});

app.MapRazorPages();

app.Run();

// namespace salerapp
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             try 
//             { 
//                 SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
//                 builder.DataSource = "salersqlserver.database.windows.net"; 
//                 builder.UserID = "saleradmin";            
//                 builder.Password = "Compsci10!";     
//                 builder.InitialCatalog = "SalerDatabase";

//                 using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
//                 {
//                     Console.WriteLine("\nQuery data example:");
//                     Console.WriteLine("=========================================\n");

//                     String sql = "SELECT name, collation_name FROM sys.databases";

//                     using (SqlCommand command = new SqlCommand(sql, connection))
//                     {
//                         connection.Open();
//                         using (SqlDataReader reader = command.ExecuteReader())
//                         {
//                             while (reader.Read())
//                             {
//                                 Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
//                             }
//                         }
//                     }                    
//                 }
//             }
//             catch (SqlException e)
//             {
//                 Console.WriteLine(e.ToString());
//             }
//             Console.ReadLine();
//         }
//     }
// }
