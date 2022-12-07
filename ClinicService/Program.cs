using ClinicServiceData;

using Microsoft.EntityFrameworkCore;

using System.Net;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.


 builder.WebHost.ConfigureKestrel(options =>
{
	options.Listen(IPAddress.Any, 5120, listenOptions =>
	{
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
	});
	options.Listen(IPAddress.Any, 5001, listenOptions =>
	{
		listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
	});
});



builder.Services.AddGrpc().AddJsonTranscoding();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Database
builder.Services.AddDbContext<ClinicServiceDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration["DatabaseSettings:ConnectionString"]);
});
#endregion

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title ="Clinic Service", Version = "v1"});
	var filePath = Path.Combine(AppContext.BaseDirectory, "ClinicService.xml");
	c.IncludeXmlComments(filePath);
	c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});
builder.Services.AddGrpcSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c=> { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true});
app.MapGrpcService<ClinicService.Services.ClinicService>().EnableGrpcWeb();

app.MapGet("/", ()=>
	"Communication with gRPC endpoints must be made through a gRPC client.");		

app.Run();
