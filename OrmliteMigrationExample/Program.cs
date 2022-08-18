using System.Reflection;
using ServiceStack.OrmLite;

namespace OrmliteMigrationExample;

public class Program
{
    public static void Main(string[] args)
    {
        var dbFactory = new OrmLiteConnectionFactory("data.db", SqliteDialect.Provider);
        using var db = dbFactory.Open();

        var result = db.Migrate(true, Assembly.GetExecutingAssembly());
        Console.WriteLine($"Migrate Completed, Result={result}");
        Console.ReadKey();
    }
}