using RestHandler.ClientServices;
using RestHandler.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<PostsClientService>(BackendApi.Posts, client =>
{
	client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrls")[BackendApi.Posts]!);
});

builder.Services.AddHttpClient<UsersClientService>(BackendApi.Users, client =>
{
	client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrls")[BackendApi.Users]!);
});

builder.Services.AddScoped<IPostsClientService, PostsClientService>();
builder.Services.AddScoped<IUsersClientService, UsersClientService>();
builder.Services.AddScoped<IResponseMessageUtile, ResponseMessageUtile>();

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
