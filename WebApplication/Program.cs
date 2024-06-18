using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

CreateApp(args, ConfigureMiddlewarePipeline);

static void CreateApp(string[] args, Action<WebApplication> registerServices)
{
    var builder = WebApplication.CreateBuilder(args);
    RegisterServices(builder);
    var app = builder.Build();
    registerServices.Invoke(app);
}
static void ConfigureMiddlewarePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
static void RegisterServices(WebApplicationBuilder builder)
{
    //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
    builder.Services.AddControllers()
        .AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
         });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

