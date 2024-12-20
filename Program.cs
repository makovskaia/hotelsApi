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
app.UseCors(MyAllowSpecificOrigins);
// app.MapControllers();

app.UseStatusCodePages(async statusCodeContext 
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                 .ExecuteAsync(statusCodeContext.HttpContext));

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotels API V1");
   });
}

app.UseExceptionHandler(exceptionHandlerApp 
    => exceptionHandlerApp.Run(async context 
        => await Results.Problem()
                     .ExecuteAsync(context)));

app.MapGet("/hotels", () => {
	var hotels = HotelDB.GetHotels();
	if (hotels == null)
    	{
    		Results.BadRequest("Something went wrong!");
    	}
	return hotels;
}); 
app.MapGet("/hotels/{id:int}", (int id) => {
	if(id < 0)
	{
		return Results.BadRequest("Wrong url");
	}
	var hotel = HotelDB.GetHotel(id);
	if (hotel == null)
    {
        return Results.NotFound("Hotel not found");
    }else{
    	return Results.Ok(hotel);
    }
});

app.Run();