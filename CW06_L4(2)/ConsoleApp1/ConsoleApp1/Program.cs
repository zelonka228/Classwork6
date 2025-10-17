using System;
using System.Collections.Generic;
using System.Linq;

public class TextFormatter
{
    public Func<string, string> Strategy { get; set; } = s => s;
    public IEnumerable<string> FormatAll(IEnumerable<string> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));
        foreach (var item in items)
            yield return Strategy(item);
    }
}
class Program
{
    static void Main()
    {
        var formatter = new TextFormatter();

        var texts = new List<string> { "  test 1  ", "Тестик 132#13 ", "  demo  " };

        Console.WriteLine("== За замовчуванням ==");
        foreach (var t in formatter.FormatAll(texts))
            Console.WriteLine(t);


        formatter.Strategy = s => s.Trim().ToUpper();
        Console.WriteLine("\n== TrimToUpper ==");
        foreach (var t in formatter.FormatAll(texts))
            Console.WriteLine(t);

        formatter.Strategy = s => new string(s.Select(c => char.IsDigit(c) ? '*' : c).ToArray());
        Console.WriteLine("\n== MaskDigits ==");
        foreach (var t in formatter.FormatAll(texts))
            Console.WriteLine(t);
    }
}
