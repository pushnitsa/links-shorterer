using LinksShorterer.EventManager;
using LinksShorterer.Events.Handlers;
using LinksShorterer.LinkManager;
using LinksShorterer.Options;
using LinksShorterer.Repositories;
using LinksShorterer.ShortererService;
using LinksShorterer.ShortLinkGenerator;
using LinksShorterer.ShortLinkSearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App layer
builder.Services.AddTransient<LinksService>();
builder.Services.AddTransient<IShorterer>(x => x.GetRequiredService<LinksService>());
builder.Services.AddTransient<IRedirector>(x => x.GetRequiredService<LinksService>());
builder.Services.AddTransient<IShortLinkSearch, ShortLinkSearchService>();

builder.Services.AddTransient<ILinkManager, LinkManagerService>();
builder.Services.AddTransient<IShortLinkGenerator, ShortLinkGeneratorService>();
builder.Services.AddSingleton<ILinkRepository, MongoLinkRepository>();
builder.Services.AddSingleton<Func<ILinkRepository>>(x => () => x.GetRequiredService<ILinkRepository>());

builder.Services.AddSingleton<EventManagerImpl>();
builder.Services.AddTransient<IEventManager>(x => x.GetRequiredService<EventManagerImpl>());
builder.Services.AddTransient<IEventDispatcher>(x => x.GetRequiredService<EventManagerImpl>());

builder.Services.AddTransient<LinkHitEventHandler>();

// Configuring options
builder.Services.Configure<MongoOptions>(builder.Configuration.GetSection("MongoDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(corsBuilder =>
    {
        corsBuilder.WithOrigins(builder.Configuration["AllowedHosts"])
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
}
var eventManager = app.Services.GetRequiredService<IEventManager>();
eventManager.Subscribe(app.Services.GetRequiredService<LinkHitEventHandler>());

app.UseAuthorization();

app.MapControllers();

app.Run();
