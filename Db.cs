namespace HotelStore.DB;
using System.Text.Json;
//room record not needed much i feel
public record Room
	{
		public string ? roomType {get; set;} 
		public int amount {get; set;}
	}
public record Hotel
 	{
		public int id {get; set;} 
		public string ? name { get; set; }
		public string ? location { get; set; }
		public double ? rating { get; set; }
		public string ? imageUrl { get; set; }
		public List<string> ? datesOfTravel { get; set; }
		public string ? boardBasis { get; set; }
		public List<Room> ? rooms { get; set; }
	}

public class HotelDB
 {
 	//read data from json, return hotels list
 	private static List<Hotel> _getJson()  
    {  
    	List<Hotel> hotels = new List<Hotel>();
        using (StreamReader r = new StreamReader("./hotels.json"))  
        {  
            string json = r.ReadToEnd();
            hotels = JsonSerializer.Deserialize<List<Hotel>>(json);
        }
        return hotels;
    }
    //private list of hotels we store with calling getJson. there is a better way I feel
   private static List<Hotel> _hotels = _getJson();
  //for getting hotels from api
   public static List<Hotel> GetHotels() 
   {
     return _hotels;
   } 
   //for getting hotel from api
   public static Hotel ? GetHotel(int id) 
   {
     return _hotels.SingleOrDefault(hotel => hotel.id == id);
   } 
 }
 