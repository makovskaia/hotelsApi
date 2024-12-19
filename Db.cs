namespace HotelStore.DB;
using System.Text.Json;

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
 	public static void Test()  
    {  
        List<Hotel> source = new List<Hotel>();  

        using (StreamReader r = new StreamReader("./hotels.json"))  
        {  
            string json = r.ReadToEnd();
            Console.Write(json);
            source = JsonSerializer.Deserialize<List<Hotel>>(json);
            Console.Write(source);
        }
    }  
   private static List<Hotel> _hotels = new List<Hotel>()
   {
     new Hotel{ id=1, name="Seaside Paradise", location="Maldives", rating=4.9 },
     new Hotel{ id=2, name="Cooperaor", location="Ukraine", rating=2.8 },
   };

   public static List<Hotel> GetHotels() 
   {
     return _hotels;
   } 

   public static Hotel ? GetHotel(int id) 
   {
     return _hotels.SingleOrDefault(hotel => hotel.id == id);
   } 
 }
 
 // {
 // "id": 1,
 // "name": "Seaside Paradise",
 // "location": "Maldives",
 // "rating": 4.9,
 // "imageUrl": "https://example.com/images/seaside-paradise.jpg",
 // "datesOfTravel": ["2024-01-01", "2024-01-07"],
 // "boardBasis": "All Inclusive",
 // "rooms": [
 // {
 // "roomType": "Deluxe Suite",
 // "amount": 5
 // },
 // {
 // "roomType": "Family Room",
 // "amount": 3
 // }
 // ]


