using Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://localhost:5001/api/people";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var httpClient = new HttpClient())
            {
                // Example 1: PostAsJsonAsync
                //var newPerson = new Person() { Name = "Felipe" };
                //var response = await httpClient.PostAsJsonAsync(url, newPerson);
                //if (response.IsSuccessStatusCode)
                //{
                //    var id = await response.Content.ReadAsStringAsync();
                //    Console.WriteLine("The id is " + id);
                //}

                // Example 2: PostAsync
                //var newPerson2 = new Person() { Name = "Claudia" };
                //var newPerson2Serialized = JsonSerializer.Serialize(newPerson2);
                //var stringContent = new StringContent(newPerson2Serialized, Encoding.UTF8, "application/json");

                //var response = await httpClient.PostAsync(url, stringContent);

                // Example 3: Validations
                var newPerson3 = new Person() { Age = -1, Email = "abc" };
                var response = await httpClient.PostAsJsonAsync(url, newPerson3);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    var errorsFromWebAPI = Utils.ExtractErrorsFromWebAPIResponse(body);

                    foreach (var fieldWithErrors in errorsFromWebAPI)
                    {
                        Console.WriteLine($"-{fieldWithErrors.Key}");
                        foreach (var error in fieldWithErrors.Value)
                        {
                            Console.WriteLine($"  {error}");
                        }
                    }
                }

                var people = JsonSerializer.Deserialize<List<Person>>(await httpClient.GetStringAsync(url),
                    jsonSerializerOptions);
            }

        }
    }
}
