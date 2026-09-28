using System.Xml.Linq;

namespace Sportolo_Eredmeny_API.Models
{
    public class Sportolo
    {
        public int id { get; set; } 	
	    public string name { get; set; } 
		public string email { get; set; } 
		public int age { get; set; }
        public string password { get; set; } 
		public DateTime registrationTime { get; set; }
    }
}
