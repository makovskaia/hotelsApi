using Microsoft.OpenApi.Models;
using HotelStore.DB;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
//allow get requests from our client
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
//they told me to use swagger... i don't know why
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotels API", Description = "Shows hotels info", Version = "v1" });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

//we need to send proper error messages
app.UseStatusCodePages(async statusCodeContext 
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                 .ExecuteAsync(statusCodeContext.HttpContext));

//again with their swagger... why...
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotels API V1");
   });
}
//get hotels
app.MapGet("/hotels", () => {
	var hotels = HotelDB.GetHotels();
	//in case of null we return 500
	if (hotels == null)
	{
		return Results.BadRequest("Something went wrong!");
	}
	return Results.Ok(hotels);
});
//get hotel by id
app.MapGet("/hotels/{id:int}", (int id) => {
	//just in case 500
	if(id < 0)
	{
		return Results.BadRequest("Wrong url");
	}
	var hotel = HotelDB.GetHotel(id);
	//404 for unfortunate events
	if (hotel == null)
    {
        return Results.NotFound("Hotel not found");
    }else{
    	return Results.Ok(hotel);
    }
});

app.Run();