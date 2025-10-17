using System;
using System.IO;

public class TempFileWriter : IDisposable
{
    private StreamWriter writer;
    private bool disposed;
    private string filePath;

    public TempFileWriter()
    {
        filePath = Path.Combine(Environment.CurrentDirectory, "tempfile.txt");
        writer = new StreamWriter(filePath, append: false); 
        Console.WriteLine("Створено тимчасовий файл у проекті: " + filePath);
    }

    public void WriteLine(string text)
    {
        if (disposed)
            throw new ObjectDisposedException(nameof(TempFileWriter));

        writer.WriteLine(text);
    }

    public void Dispose()
    {
        if (!disposed)
        {
            writer.Close();
            disposed = true;
            Console.WriteLine("Файл закрито і ресурси звільнено.");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            using (var file = new TempFileWriter())
            {
                file.WriteLine("Это мой крутой тестовый текст для блакнотика)");
                file.WriteLine("Це приклад using з IDisposable.");
            }

            Console.WriteLine("Після using — файл вже закрито.");
        }
        catch (ObjectDisposedException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Програма завершена.");
        }
    }
}