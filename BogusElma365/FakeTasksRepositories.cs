using Bogus;

namespace BogusElma365;

public class FakeTasksRepositories
{
    private readonly IEnumerable<Task> _tasks;
    private int count = 100;//количество созданных фейковых задач

    
    public FakeTasksRepositories()
    {
        var taskFaker = new Faker<Task>()
            .RuleFor(t => t.name, "dokumentooborot") // Установка одинакокого имени для каждой задачи
            .RuleFor(t => t.code, "vkhodyashaya_korrespondenciya"); // Установка одинакового кода для всех задач;
        _tasks = taskFaker.Generate(count);
    }
    
    //Получение всех тасков
    public IEnumerable<Task> GetAll()
    {
        return _tasks;
    }
}