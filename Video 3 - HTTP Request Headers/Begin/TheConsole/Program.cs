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
                //example 1: Sending a value on the header once
                //Ideal when we don't want to affect other HTTP requests

                // example 2: Modifying the headers of all of the requests
              
                //example 3: JWT

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
