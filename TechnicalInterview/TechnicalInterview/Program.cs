using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using TechnicalInterview.Infrastructure;
using TechnicalInterview.WebAPI.Dtos.Response;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = new List<string>();
            foreach (var state in context.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            var customResponse = new ApiResponse<object>
            {
                Message = "Error de validacion de datos", 
                Errors = errors
            };

            // 4. Retornar la respuesta como un BadRequestObjectResult
            return new BadRequestObjectResult(customResponse);
        };
    });
//builder.Services.AddApiVersioning();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<TechnicalInterview.Core.Application.Mappings.AccountProfile>();
    cfg.AddProfile<TechnicalInterview.Core.Application.Mappings.TransferProfile>();
});
builder.Services.AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters()
                   .AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options=> options.SwaggerEndpoint("/openapi/v1.json","OpenAPI V1"));
    app.UseReDoc(options =>
    {
        options.SpecUrl("/openapi/v1.json");
        options.RoutePrefix = "redoc";
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();


app.Run();
