using Microsoft.EntityFrameworkCore;
using myappdotnet.Controllers;
using myappdotnet.Model;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(); 
builder.Services.AddControllers();
builder.Services.AddScoped<AuthorController>(); 
builder.Services.AddScoped<BookController>(); 
builder.Services.AddScoped<LoanController>(); 

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();