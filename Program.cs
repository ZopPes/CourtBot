// Класс программы, которая запрашивает внедрения через конструктор 
// абстрактной коробки с содержимым в виде абстрактного кота.
// При выполнении метода Run описание коробки выводится в stdOut.
namespace CourtBot;

public class Program
{
    public static void Main() => new Composition().Root.Run();

    private readonly TelegramBackgroundService _box;

    internal Program(TelegramBackgroundService box) => _box = box;

    private void Run()
    {
        var ts = new CancellationTokenSource();
        _box.ExecuteAsync(ts.Token);
        Console.ReadLine();
        ts.Cancel();
    }
}