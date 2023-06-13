using System.Net.Http.Headers;
using System.Text;

namespace BogusElma365;
//класс, в котором мы выполняем HTTP-запросы
public class Response
{
    private HttpClient client = new HttpClient();
    private string _url;
    private string _token;
    
    //конструктор на случай, если не нужна авторизация через токен
    public Response(string url)
    {
        _url = url;
    }

    public Response(string url, string token)
    {
        _url = url;
        _token = token;
    }
    //метод Get-запроса
    public async Task<string> GetResponseAsync()
    {
        try
        {
            SetBearerToken();
            HttpResponseMessage response = await client.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nВозникло исключение!");
            Console.WriteLine("Сообщение: {0}", e.Message);
            return null;
        }
    }
    
    //метод Post-запроса, в параметр передаем json-строку с телом запроса
    public async Task<string> PostDataAsync(string data)
    {
        try
        {
            SetBearerToken();
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nВозникло исключение!");
            Console.WriteLine("Сообщение: {0}", e.Message);
            return null;
        }
    }

    //метод, проверяющий наличие токена. Если он есть, то проводим авторизацию
    private void SetBearerToken()
    {
        if (_token!=null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}