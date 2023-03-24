namespace PerformanceMeter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var foo = new PerformanceUsing())
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Hello, World!");
                }
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Hello, World!");
                }
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Hello, World!");
                }
            }
        }
    }

    public class PerformanceUsing : IDisposable
    {
        public DateTime Begin { get; set; } = DateTime.Now;
        public void Dispose()
        {
            DateTime completion = DateTime.Now;
            TimeSpan estimatedTime = completion - Begin;
            Console.WriteLine($"{estimatedTime.TotalMilliseconds} ms");
        }
    }
}