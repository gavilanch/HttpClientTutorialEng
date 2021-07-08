using Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpClientDemo.TheConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var peopleURL = "https://localhost:5001/api/people";

            using (var httpClient = new HttpClient())
            {
                var person = new Person() { Name = "Felipe Gavilán" };

                // Creating the person
                var responseMessage = await httpClient.PostAsJsonAsync(peopleURL, person);
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                var personId = int.Parse(content);

                // Example 1: Make an HTTP PUT
            }

            Console.Read();
        }
    }
}