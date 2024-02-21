using Person.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddCustomMvc()
    .AddValidators()    
    .AddRepositories(builder.Configuration)
    .AddAuthenticationService()
    .AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy")
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization()
   .UseHttpsRedirection()
   .UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();


