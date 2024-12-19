using Microsoft.OpenApi.Models;
using HotelStore.DB;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotels API", Description = "Shows hotels info", Version = "v1" });
});
    
var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotels API V1");
   });
}
app.MapGet("/hotels", () => HotelDB.GetHotels());    
app.MapGet("/", () => "Hello World!");
    
app.Run();