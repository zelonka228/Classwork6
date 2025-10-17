using System;

class Program
{
    static string Transform(string s, Func<string, string> strategy)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (strategy == null) throw new ArgumentNullException(nameof(strategy));
        return strategy(s);
    }
    static string TrimToUpper(string s) => s.Trim().ToUpper();

    static string MaskDigits(string s)
    {
        var result = "";
        foreach (char c in s)
        {
            result += char.IsDigit(c) ? '*' : c;
        }
        return result;
    }

    static void Main()
    {
        string input = "  Проверка микрофона 123 1 2 3  ";

        Console.WriteLine("Оригінал: " + input);
        Console.WriteLine("TrimToUpper: " + Transform(input, TrimToUpper));
        Console.WriteLine("MaskDigits: " + Transform(input, MaskDigits));
    }
}
