using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class AsyncFileCopier : IAsyncDisposable
{
    private bool disposed;

    public async Task CopyAsync(string src, string dst, CancellationToken ct)
    {
        if (disposed)
            throw new ObjectDisposedException(nameof(AsyncFileCopier));

        try
        {
            await using (FileStream input = new FileStream(src, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
            await using (FileStream output = new FileStream(dst, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                await input.CopyToAsync(output, ct);
                Console.WriteLine("Файл скопійовано успішно.");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Копіювання скасовано користувачем.");
            throw;
        }
    }

    public ValueTask DisposeAsync()
    {
        disposed = true;
        Console.WriteLine("Ресурси AsyncFileCopier звільнено.");
        return ValueTask.CompletedTask;
    }
}
class Program
{
    static async Task Main()
    {
        try
        {
            using var cts = new CancellationTokenSource();

            await using (var copier = new AsyncFileCopier())
            {
                await copier.CopyAsync("test.txt", "copy_test.txt", cts.Token);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Асинхронна програма завершена.");
        }
    }
}
