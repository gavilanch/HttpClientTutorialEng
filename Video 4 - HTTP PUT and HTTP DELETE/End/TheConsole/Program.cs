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
            var peopleURL = "https://localhost:44301/api/people";

            using (var httpClient = new HttpClient())
            {
                var person = new Person() { Name = "Felipe Gavilán" };

                // Creating the person
                var responseMessage = await httpClient.PostAsJsonAsync(peopleURL, person);
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                var personId = int.Parse(content);

                // Example 1: Make an HTTP PUT
                person.Id = personId;
                person.Name = "Updated";
                await httpClient.PutAsJsonAsync($"{peopleURL}/{personId}", person);

                var people = await httpClient.GetFromJsonAsync<List<Person>>(peopleURL);

                foreach (var p in people)
                {
                    Console.WriteLine($"Id: {p.Id} - Name: {p.Name}");
                }

                // Example 2: Make an HTTP DELETE

                await httpClient.DeleteAsync($"{peopleURL}/{personId}");
                var people2 = await httpClient.GetFromJsonAsync<List<Person>>(peopleURL);
                if (people2.Count == 0)
                {
                    Console.WriteLine($"There are no records in the table");
                }
            }

            Console.WriteLine("The end");

            Console.Read();
        }
    }
}