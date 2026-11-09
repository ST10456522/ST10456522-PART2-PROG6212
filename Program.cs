var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

string apiUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddHttpClient("RaceDayApi", client =>
{
    client.BaseAddress = new Uri(apiUrl);
})
.ConfigurePrimaryHttpMessageHandler(() =>
 {
     var handler = new HttpClientHandler();
     // THIS LINE BYPASSES THE SSL ERROR
     handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
     return handler;
 });
builder.Services.AddScoped<RaceDayMVC.Services.RaceDayApiClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // <-- MUST BE HERE
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();