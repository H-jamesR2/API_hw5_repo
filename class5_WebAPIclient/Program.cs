using System;
using System.Threading.Tasks;
using System.Net.Http;
// must manage nuget packages on Dependencies and add newtonsoft.json;
using Newtonsoft.Json;
using System.Collections.Generic;

// https://documenter.getpostman.com/view/8854915/Szf7znEe

///*
namespace WebAPIClient
{
    // Country/ZipCode client;
    // get post code, country+abbreviation (2);
        // get places: place name, LONG, state+abbrev(2), LAT
    class Location
    {
        [JsonProperty("post code")]
        public string PostCode { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        public List<Places> Places { get; set; }
    }

    // places contain objects w/ place name, LONG, state+abbrev(2), LAT
    public class Places
    {
        [JsonProperty("place name")]
        public string PlaceName { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
    }


    class  Program
    {
        private static readonly HttpClient client = new HttpClient();
        // Task = object => work that should be done
        // async keyword indicates to C# that the method is asynchronous
        // run independent of main program flow + may use arbitary # of wait;


        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a Thailand ZIP Code (TH denominated only with range of 10100 : 96220):");
                    Console.WriteLine("Press Enter without writing a numerical code to quit the program.");

                    var locationZIPcode = Console.ReadLine();
                    if (string.IsNullOrEmpty(locationZIPcode))
                    {
                        break;
                    }

                    // TH only testing. Thailand
                    var result = await client.GetAsync("https://api.zippopotam.us/TH/" + locationZIPcode.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    // Serializing + Displaying Results:
                    var location = JsonConvert.DeserializeObject<Location>(resultRead);

                    //Console.WriteLine(result);
                    if ((int)result.StatusCode == 404) // not valid
                    {
                        //throw new ArgumentException(String.Format("{0} is not an even number", num),
                        //              "num");
                        throw new ArgumentException(
                            String.Format("{0} is not a valid zip code",locationZIPcode)
                        );
                    }

                    Console.WriteLine("-----");
                    Console.WriteLine("Postal Code: " + location.PostCode);
                    Console.WriteLine("Country+Abbreviation: " + location.Country + ", "
                        + location.CountryAbbreviation);
                    Console.WriteLine("Places:");

                    location.Places.ForEach(p => {
                        Console.WriteLine(" - PlaceName: " + p.PlaceName);
                        Console.WriteLine(" [Longitude, Latitude]: " + "[" + p.Longitude
                            +", "+ p.Latitude + "]");
                        // add case for when empty
                        Console.WriteLine(" State+Abbreviation: " + p.State + ", "
                            + p.StateAbbreviation);
                        Console.Write("\n");
                    });
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid ZIP Code");
                    Console.WriteLine("\n--");
                }
            }
        }

    }
} //*/

/*
namespace WebAPIClient
{
    class Pokémon
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("weight")]
        public int Weight { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }

        public List<Types> Types { get; set; }
    }

    public class Type
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class Types
    {
        [JsonProperty("type")]
        public Type Type;
    }


    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        // Task = object => work that should be done
        // async keyword indicates to C# that the method is asynchronous
        // run independent of main program flow + may use arbitary # of wait;


        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Pokemon name. Press Enter without writing a name to quit the program.");

                    var pokémonName = Console.ReadLine();
                    if (string.IsNullOrEmpty(pokémonName))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + pokémonName.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    // Serializing + Displaying Results:
                    var pokémon = JsonConvert.DeserializeObject<Pokémon>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Pokemon #" + pokémon.Id);
                    Console.WriteLine("Name: " + pokémon.Name);
                    Console.WriteLine("Weight: " + pokémon.Weight + "lb");
                    Console.WriteLine("Height: " + pokémon.Height + "ft");
                    Console.WriteLine("Type(s):");
                    pokémon.Types.ForEach(t => Console.Write(" " + t.Type.Name));
                    Console.WriteLine("\n---");



                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid Pokémon name!");
                }
            }
        }

    }
}
*/



