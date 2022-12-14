using LinksShorterer.LinkStorage;
using LinksShorterer.ShortererService;
using LinksShorterer.ShortLinkGenerator;

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

builder.Services.AddSingleton<LinkStorageService>();
builder.Services.AddTransient<ILinkStorage>(x => x.GetRequiredService<LinkStorageService>());
builder.Services.AddTransient<ILinkExistanceValidator>(x => x.GetRequiredService<LinkStorageService>());

builder.Services.AddTransient<IShortLinkGenerator, ShortLinkGeneratorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
