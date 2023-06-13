using System.Diagnostics;

namespace BogusElma365;
//TODO создаем фейковую базу для проверки работы API elma365, запускаем ее для тест-нагрузки

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        //создаем таймер, чтобы узнать время работы программы
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        var taskRepository = new FakeTasksRepositories();
        var tasks = taskRepository.GetAll();
        //Записываем в джейсон-файл, если надо
        /*var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory+"data.json",json);*/
        
        //запускаем цикл запросов
        foreach (var task in tasks)
        {
            var url = $"https://elma-dev.kapitalbank.uz/pub/v1/app/{task.name}/{task.code}/list";
            var token = "ea344949-41bf-491d-8792-d0ba33378390";
            var response = new Response(url,token);
            var responseBody = await response.PostDataAsync("{}");
            Console.WriteLine(responseBody);
        }
        
        //выводим время, за которое проходит весь справочник
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения программы: {stopwatch.Elapsed}");
    }
}