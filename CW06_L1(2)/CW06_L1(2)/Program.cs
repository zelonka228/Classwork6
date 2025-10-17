using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

class Program
{

    static (string key, string value) ParseSetting(
        string line,
        [CallerArgumentExpression("line")] string name = "")
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentNullException(name, "Рядок порожній");

        if (!line.Contains('='))
            throw new FormatException($"Немає '=' у {name}: \"{line}\"");

        string[] parts = line.Split('=', 2);
        return (parts[0].Trim(), parts[1].Trim());
    }


    static Dictionary<string, string> ParseFile(string path)
    {
        var result = new Dictionary<string, string>();
        string[] lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line == "" || line.StartsWith("#"))
                continue;

            try
            {
                var (k, v) = ParseSetting(line);
                result[k] = v;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка у рядку {i + 1}: {e.Message}");
            }
        }

        return result;
    }
    static void Main()
    {
        try
        {
            var config = ParseFile("test.txt");
            Console.WriteLine("Налаштування:");
            foreach (var x in config)
                Console.WriteLine($"{x.Key} = {x.Value}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Готово.");
        }
    }
}
