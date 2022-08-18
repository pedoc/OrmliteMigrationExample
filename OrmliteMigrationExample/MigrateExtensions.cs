using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace ServiceStack.OrmLite
{
    public static class MigrateExtensions
    {
        public static bool Migrate(
            this IDbConnection db,
            bool enableTransaction,
            params Assembly[] migrationAssemblies)
        {

            var migratory = migrationAssemblies
                .SelectMany(x => x.GetTypes().Where(x => x.IsInstanceOf(typeof(MigrateBase)) && !x.IsAbstract))
                .Select(t => (MigrateBase)Activator.CreateInstance(t, new object[] { db }))
                .Where(x => !x.Ignore)
                .OrderBy(x => x.Order)
                .ThenBy(i =>
                {
                    var type = i.GetType();
                    var match = Regex.Match(type.Name, "\\d+");
                    var result = DateTime.MaxValue;
                    if (match.Success && (
                            DateTime.TryParseExact(match.Value, "yyyyMMddHHmmss", CultureInfo.CurrentCulture,
                                DateTimeStyles.None, out result) ||
                            DateTime.TryParseExact(match.Value, "yyyyMMddHHmm", CultureInfo.CurrentCulture,
                                DateTimeStyles.None, out result)
                        ))
                    {
                        return result;
                    }

                    return result;
                }).ToList();
            var trans = enableTransaction ? db.OpenTransactionIfNotExists() : null;
            try
            {
                foreach (var migrator in migratory)
                {
                    var type = migrator.GetType();
                    Console.WriteLine($"Executing Migrator ：{type.Name}，Description：{migrator.Description}");
                    migrator.Up();
                    migrator.Down();
                    migrator.SeedData();
                }

                trans?.Commit();
            }
            catch
            {
                trans?.Rollback();
                throw;
            }

            return true;
        }

        public static bool AddTableIfNotExists<TEntity>(this IDbConnection connection)
        {
            return connection.CreateTableIfNotExists<TEntity>();
        }

        public static void DropTableIfExists<T>(this IDbConnection db)
        {
            if (db.TableExists<T>()) db.DropTable<T>();
        }

        public static void AddColumnIfNotExists<T>(this IDbConnection db, Expression<Func<T, object>> fieldSelector)
        {
            if (!db.ColumnExists(fieldSelector))
                db.AddColumn(fieldSelector);
        }
    }
}
