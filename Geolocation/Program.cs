using System;
using System.Net;
using System.Text.Json;
namespace Geolocation
{
    class Program
    {
        private static readonly HttpClient m_Client = new HttpClient(); //HTTP клиент для запросов.
        private static async Task GetCoordinatesFromYandex(string address)
        {
            string apiKey = "1ff185b3-8d67-4008-9bb3-3f26af777870";
            string url = $"https://geocode-maps.yandex.ru/1.x/?apikey={apiKey}&geocode={address}&format=json";
            try
            {
                string response = await m_Client.GetStringAsync(url); //Ответ на запрос в виду Json
                JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                GeoResponseYandex data = JsonSerializer.Deserialize<GeoResponseYandex>(response, options); // Десерилизование Json
                string pos = data.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Point.Pos; // Координаты 1 элемента ответа.
                string description= data.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Description; // Страна 1 элемента ответа.
                string name= data.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Name; // Имя 1 элемента ответа.
                if (!string.IsNullOrEmpty(description))
                {
                    Console.WriteLine($"Яндекс: {description}");
                }

                else
                {
                    Console.WriteLine("Яндекс: Страна не найдена.");
                }

                if (!string.IsNullOrEmpty(name))
                {
                    Console.WriteLine($"Яндекс: {name}");
                }

                else
                {
                    Console.WriteLine("Яндекс: Имя не найдено.");
                }

                if (!string.IsNullOrEmpty(pos))
                {
                    string[] coord = pos.Split(' ');
                    Console.WriteLine($"Яндекс: Долгота={coord[0]}, Широта={coord[1]}");
                }

                else
                {
                    Console.WriteLine("Яндекс: Координаты не найдены.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Яндекс: Ошибка запроса - {ex.Message}");
            }
        }

        private static async Task GetCoordinatesFromGis(string address)
        {
            string apiKey = "1d2f3fd8-141a-463a-bb26-716cc3e30001";
            string url = $"https://catalog.api.2gis.com/3.0/items/geocode?q={address}&fields=items.point&key={apiKey}";
            try
            {
                string response = await m_Client.GetStringAsync(url); //Ответ на запрос в виду Json
                JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                GeoResponseGis data = JsonSerializer.Deserialize<GeoResponseGis>(response, options); // Десерилизование Json
                string name = data.Result.Items[0].Full_name; // Имя 1 элемента ответа.
                float lat = data.Result.Items[0].Point.Lat; // Широта 1 элемента ответа.
                float lon = data.Result.Items[0].Point.Lon; // Долгота 1 элемента ответа.

                if (!string.IsNullOrEmpty(name))
                {
                    Console.WriteLine($"2Gis: {name}");
                }

                else
                {
                    Console.WriteLine("2Gis: Имя не найдено.");
                }

                if (lon != float.NaN)
                {
                    Console.WriteLine($"2Gis: Долгота={lon}");
                }

                else
                {
                    Console.WriteLine("2Gis: Координаты не найдены.");
                }

                if (lat!=float.NaN)
                {
                    Console.WriteLine($"2Gis: Широта={lat}");
                }

                else
                {
                    Console.WriteLine("2Gis: Координаты не найдены.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"2Gis: Ошибка запроса - {ex.Message}");
            }
        }
        static async Task Main()
        {
            while (true)
            {
                Console.Write("Введите адрес: ");
                string address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Ошибка: Адресс не может быть пустым");
                    continue;
                }
                await GetCoordinatesFromYandex(address);
                await GetCoordinatesFromGis(address);
            }
        }
    }
}