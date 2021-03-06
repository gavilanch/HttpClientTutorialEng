using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheConsole
{
    class Program
    {
        static string urlAccounts = "https://localhost:5001/api/accounts";
        static UserInfo credentials = new UserInfo() { EmailAddress = "felipe@hotmail.com", Password = "aA123456!" };
        static async Task Main(string[] args)
        {
            var urlPeople = "https://localhost:5001/api/people";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var urlExample1 = "https://localhost:5001/WeatherForecast";

            using (var httpClient = new HttpClient())
            {
                // example 1: Sending a value on the header once
                // Ideal when we don't want to affect other HTTP requests

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, urlExample1))
                {
                    requestMessage.Headers.Add("weatherAmount", "10");
                    var responseExample1 = await httpClient.SendAsync(requestMessage);
                    var weatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(
                        await responseExample1.Content.ReadAsStringAsync(), jsonSerializerOptions);
                    Console.WriteLine($"Amount of Weather data #1: {weatherForecasts.Count}");
                }

                var weatherForecasts2 = await httpClient.GetFromJsonAsync<List<WeatherForecast>>(urlExample1,
                    jsonSerializerOptions);
                Console.WriteLine($"Amount of Weather data #2: {weatherForecasts2.Count}");

                // example 2: Modifying the headers of all of the requests
                httpClient.DefaultRequestHeaders.Add("weatherAmount", "30");

                var weatherForecasts3 = await httpClient.GetFromJsonAsync<List<WeatherForecast>>(urlExample1,
                   jsonSerializerOptions);
                Console.WriteLine($"Amount of Weather data #3: {weatherForecasts3.Count}");

                var weatherForecasts4 = await httpClient.GetFromJsonAsync<List<WeatherForecast>>(urlExample1,
                 jsonSerializerOptions);
                Console.WriteLine($"Amount of Weather data #4: {weatherForecasts4.Count}");

                // example 3: JWT
                await CreateUser();
                var httpResponseToken = await httpClient.PostAsJsonAsync($"{urlAccounts}/login", credentials);
                var responseToken = JsonSerializer.Deserialize<UserToken>(await
                    httpResponseToken.Content.ReadAsStringAsync(), jsonSerializerOptions);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                    responseToken.Token);

                var response = await httpClient.PostAsJsonAsync(urlPeople, new Person() { Name = "Felipe" });
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Person created successfully");

            }

        }

        private async static Task CreateUser()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var respuesta = await httpClient.PostAsJsonAsync($"{urlAccounts}/create", credentials);
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        respuesta.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
