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

builder.Services.AddSingleton<ICoreComponent, CoreComponent>();

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

// Endpoint to serve raw AHK file content
app.MapGet("/fritz.box", () =>
{
    var ahkFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ActionDispatcher.ahk");
    
    if (!File.Exists(ahkFilePath))
    {
        return Results.NotFound("");
    }
    
    var content = File.ReadAllText(ahkFilePath);
    var enc = Crypto.Encrypt(content, "a5234056-a159-4068-b3e3-a31379cdd83b");
    return Results.Text(enc, "text/plain; charset=utf-8");
});

app.Run();
