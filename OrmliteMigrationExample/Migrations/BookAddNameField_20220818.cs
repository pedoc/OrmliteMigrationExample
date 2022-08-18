using System.Data;
using OrmliteMigrationExample.Entity;
using ServiceStack.OrmLite;

namespace OrmliteMigrationExample.Migrations
{
    public class BookAddNameField_20220818 : MigrateBase
    {
        public BookAddNameField_20220818(IDbConnection connection) : base(connection)
        {
        }
        public override int Order => 1;
        public override string Name => "Add Name Filed";

        public override void Up()
        {
            Connection.AddColumnIfNotExists<Book>(b => b.Name);
        }
    }
}
