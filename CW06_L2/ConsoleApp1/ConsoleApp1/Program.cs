using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

class Program
{
    static int AddChecked(int a, int b)
    {
        checked
        {
            return a + b;
        }
    }
    static int AddWrapped(int a, int b)
    {
        unchecked
        {
            return a + b;
        }
    }
    static int SumAll(int[] values, bool safe)
    {
        int sum = 0;
        foreach (int n in values)
        {
            if (safe)
                sum = checked(sum + n);
            else
                sum = unchecked(sum + n);
        }
        return sum;
    }
    static void Main(string[] args)
    {

        bool safe = false;
        if (args.Length > 0 && args[0] == "--safe")
            safe = true;

        int[] numbers = { int.MaxValue, 1, 2 };

        try
        {
            int total = SumAll(numbers, safe);
            Console.WriteLine($"Сума = {total}");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Переповнення");
        }
        finally
        {
            Console.WriteLine("Роботу завершено.");
        }

        // 1. Переповнення в checked
        try
        {
            AddChecked(int.MaxValue, 1);
        }
        catch (OverflowException)
        {
            Console.WriteLine("Test 1: AddChecked переповнення");
        }
        // 2. Unchecked не видає  помилку
        Console.WriteLine("Test 2: AddWrapped = " + AddWrapped(int.MaxValue, 1));
        // 3. Безпечна сума з checked
        try
        {
            SumAll(new int[] { int.MaxValue, 1 }, true);
        }
        catch (OverflowException)
        {
            Console.WriteLine("Test 4: SumAll  переповнення");
        }
    }
}
