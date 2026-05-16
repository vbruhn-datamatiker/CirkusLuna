using CirkusLuna.ClassLibrary.Repository;

var builder = WebApplication.CreateBuilder(args);
// Register repositories for dependency injection
builder.Services.AddSingleton<IShowRepository, ShowRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddSingleton<INewsPostRepository, NewsPostRepository>();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
