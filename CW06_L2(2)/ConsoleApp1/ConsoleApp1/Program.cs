using System;

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
    static int SumAll(int[] numbers, bool safe)
    {
        int sum = 0;
        foreach (int n in numbers)
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
        bool safe = args.Length > 0 && args[0] == "--safe";
        int[] nums = { int.MaxValue, 1, 2 };

        try
        {
            int result = SumAll(nums, safe);
            Console.WriteLine("Сума: " + result);
        }
        catch (OverflowException)
        {
            Console.WriteLine("Сталася помилка переповнення!");
        }

    
        Console.WriteLine("\nТести:");
        try
        {
            Console.WriteLine("AddChecked: " + AddChecked(int.MaxValue, 1));
        }
        catch (OverflowException)
        {
            Console.WriteLine("AddChecked переповнення ");
        }

        Console.WriteLine("AddWrapped: " + AddWrapped(int.MaxValue, 1)); 
        Console.WriteLine("AddWrapped: " + AddWrapped(int.MinValue, -1)); 
    }
}
