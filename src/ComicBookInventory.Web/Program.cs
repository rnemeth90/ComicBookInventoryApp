using System.Configuration;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient(name: "ComicBookInventory.Api",
//    configureClient: options =>
//    {
//        options.BaseAddress = new Uri(builder.Environment.);
//        options.DefaultRequestHeaders.Accept.Add(
//            new MediaTypeWithQualityHeaderValue("application/json",1.0));
//    });

builder.Services.AddHttpClient();

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
    pattern: "{controller=ComicBook}/{action=GetAllComics}/{id?}");

app.Run();
