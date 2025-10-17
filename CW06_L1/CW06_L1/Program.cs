using System;
using System.Runtime.CompilerServices;

class Program
{
    public static (string key, string value) ParseSetting(
        string line,
        [CallerArgumentExpression("line")] string paramName = "")
    {
        // Перевірка на порожній рядок або null
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentNullException(paramName, "Рядок порожній або не заданий.");

        // Перевірка на відсутність символу '='
        if (!line.Contains('='))
            throw new FormatException($"Рядок не містить '=' у {paramName}: \"{line}\"");

        // Розбиття рядка на ключ і значення
        string[] parts = line.Split('=', 2);
        return (parts[0].Trim(), parts[1].Trim());
    }

    static void Main()
    {
        var result = ParseSetting("test=1w");
        Console.WriteLine($"Ключ: {result.key}, Значення: {result.value}");
    }
}