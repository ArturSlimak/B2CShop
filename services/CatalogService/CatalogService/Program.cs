using CatalogService.Extensions;
using CatalogService.Helpers;
using CatalogService.Models;
using CatalogService.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


// Add services to the container.
builder.Services.Configure<CatalogDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ProductsService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


//Validation
builder.Services.AddValidatorsFromAssemblyContaining<ProductRequest.Create.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductRequest.Index.Validator>();

builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, services, configuration) =>
{

    configuration.ReadFrom.Configuration(context.Configuration);

    configuration.ReadFrom.Services(services);
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


