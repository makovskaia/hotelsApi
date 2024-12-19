using Microsoft.OpenApi.Models;
using HotelStore.DB;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                          .WithMethods("GET");
                      });	
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotels API", Description = "Shows hotels info", Version = "v1" });
});
    
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotels API V1");
   });
}


app.MapGet("/hotels", () => HotelDB.GetHotels()); 
app.MapGet("/hotels/{id}", (int id) => HotelDB.GetHotel(id)); 
app.MapGet("/", () => "Hello World!");
app.Run();