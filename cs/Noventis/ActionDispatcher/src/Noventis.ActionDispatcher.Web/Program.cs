using Noventis.ActionDispatcher.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to bind to both localhost and network interface
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5026);
    options.ListenLocalhost(5027);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
