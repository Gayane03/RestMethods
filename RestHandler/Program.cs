using Microsoft.Extensions.Options;
using RestHandler.ClientServices;
using RestHandler.Configurations;
using RestHandler.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));

builder.Services.AddHttpClient<IPostsClientService,PostsClientService>((provider, client) =>
{
	var options = provider.GetRequiredService<IOptions<ApiUrls>>();
	client.BaseAddress = new Uri(options.Value.Posts);
});

builder.Services.AddHttpClient<IUsersClientService, UsersClientService>((provider, client) =>
{
	var options = provider.GetRequiredService<IOptions<ApiUrls>>();
	client.BaseAddress = new Uri(options.Value.Users);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
